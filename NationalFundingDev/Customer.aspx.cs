using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace NationalFundingDev
{
    public partial class CustomerPage : System.Web.UI.Page
    {
        #region Local Variables
        public Customer customer;
        public String CustomerID;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public User user;
        #endregion

        #region Page Load Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the Organization Code from the url www.url/Customer.aspx?OrgCode={0}
            CustomerID = Request.QueryString["CustomerID"];
            //Set customer to be the one from the Database
            customer = siftaDB.Customers.FirstOrDefault(p => p.CustomerID.ToString() == CustomerID);
            //If the customer is not valid send them back to the Default Page
            if (customer == null) Response.Redirect("Default.aspx");
            //Set the User to be a User from this OrgCode for Permissions
            user = new User(customer.Center.OrgCode);
            //Set the Session variable Title 
            Session["Title"] = customer.Center.Name.Replace(" Water Science Center", " ");
            //Set Customer Logo Source
            imgCustLogo.ImageUrl = String.Format("http://sifta.water.usgs.gov/Services/REST/Customer/CustomerIcon.ashx?CustomerID={0}", CustomerID);
            //Set the target folder for the image uploader
            var dir = "D:\\siftaroot\\Temp\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            rauImage.TargetFolder = dir;
            if(user.CanInsert)
            {
                pnlCopyAgreeement.Visible = true;
            }
            if(user.CanUpdate && !Page.IsPostBack)
            {
                rtsCustomerOptions.Tabs.Add(new RadTab() { Text = "Edit Customer", TabIndex = 2 });
                //Check for Texas Users Only
                if (customer.Center.CoopDBAccess)
                {
                    rtsCustomerOptions.Tabs.Add(new RadTab() { Text = "Coop Funding", TabIndex = 3 });
                }
                
            }
            //Set the search box to be a blank list
            rsbCoopFunding.DataSource = new List<string>();
            //Initialize
            if (!IsPostBack)
            {
                initializeTabStrip(rtsCustomerOptions, rmpCustomerOptions);
                PageDataBind();
            }
        }
        private void initializeTabStrip(RadTabStrip rts, RadMultiPage rmp)
        {
            string selected = Request.QueryString["selected"];
            if(!String.IsNullOrEmpty(selected))
            {
                try {
                    //Convert it to an integer
                    var selectedIndex = Convert.ToInt32(selected);
                    //Check to see if it is in an appropriate range for the page
                    if (selectedIndex >= 0 && selectedIndex <= rts.Tabs.Count() - 1)
                    {
                        rts.SelectedIndex = selectedIndex;
                        rmp.SelectedIndex = selectedIndex;
                    }else
                    {
                        rts.SelectedIndex = 0; rmp.SelectedIndex = 0;
                    }
                }
                catch (Exception ex) { rts.SelectedIndex = 0; rmp.SelectedIndex = 0; }
            }else
            {
                rts.SelectedIndex = 0;
                rmp.SelectedIndex = 0;
            }
        }
        #endregion

        #region Tab Strip
        protected void rtsCustomerOptions_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            rmpCustomerOptions.SelectedIndex = rtsCustomerOptions.SelectedTab.TabIndex;
            PageDataBind();
        }
        private void PageDataBind()
        {
            switch(rmpCustomerOptions.SelectedIndex)
            {
                case 0:
                    rgAgreements.Rebind();
                    break;
                case 1:
                    rgContacts.Rebind();
                    break;
                case 2:
                    RebindCustomerEditInfo();
                    break;
                case 3:
                    rgCoopFunding.Rebind();
                    break;
            }
        }
        private void RebindCustomerEditInfo()
        {
            imgCustomer.ImageUrl = String.Format("http://sifta.water.usgs.gov/Services/REST/Customer/CustomerIcon.ashx?CustomerID={0}", customer.CustomerID);
            rtbCustomerCd.Text = customer.Code;
            rcbAgreementType.DataBind();
            rcbAgreementType.SelectedValue = customer.CustomerAgreementTypeID.ToString();
            rntbCustomerNo.Value = customer.Number;
            rtbCustomerName.Text = customer.Name;
            rtbCustomerAbbrev.Text = customer.Abbreviation;
            rtbCustomerUrl.Text = customer.URL;
            rtbRemarks.Text = customer.Remarks;
            rtbCustomerTin.Text = customer.TaxIdentificationNumber;
        }
        #endregion

        #region Agreements
        protected void cbCopyAgreement_CheckedChanged(object sender, EventArgs e)
        {
            rgAgreements.Columns.FindByUniqueName("copyAgreement").Visible = cbCopyAgreement.Checked;
            rgAgreements.Rebind();
        }
        protected void rgAgreements_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if(e.CommandName == "Copy")
            {
                GridEditableItem editedItem = (GridEditableItem)e.Item;
                int AgreementID = Convert.ToInt32(editedItem.GetDataKeyValue("AgreementID"));
                var agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                CopyAgreement(agreement);
            }
        }
        /// <summary>
        /// Called to copy an agreement 1 fiscal year ahead
        /// </summary>
        /// <param name="agreement">The agreement to be copied</param>
        protected void CopyAgreement(Agreement agreement)
        {
            //Add metrics
            var metric = new MetricHandler(customer.OrgCode, customer.CustomerID, null, MetricType.RecordCopied, "Agreement", String.Format("Copied AgreemendID = {0}", agreement.AgreementID));
            metric.SubmitChanges();
            //Grabs Mod 0 for the Agreement (the original agreement information is what we are copying over)
            var mod = agreement.AgreementMods.FirstOrDefault(p => p.Number == 0);
            //Grabs the list of Site funding for the original mod
            var siteFunding = mod.FundingSites.ToList();
            //Grab all coop funding for mod 0;
            var coopFunding = mod.CooperativeFundings.ToList();
            //Grabs the list of studies funding for the original mod
            var studiesFunding = mod.FundingStudies.ToList();
            //Create a new mod
            var copyMod = new AgreementMod();
            //Create a new Agreement and assign it values from the old one
            var copy = new Agreement()
            {
                BillingCycleFrequency = agreement.BillingCycleFrequency,
                PurchaseOrderNumber = agreement.PurchaseOrderNumber + "_Copy",
                FundsType = agreement.FundsType,
                Remarks = agreement.Remarks,
                CustomerBillingContact = agreement.CustomerBillingContact,
                CustomerTechnicalContact = agreement.CustomerTechnicalContact,
                USGSBillingContact = agreement.USGSBillingContact,
                USGSTechnicalContact = agreement.USGSTechnicalContact,
                CreatedBy = user.ID,
                CreatedDate = DateTime.Now,
                ModifiedBy = user.ID,
                ModifiedDate = DateTime.Now
            };
            //The Start date needs to be pushed up a year if it isn't null
            if(mod.StartDate != null)
            {
                //Set the start date for the agreement to 1 year after the previous agreements start date
                copy.StartDate = Convert.ToDateTime(mod.StartDate).AddYears(1);
            }
            else
            {
                copy.StartDate = null;
            }
            //The End date needs to be pushed up a year if it isn't null
            if (mod.EndDate != null)
            {
                //Set the end date for the agreement to 1 year after the previous agreements end date
                copy.EndDate = Convert.ToDateTime(mod.EndDate).AddYears(1);
            }
            else
            {
                copy.EndDate = null;
            }
            //Set the Mods Start and End Date to be the one from the copied Agreement
            copyMod.StartDate = copy.StartDate;
            copyMod.EndDate = copy.EndDate;

            //Assign values from old mod to new mod
            copyMod.FundingUSGSCMF = mod.FundingUSGSCMF;
            copyMod.FundingCustomer = mod.FundingCustomer;
            copyMod.FundingOther = mod.FundingOther;
            copyMod.FundingOtherReason = mod.FundingOtherReason;
            copyMod.Number = 0;
            copyMod.Remarks = mod.Remarks;
            copyMod.CreatedBy = copyMod.ModifiedBy = user.ID;
            copyMod.CreatedDate = copyMod.ModifiedDate = DateTime.Now;

            //Add Site Funding to copy mod
            foreach(var siteF in siteFunding)
            {
                copyMod.FundingSites.Add(new FundingSite()
                {
                    AgencyCode = siteF.AgencyCode,
                    CollectionCodeID = siteF.CollectionCodeID,
                    DifficultyFactor = siteF.DifficultyFactor,
                    DifficultyFactorReason = siteF.DifficultyFactorReason,
                    FundingCustomer = siteF.FundingCustomer,
                    FundingUSGSCMF = siteF.FundingUSGSCMF,
                    CollectionUnits = siteF.CollectionUnits,
                    SiteNumber = siteF.SiteNumber,
                    Remarks = siteF.Remarks,
                    CreatedBy = user.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = user.ID,
                    ModifiedDate = DateTime.Now
                });
            }
            //Add Studies Funding to the copyMod
            foreach(var studiesF in studiesFunding)
            {
                copyMod.FundingStudies.Add(new FundingStudy()
                {
                    BasisProjectNumber = studiesF.BasisProjectNumber,
                    ResearchCodeID = studiesF.ResearchCodeID,
                    Units = studiesF.Units,
                    Remarks = studiesF.Remarks,
                    FundingCustomer = studiesF.FundingCustomer,
                    FundingUSGSCMF = studiesF.FundingUSGSCMF,
                    CreatedBy = user.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = user.ID,
                    ModifiedDate = DateTime.Now
                });
            }
            
            //Add Copied Mod to the copied agreement
            copy.AgreementMods.Add(copyMod);
            //Add copied agreement to the customer
            customer.Agreements.Add(copy);
            //Submit changes to the database
            siftaDB.SubmitChanges();
            //Add the cooperative funding to the new agreement mod
            foreach (var cfunding in coopFunding)
            {
                copyMod.CooperativeFundings.Add(new CooperativeFunding()
                {
                    AccountNumber = cfunding.AccountNumber,
                    AgreementID = copy.AgreementID,
                    CreatedBy = user.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = user.ID,
                    ModifiedDate = DateTime.Now,
                    Remarks = cfunding.Remarks,
                    Status = "LOW",
                    ModNumber = cfunding.ModNumber,
                    FiscalYear = Convert.ToInt32(cfunding.FiscalYear) + 1,
                    FundingCustomer = cfunding.FundingCustomer,
                    FundingUSGSCMF = cfunding.FundingUSGSCMF
                });
            }
            siftaDB.SubmitChanges();
            //Update Records with Unique Record ID's
            RecordIdentifiers.UpdateRecords();
            //Rebind the agreement grid
            rgAgreements.Rebind();
        }
        protected void rcbAgreements_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgAgreements.Rebind();
        }
        protected void rgAgreements_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            switch (rcbAgreements.SelectedValue)
            {
                case "Active":
                    rgAgreements.DataSource = siftaDB.vAgreementInformations.Where(p => p.CustomerID == customer.CustomerID && p.EndDate > DateTime.Now).OrderByDescending(p => p.EndDate);
                    break;
                case "Sites":
                    var agreements = siftaDB.vAgreementInformations.Where(p => p.CustomerID == customer.CustomerID).OrderByDescending(p => p.EndDate).ToList();
                    var agreementsWithSites = new List<vAgreementInformation>();
                    foreach (var agreement in agreements)
                    {
                        if (siftaDB.FundingSites.Where(s => s.AgreementMod.AgreementID == agreement.AgreementID).Count() > 0)
                        {
                            agreementsWithSites.Add(agreement);
                        }
                    }
                    rgAgreements.DataSource = agreementsWithSites;
                    break;
                case "Recent":
                    rgAgreements.DataSource = siftaDB.vAgreementInformations.Where(p => p.CustomerID == customer.CustomerID && p.EndDate >= DateTime.Now.AddYears(-1)).OrderByDescending(p => p.EndDate).ToList();
                    break;
                default:
                    rgAgreements.DataSource = siftaDB.vAgreementInformations.Where(p => p.CustomerID == customer.CustomerID).OrderByDescending(p => p.EndDate);
                    break;
            }
            //Set Permissions
            if(user.CanInsert)
            {
                rgAgreements.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgAgreements.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = true;
            }
            if(user.CanUpdate)
            {
                rgAgreements.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
            }
        }
        /// <summary>
            /// Called when an Agreement is being added
            /// </summary>
            /// <param name="sender">The grid that is sending the insert command</param>
            /// <param name="e">The Command Event Arguments</param>
        protected void rgAgreements_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Add metrics
            var metric = new MetricHandler(customer.OrgCode, customer.CustomerID, null, MetricType.RecordAdded, "Agreement", "Agreement Added");
            metric.SubmitChanges();
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Create a new Agreement object
            var agreement = new Agreement();
            //Create a new mod object
            var mod = new AgreementMod();
            //Assign the values from the user control
            GrabValuesFromUserControl(userControl, ref agreement, ref mod);
            //Set Agreement End date equal to that from the mod 0
            agreement.EndDate = mod.EndDate;
            //Add the mod to the agreement
            agreement.AgreementMods.Add(mod);
            //Add the Agreement to the Customer
            customer.Agreements.Add(agreement);
            //Add the agreement to the agreements table
            siftaDB.Agreements.InsertOnSubmit(agreement);
            //Submit changes to the database
            siftaDB.SubmitChanges();
            //Update Records with Unique Record ID's
            RecordIdentifiers.UpdateRecords();
        }

        /// <summary>
        /// Called when an Agreements is edited
        /// </summary>
        /// <param name="sender">The grid that is sending the update command</param>
        /// <param name="e">The Command Event Arguments</param>
        protected void rgAgreements_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the AgreementID of the Agreement that was edited
            var AgreementID = Convert.ToInt32(editedItem.GetDataKeyValue("AgreementID").ToString());
            //Add metrics
            var metric = new MetricHandler(customer.OrgCode, customer.CustomerID, null, MetricType.RecordUpdate, "Agreement", String.Format("Updated AgreementID = {0}", AgreementID));
            metric.SubmitChanges();
            //Grab the agreement from the database with the matching ID
            var agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
            //Grab Mod 0 associated with that agreement
            var mod = agreement.AgreementMods.FirstOrDefault(p => p.Number == 0);
            //Assign the values from the user control
            GrabValuesFromUserControl(userControl, ref agreement, ref mod);
            //Grab all mods that don't have a null end date
            var mods = agreement.AgreementMods.Where(p => p.EndDate != null).OrderByDescending(p=>p.Number).ToList();
            //If a mod meets the criteria of having an end date grab the first one (highest mod number) and assign its end date to the agreement.
            if(mods.Count()!= 0)
            {
                agreement.EndDate = mods.FirstOrDefault().EndDate;
            }
            //Submit Changes to the database
            siftaDB.SubmitChanges();
        }

        /// <summary>
        /// Takes a user control and maps the values from it to an Agreement and Mod object.
        /// The user control being used is the Agreement.ascx. 
        /// Found at Controls/Agreement.ascx
        /// </summary>
        /// <param name="uc">The User Control for editing agreement information</param>
        /// <param name="a">The Agreement Object</param>
        /// <param name="m">The Agreement Mod Object</param>
        private void GrabValuesFromUserControl(UserControl uc, ref Agreement a, ref AgreementMod m)
        {
            #region User Controls
            //Grab the controls from the user controls
            var rtbPurchaseOrderNumber = (uc.FindControl("rtbPurchaseOrderNumber") as RadTextBox);
            var rtbMPC = (uc.FindControl("rtbMPC") as RadTextBox);
            var rtbSalesDocument = (uc.FindControl("rtbSalesDocument") as RadTextBox);
            var rdpStartDate = (uc.FindControl("rdpStartDate") as RadDatePicker);
            var rdpEndDate = (uc.FindControl("rdpEndDate") as RadDatePicker);
            var rdpCustomerSigned = (uc.FindControl("rdpCustomerSigned") as RadDatePicker);
            var rdpUSGSSigned = (uc.FindControl("rdpUSGSSigned") as RadDatePicker);
            var rcbFundsType = (uc.FindControl("rcbFundsType") as RadComboBox);
            var rcbBillingCycle = (uc.FindControl("rcbBillingCycle") as RadComboBox);
            var rntbCustomerFunding = (uc.FindControl("rntbCustomerFunding") as RadNumericTextBox);
            var rntbOtherFunding = (uc.FindControl("rntbOtherFunding") as RadNumericTextBox);
            var rntbUSGSFunding = (uc.FindControl("rntbUSGSFunding") as RadNumericTextBox);
            var rtbOtherFundingReason = (uc.FindControl("rtbOtherFundingReason") as RadTextBox);
            //Removed 11/4/2014 for to remove tags var racbTags = (uc.FindControl("racbTags") as RadAutoCompleteBox);
            #endregion

            #region Assign Values
            //Agreement Info
            a.BillingCycleFrequency = rcbBillingCycle.SelectedValue;
            a.FundsType = rcbFundsType.SelectedValue;
            a.MatchPairCode = rtbMPC.Text;
            a.SalesDocument = rtbSalesDocument.Text;
            a.PurchaseOrderNumber = rtbPurchaseOrderNumber.Text;
            //Mod Info
            a.StartDate = m.StartDate = rdpStartDate.SelectedDate;
            a.EndDate = m.EndDate = rdpEndDate.SelectedDate;
            //Check to see if the usgs sign date has changed and it isn't null
            if(m.SignUSGSDate != rdpUSGSSigned.SelectedDate && rdpUSGSSigned.SelectedDate != null)
            {
                //Create USGS Signed Log
                var log = new AgreementModLog()
                {
                    AgreementLogTypeID = 6,
                    CreatedBy = user.ID,
                    ModifiedBy = user.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    LoggedDate = Convert.ToDateTime(rdpUSGSSigned.SelectedDate),
                    Remarks = "Auto generated from changing or adding a USGS Sign date to the agreement or agreement mod."
                };
                m.AgreementModLogs.Add(log);
            }
            m.SignUSGSDate = rdpUSGSSigned.SelectedDate;
            m.SignCustomerDate = rdpCustomerSigned.SelectedDate;
            m.FundingUSGSCMF = rntbUSGSFunding.Value;
            m.FundingCustomer = rntbCustomerFunding.Value;
            m.FundingOther = rntbOtherFunding.Value;
            a.ModifiedBy = m.ModifiedBy = user.ID;
            if (m.CreatedDate == null || a.CreatedDate == null)
            {
                //Set all of the created dates to be the current one
                a.CreatedDate = a.ModifiedDate = m.CreatedDate = m.ModifiedDate = DateTime.Now;
            }
            else
            {
                //Set modified fields for tracking purposes
                a.ModifiedDate = m.ModifiedDate = DateTime.Now;
                a.ModifiedBy = m.ModifiedBy = user.ID;
            }
            #endregion
        }
        #endregion

        #region Contacts
        protected void rgContacts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgContacts.DataSource = siftaDB.CustomerContacts.Where(p => p.CustomerID == customer.CustomerID).OrderBy(p=>p.LastName).ToList();
            if(user.CanInsert)
            {
                rgContacts.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgContacts.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = true;
            }
            else
            {
                rgContacts.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
            if(user.CanUpdate)
            {
                rgContacts.Columns.FindByUniqueName("Edit").Visible = true;
            }
            else
            {
                rgContacts.Columns.FindByUniqueName("Edit").Visible = false;
            }
            if(user.CanDelete)
            {
                rgContacts.Columns.FindByUniqueName("DeleteContact").Visible = true;
            }
        }
        protected void rgContacts_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            int CustomerContactID = Convert.ToInt32(dataItem.GetDataKeyValue("CustomerContactID"));
            e.DetailTableView.DataSource = siftaDB.CustomerContactAddresses.Where(p => p.CustomerContactID == CustomerContactID);
            //Sets ability to edit
            e.DetailTableView.Columns.FindByUniqueName("EditAddress").Visible = user.CanUpdate;
            //Sets ability to insert
            if (user.CanInsert)
            {
                e.DetailTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                e.DetailTableView.CommandItemSettings.ShowAddNewRecordButton = true;
            }
            //Set ability to delete
            e.DetailTableView.Columns.FindByUniqueName("DeleteAddress").Visible = user.CanDelete;
        }
        protected void rgContacts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //grab the User control
            var uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            switch (e.Item.OwnerTableView.Name)
            {
                case "Contact":
                    //Grab the controls from the user control
                    #region Grab Contact Controls
                    var rcbSalutation = uc.FindControl("rcbSalutation") as RadComboBox;
                    var rtbFirstName = uc.FindControl("rtbFirstName") as RadTextBox;
                    var rtbMiddleInitial = uc.FindControl("rtbMiddleInitial") as RadTextBox;
                    var rtbLastName = uc.FindControl("rtbLastName") as RadTextBox;
                    var rtbTitle = uc.FindControl("rtbTitle") as RadTextBox;
                    var rtbPhoneWork = uc.FindControl("rtbPhoneWork") as RadTextBox;
                    var rtbPhoneMobile = uc.FindControl("rtbPhoneMobile") as RadTextBox;
                    var rtbPhoneFax = uc.FindControl("rtbPhoneFax") as RadTextBox;
                    var rtbEmail = uc.FindControl("rtbEmail") as RadTextBox;
                    var rtbRemarks = uc.FindControl("rtbRemarks") as RadTextBox;
                    #endregion
                    //Create New Contact
                    var customerContact = new CustomerContact()
                    {
                        Salutation = rcbSalutation.SelectedValue,
                        FirstName = rtbFirstName.Text,
                        MiddleName = rtbMiddleInitial.Text,
                        LastName = rtbLastName.Text,
                        Title = rtbTitle.Text,
                        PhoneWork = rtbPhoneWork.Text.ToPhoneFormat(),
                        PhoneMobile = rtbPhoneMobile.Text.ToPhoneFormat(),
                        PhoneFax = rtbPhoneFax.Text.ToPhoneFormat(),
                        Email = rtbEmail.Text,
                        Remarks = rtbRemarks.Text,
                        CreatedBy = user.ID,
                        ModifiedBy = user.ID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    //Add Contact to the customer
                    customer.CustomerContacts.Add(customerContact);
                    siftaDB.SubmitChanges();
                    Session["NewContactID"] = customerContact.CustomerContactID;
                    break;
                case "Address":
                    //Grab the contact the address is being added to
                    var contact = (GridDataItem)e.Item.OwnerTableView.ParentItem;
                    //Grab the controls from the user control
                    #region Grab Address Controls
                    var rcbType = uc.FindControl("rcbAddressType") as RadComboBox;
                    var rtbStreetOne = uc.FindControl("rtbStreetOne") as RadTextBox;
                    var rtbStreetTwo = uc.FindControl("rtbStreetTwo") as RadTextBox;
                    var rtbCity = uc.FindControl("rtbCity") as RadTextBox;
                    var rtbState = uc.FindControl("rtbState") as RadTextBox;
                    var rtbZipCode = uc.FindControl("rtbZipCode") as RadTextBox;
                    #endregion
                    //Create New Address with the values from the user control
                    var address = new CustomerContactAddress()
                    {
                        //Grabs the customer ID from the address tables parent
                        CustomerContactID = Convert.ToInt32(contact.OwnerTableView.DataKeyValues[contact.ItemIndex]["CustomerContactID"]),
                        Type = rcbType.SelectedValue,
                        StreetOne = rtbStreetOne.Text,
                        StreetTwo = rtbStreetTwo.Text,
                        City = rtbCity.Text,
                        State = rtbState.Text,
                        ZipCode = rtbZipCode.Text
                    };
                    //Add new address the Customer Contact Address table
                    siftaDB.CustomerContactAddresses.InsertOnSubmit(address);
                    //Submit Changes to the Database
                    siftaDB.SubmitChanges();
                    break;
            }
        }
        protected void rgContacts_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //grab the User control
            var uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the Edited Item
            GridEditableItem editedItem = (GridEditableItem)e.Item;
            switch (e.Item.OwnerTableView.Name)
            {
                case "Contact":
                    //Grab the controls from the user control
                    #region Grab Contact Controls
                    var rcbSalutation = uc.FindControl("rcbSalutation") as RadComboBox;
                    var rtbFirstName = uc.FindControl("rtbFirstName") as RadTextBox;
                    var rtbMiddleInitial = uc.FindControl("rtbMiddleInitial") as RadTextBox;
                    var rtbLastName = uc.FindControl("rtbLastName") as RadTextBox;
                    var rtbTitle = uc.FindControl("rtbTitle") as RadTextBox;
                    var rtbPhoneWork = uc.FindControl("rtbPhoneWork") as RadTextBox;
                    var rtbPhoneMobile = uc.FindControl("rtbPhoneMobile") as RadTextBox;
                    var rtbPhoneFax = uc.FindControl("rtbPhoneFax") as RadTextBox;
                    var rtbEmail = uc.FindControl("rtbEmail") as RadTextBox;
                    var rtbRemarks = uc.FindControl("rtbRemarks") as RadTextBox;
                    #endregion
                    //Grab the ID of the contact being edited
                    var contactID = Convert.ToInt32(editedItem.GetDataKeyValue("CustomerContactID"));
                    //Grab the contact from the database
                    var contact = siftaDB.CustomerContacts.FirstOrDefault(p => p.CustomerContactID == contactID);
                    //Set Values
                    contact.Salutation = rcbSalutation.SelectedValue;
                    contact.FirstName = rtbFirstName.Text;
                    contact.MiddleName = rtbMiddleInitial.Text;
                    contact.LastName = rtbLastName.Text;
                    contact.Title = rtbTitle.Text;
                    contact.PhoneWork = rtbPhoneWork.Text.ToPhoneFormat();
                    contact.PhoneMobile = rtbPhoneMobile.Text.ToPhoneFormat();
                    contact.PhoneFax = rtbPhoneFax.Text.ToPhoneFormat();
                    contact.Email = rtbEmail.Text;
                    contact.Remarks = rtbRemarks.Text;
                    contact.ModifiedBy = user.ID;
                    contact.ModifiedDate = DateTime.Now;
                    //Submit Changes
                    siftaDB.SubmitChanges();
                    break;
                case "Address":
                    //Grab the controls from the user control
                    #region Grab Address Controls
                    var rcbType = uc.FindControl("rcbAddressType") as RadComboBox;
                    var rtbStreetOne = uc.FindControl("rtbStreetOne") as RadTextBox;
                    var rtbStreetTwo = uc.FindControl("rtbStreetTwo") as RadTextBox;
                    var rtbCity = uc.FindControl("rtbCity") as RadTextBox;
                    var rtbState = uc.FindControl("rtbState") as RadTextBox;
                    var rtbZipCode = uc.FindControl("rtbZipCode") as RadTextBox;
                    #endregion
                    //Grab the ID of the Address being edited
                    var addressID = Convert.ToInt32(editedItem.GetDataKeyValue("CustomerContactAddressID"));
                    //Grab the address from the databse
                    var address = siftaDB.CustomerContactAddresses.FirstOrDefault(p => p.CustomerContactAddressID == addressID);
                    address.Type = rcbType.SelectedValue;
                    address.StreetOne = rtbStreetOne.Text;
                    address.StreetTwo = rtbStreetTwo.Text;
                    address.City = rtbCity.Text;
                    address.State = rtbState.Text;
                    address.ZipCode = rtbZipCode.Text;
                    address.ModifiedBy = user.ID;
                    address.ModifiedDate = DateTime.Now;
                    //Submit Changes
                    siftaDB.SubmitChanges();
                    break;
            }
        }
        protected void rgContacts_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.Item.OwnerTableView.Name)
            {
                case "Contact":
                    var contactID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerContactID"];
                    RemoveContact(siftaDB.CustomerContacts.FirstOrDefault(p => p.CustomerContactID == contactID));
                    siftaDB.SubmitChanges();
                    break;
                case "Address":
                    var addressID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerContactAddressID"];
                    RemoveAddress(siftaDB.CustomerContactAddresses.FirstOrDefault(p => p.CustomerContactAddressID == addressID));
                    siftaDB.SubmitChanges();
                    break;
            }
        }
        /// <summary>
        /// Removes a Contact from the database. 
        /// Removes all Addresses and agreements that those addresses are tied to
        /// </summary>
        /// <param name="ContactID">The ID of the contact being removed</param>
        private void RemoveContact(CustomerContact contact)
        {
            //Foreach address call the Remove Address Function to delete it
            foreach (var address in contact.CustomerContactAddresses)
            {
                //Removes the address
                RemoveAddress(address);
            }
            //Delete Contact
            siftaDB.CustomerContacts.DeleteOnSubmit(contact);
        }
        private void RemoveAddress(CustomerContactAddress address)
        {
            //Grabs all the agreements that this address is assigned to as a technical contact
            var tContacts = siftaDB.Agreements.Where(p => p.CustomerTechnicalContact == address.CustomerContactAddressID);
            //Grabs all the agreements that this address is assigned to as a billing contact
            var bContacts = siftaDB.Agreements.Where(p => p.CustomerBillingContact == address.CustomerContactAddressID);
            //Removes them as a technical contact
            foreach (var c in tContacts)
            {
                c.CustomerTechnicalContact = null;
            }
            //Removes them as a billing contact
            foreach (var c in bContacts)
            {
                c.CustomerBillingContact = null;
            }
            //Delete the Address
            siftaDB.CustomerContactAddresses.DeleteOnSubmit(address);
        }
        #endregion

        #region Cooperative Funding
        protected void rgCoopFunding_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCoopFunding.DataSource = siftaDB.spCooperativeFunding(customer.Center.OrgCode, rsbCoopFunding.Text).Where(p => p.CustomerID.ToString() == CustomerID).OrderByDescending(p => p.StartDate);
        }

        protected void rgCoopFunding_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            if (e.DetailTableView.Name == "Accounts")
            {
                var AgreementID = Convert.ToInt32(dataItem.GetDataKeyValue("AgreementID"));
                e.DetailTableView.DataSource = siftaDB.CooperativeFundings.Where(p => p.AgreementID == AgreementID).OrderBy(p=>p.FiscalYear);
            }
        }

        protected void rgCoopFunding_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Add metrics
            var metric = new MetricHandler(customer.OrgCode, customer.CustomerID, null, MetricType.RecordAdded, "Cooperative Funding", "Cooperative Funding Added");
            metric.SubmitChanges();
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
            var AgreementID = Convert.ToInt32(parentItem.GetDataKeyValue("AgreementID"));
            var cf = new CooperativeFunding();
            GrabCooperativeFundingValuesFromForm(ref cf, userControl);
            cf.AgreementID = AgreementID;
            cf.CreatedBy = user.ID;
            cf.CreatedDate = DateTime.Now;
            siftaDB.CooperativeFundings.InsertOnSubmit(cf);
            siftaDB.SubmitChanges();
        }

        protected void rgCoopFunding_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
            GridEditableItem editedItem = (GridEditableItem)e.Item;
            var CooperativeFundingID = Convert.ToInt32(editedItem.GetDataKeyValue("CooperativeFundingID"));
            //Add metrics
            var metric = new MetricHandler(customer.OrgCode, customer.CustomerID, null, MetricType.RecordUpdate, "Cooperative Funding", String.Format("Updated CooperativeFundingID = {0}", CooperativeFundingID));
            metric.SubmitChanges();
            var cf = siftaDB.CooperativeFundings.FirstOrDefault(p => p.CooperativeFundingID == CooperativeFundingID);
            GrabCooperativeFundingValuesFromForm(ref cf, userControl);
            siftaDB.SubmitChanges();
        }
        private void GrabCooperativeFundingValuesFromForm(ref CooperativeFunding cf, UserControl control)
        {
            var rntbFiscalYear = (RadNumericTextBox)control.FindControl("rntbFiscalYear");
            var rcbMod = (RadComboBox)control.FindControl("rcbMod");
            var rcbAccount = (RadComboBox)control.FindControl("rcbAccount");
            var rntbUSGS = (RadNumericTextBox)control.FindControl("rntbUSGS");
            var rntbCooperator = (RadNumericTextBox)control.FindControl("rntbCooperator");
            var rcbStatus = (RadComboBox)control.FindControl("rcbStatus");
            var rtbRemarks = (RadTextBox)control.FindControl("rtbRemarks");

            var mod = siftaDB.AgreementMods.FirstOrDefault(p => p.AgreementModID.ToString() == rcbMod.SelectedValue);
            cf.ModNumber = mod.Number;
            cf.AgreementModID = mod.AgreementModID;
            cf.FiscalYear = Convert.ToInt32(rntbFiscalYear.Value);
            cf.AccountNumber = rcbAccount.Text;
            cf.FundingUSGSCMF = Convert.ToDouble(rntbUSGS.Value);
            cf.FundingCustomer = Convert.ToDouble(rntbCooperator.Value);
            cf.Status = rcbStatus.SelectedValue;
            cf.Remarks = rtbRemarks.Text;
            cf.ModifiedBy = user.ID;
            cf.ModifiedDate = DateTime.Now;
        }
        protected void rbShowAll_Click(object sender, EventArgs e)
        {
            rsbCoopFunding.Text = "";
            rgCoopFunding.Rebind();
        }

        protected void rsbCoopFunding_Search(object sender, SearchBoxEventArgs e)
        {
            rgCoopFunding.Rebind();
        }
        protected void rbViewReport_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("Reports/Center/CoopFunding.aspx?OrgCode={0}", customer.Center.OrgCode).AppendBaseURL());
        }
        #endregion

        #region EditCustomer
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            #region Assign Values
            customer.Code = rtbCustomerCd.Text;
            //If the combo box had a selected value convert it to int and store it as the customer agreement type. If not set the agreementtypeID to null
            if (!String.IsNullOrEmpty(rcbAgreementType.SelectedValue)) customer.CustomerAgreementTypeID = Convert.ToInt32(rcbAgreementType.SelectedValue); else customer.CustomerAgreementTypeID = null;
            //Convert double to long to store in db
            customer.Number = Convert.ToInt64(rntbCustomerNo.Value);
            customer.Name = rtbCustomerName.Text;
            customer.Abbreviation = rtbCustomerAbbrev.Text;
            customer.URL = rtbCustomerUrl.Text;
            customer.Remarks = rtbRemarks.Text;
            customer.TaxIdentificationNumber = rtbCustomerTin.Text;
            customer.ModifiedDate = DateTime.Now;
            customer.ModifiedBy = user.ID;
            #endregion

            #region Customer Image Upload
            //If a file was even uploaded
            if (rauImage.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile file in rauImage.UploadedFiles)
                {
                    var dir = "D:\\siftaroot\\Temp\\";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = dir + file.FileName;
                    if (File.Exists(path))
                    {
                        customer.IconType = file.ContentType;
                        customer.Icon = File.ReadAllBytes(path);
                        File.Delete(path);
                    }
                }
            }
            #endregion
            //Add metrics
            var metric = new MetricHandler(customer.OrgCode, customer.CustomerID, null, MetricType.RecordUpdate, "Customer", String.Format("Updated CustomerID = {0}", CustomerID));
            metric.SubmitChanges();
            siftaDB.SubmitChanges();
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            rtsCustomerOptions.SelectedIndex = 0;
            rmpCustomerOptions.SelectedIndex = 0;
        }
        #endregion

        #region Inline Code
        public String FormatPhone(object o)
        {
            return o.ToPhoneFormat();
        }
        #endregion

        protected void rgContacts_PreRender(object sender, EventArgs e)
        {
            if (Session["NewContactID"] == null) return;
            var CustomerContactID = Session["NewContactID"].ToString();
            if(!string.IsNullOrEmpty(CustomerContactID))
            {
                foreach(GridDataItem item in rgContacts.Items)
                {
                    if(item.GetDataKeyValue("CustomerContactID").ToString() == CustomerContactID)
                    {
                        item.Expanded = true;
                        Session["NewContactID"] = null;
                    }
                }
            }
        }
        
    }
}