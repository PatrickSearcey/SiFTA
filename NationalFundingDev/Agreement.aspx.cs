using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev
{
    public partial class AgreementPage : System.Web.UI.Page
    {
        #region Local Variables
        private int AgreementID;
        private string SalesOrderNumber = "", PurchaseOrderNumber = "";
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public Agreement agreement;
        private User user;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the Agreement Information
            GetAgreement();            
            //Set the User to be a User from this OrgCode for Permissions
            user = new User(agreement.Customer.OrgCode);
            //Set the Session variable Title 
            Session["Title"] = agreement.Customer.Center.Name.Replace(" Water Science Center", " ");
            //Set Customer Logo Source
            imgCustLogo.ImageUrl = String.Format("https://sifta.water.usgs.gov/Services/REST/Customer/CustomerIcon.ashx?CustomerID={0}", agreement.CustomerID);
            if (!IsPostBack) initializeTabStrip(rtsAgreementOptions, rmpAgreementOptions);
            //Set Visibilty for the Coop Funding
            if(user.CanUpdate && agreement.Customer.Center.CoopDBAccess)
            {
                rtsAgreementOptions.Tabs.FindTabByValue("Coop").Visible = true;
                //Set the search box to be a blank list
                rsbCoopFunding.DataSource = new List<string>();
            }
            BindAgreementLog();
        }
        private void GetAgreement()
        {
            //Grab the AgreementID from the url www.url/Agreement.aspx?AgreementID={0}
            var agreementID = Request.QueryString["AgreementID"];
            if (!string.IsNullOrEmpty(agreementID))
            {
                AgreementID = Convert.ToInt32(agreementID);
                //Set agreement to be the one from the Database
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                //If the agreement is not valid send them to the search page
                if (agreement == null) Response.Redirect(String.Format("Search.aspx?Query={0}", AgreementID).AppendBaseURL());
            }
            SalesOrderNumber = Request.QueryString["SalesOrderNumber"];
            if(!String.IsNullOrEmpty(SalesOrderNumber))
            {
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.SalesDocument == SalesOrderNumber);
                //If the agreement is not valid send them to the search page
                if (agreement == null) Response.Redirect(String.Format("Search.aspx?Query={0}", SalesOrderNumber).AppendBaseURL());
            }
            PurchaseOrderNumber = Request.QueryString["PurchaseOrderNumber"];
            if(!String.IsNullOrEmpty(PurchaseOrderNumber))
            {
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.PurchaseOrderNumber == PurchaseOrderNumber);
                //If the agreement is not valid send them to the search page
                if (agreement == null) Response.Redirect(String.Format("Search.aspx?Query={0}", PurchaseOrderNumber).AppendBaseURL());
            }
            
            //If the agreement is not valid send them back to the Default Page
            if (agreement == null) Response.Redirect("Default.aspx".AppendBaseURL());
        }
        private void initializeTabStrip(RadTabStrip rts, RadMultiPage rmp)
        {
            string selected = Request.QueryString["selected"];
            if (!String.IsNullOrEmpty(selected))
            {
                try
                {
                    //Convert it to an integer
                    var selectedIndex = Convert.ToInt32(selected);
                    //Check to see if it is in an appropriate range for the page
                    if (selectedIndex >= 0 && selectedIndex <= rmp.PageViews.Count - 1)
                    {
                        switch(selectedIndex)
                        {
                            case 2:
                                rts.SelectedIndex = 2;
                                rts.SelectedTab.Tabs.FirstOrDefault(p => p.TabIndex == 5).Selected = true;
                                rmp.SelectedIndex = 5;
                                break;
                            case 3:
                                rts.SelectedIndex = 2;
                                rts.SelectedTab.Tabs.FirstOrDefault(p => p.TabIndex == 4).Selected = true;
                                rmp.SelectedIndex = 4;
                                break;
                            case 4:
                                rts.SelectedIndex = 3;
                                rmp.SelectedIndex = 2;
                                break;
                            case 5:
                                rts.SelectedIndex = 4;
                                rmp.SelectedIndex = 3;
                                break;
                            case 6:
                                rts.SelectedIndex = 5;
                                rmp.SelectedIndex = 6;
                                break;
                            default:
                                rts.SelectedIndex = selectedIndex;
                                rmp.SelectedIndex = selectedIndex;
                                break;
                        }
                    }
                    else
                    {
                        rts.SelectedIndex = 0; rmp.SelectedIndex = 0;
                    }
                }
                catch (Exception ex) { rts.SelectedIndex = 0; rmp.SelectedIndex = 0; }
            }
            else
            {
                rts.SelectedIndex = 0;
                rmp.SelectedIndex = 0;
            }
            PageDataBind();
        }
        #endregion

        #region Tab Strip
        protected void rtsAgreementOptions_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            if (e.Tab.TabIndex == 20) Response.Redirect(String.Format("http://nwissdpdasnwra.cr.usgs.gov/cgi-bin/attachment-ca.cgi?AgreementID={0}", agreement.AgreementID));
            if (e.Tab.TabIndex == 21) Response.Redirect(String.Format("http://nwissdpdasnwra.cr.usgs.gov/cgi-bin/attachment.cgi?AgreementID={0}", agreement.AgreementID));
            //If the tab has children 
            if (e.Tab.Tabs.Count > 0)
            {
                //make the first child selected
                e.Tab.Tabs.First().Selected = true;
                //Set the multipage to show the selected childs tabindex
                rmpAgreementOptions.SelectedIndex = e.Tab.Tabs.First().TabIndex;
            }
            else
            {
                //Set the multipage to show the selected Page
                rmpAgreementOptions.SelectedIndex = e.Tab.TabIndex;
            }
            PageDataBind();
        }
        private void PageDataBind()
        {
            switch(rmpAgreementOptions.SelectedIndex)
            {
                //Agreement tab
                case 0:
                    rgAgreements.Rebind();
                    break;
                //Contacts
                case 1:
                    rcbcbContacts.DataBind();
                    rgcbContact.Rebind();
                    rcbctContacts.DataBind();
                    rgctContact.Rebind();
                    rcbubContacts.DataBind();
                    rgubContact.Rebind();
                    rcbutContacts.DataBind();
                    rgutContact.Rebind();
                    break;
                case 2:
                    rgFundedSites.Rebind();
                    break;
                case 3:
                    rgStudiesSupport.Rebind();
                    break;
                //JFA Document Generator
                case 4:
                    rcbDirector.DataSource = siftaDB.CenterDirectors.Where(p => p.OrgCode == agreement.Customer.OrgCode);
                    rcbDirector.DataBind();
                    rcbFinancialReviewer.DataSource = siftaDB.CenterFinancialReviewers.Where(p => p.OrgCode == agreement.Customer.OrgCode);
                    rcbFinancialReviewer.DataBind();
                    rtbDUNS.Text = agreement.Customer.Center.DUNS;
                    rtbProjectNumber.Text = agreement.Customer.Center.ProjectNumber;
                    break;
                //Upload Documents
                case 5:
                    rgAgreementDocuments.Rebind();
                    break;
                //AgreementLog
                case 6:
                    break;
                //Coop
                case 7:
                    break;
                //Overview
                case 8:
                    Response.Redirect(OverviewURL());
                    break;
            }
            
            
        }
        #endregion

        #region Modify Agreement
        protected void rgAgreements_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgAgreements.DataSource = siftaDB.vAgreementModInformations.Where(p => p.AgreementID == agreement.AgreementID).OrderBy(p => p.Number);
            if (user.CanInsert)
            {
                rgAgreements.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgAgreements.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            if (user.CanUpdate)
            {
                rgAgreements.Columns.FindByUniqueName("Edit").Visible = true;
            }
            if (user.CanDelete)
            {
                rgAgreements.Columns.FindByUniqueName("Delete").Visible = true;
            }
        }
        protected void rgAgreements_PreRender(object sender, EventArgs e)
        {
            var highestModNumber = String.Format("Mod {0}", agreement.AgreementMods.OrderByDescending(p => p.Number).FirstOrDefault().Number.ToString());
            foreach(GridDataItem item in rgAgreements.Items)
            {
                item["Delete"].Visible = item["ModName"].Text == highestModNumber;
            }
        }
        protected void rgAgreements_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the AgreementID of the Agreement that was edited
            var AgreementModID = Convert.ToInt32(editedItem.GetDataKeyValue("AgreementModID").ToString());
            //Grab the Mod that was edited
            var mod = agreement.AgreementMods.FirstOrDefault(p => p.AgreementModID == AgreementModID);
            GrabValuesFromUserControl(userControl, ref agreement, ref mod);
            //Submit Changes to the database
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordUpdate, "Agreement Mod", String.Format("Mod {0}", mod.Number));
            metric.SubmitChanges();
            agreement.StartDate = agreement.TrueStartDate();
            agreement.EndDate = agreement.TrueEndDate();
            siftaDB.SubmitChanges();
            Response.Redirect(Request.Url.PathAndQuery);
        }
        protected void rgAgreements_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the Mod that was edited
            var mod = new AgreementMod();
            GrabValuesFromUserControl(userControl, ref agreement, ref mod);
            var highestModNumber = agreement.AgreementMods.OrderByDescending(p => p.Number).FirstOrDefault().Number;
            mod.Number = highestModNumber + 1;
            //Add Mod to teh agreement
            agreement.AgreementMods.Add(mod);
            //Submit Changes to the database
            siftaDB.SubmitChanges();
            agreement.StartDate = agreement.TrueStartDate();
            agreement.EndDate = agreement.TrueEndDate();
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordAdded, "Agreement Mod", "New Mod");
            metric.SubmitChanges();
            //Update Records with Unique Record ID's
            RecordIdentifiers.UpdateRecords();
            siftaDB.SubmitChanges();
        }
        protected void rgAgreements_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Get the AgreementModID from the row the delete command was clicked on
            var AgreementModID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AgreementModID"];
            //Kill the Sites Children
            siftaDB.FundingSites.DeleteAllOnSubmit(siftaDB.FundingSites.Where(p => p.AgreementModID == AgreementModID));
            //Kill the Studies Children
            siftaDB.FundingStudies.DeleteAllOnSubmit(siftaDB.FundingStudies.Where(p => p.AgreementModID == AgreementModID));
            //Kill the Parent
            siftaDB.AgreementMods.DeleteOnSubmit(siftaDB.AgreementMods.FirstOrDefault(p => p.AgreementModID == AgreementModID));
            //Submit Changes to DB
            siftaDB.SubmitChanges();
            //Reorder numbers if necessary
            int idx = 0;
            foreach (var mod in agreement.AgreementMods)
            {
                mod.Number = idx;
                idx++;
            }
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordRemoved, "Agreement Mod", "Mod Deleted");
            metric.SubmitChanges();
            //Submit renumbering of mods change to DB
            siftaDB.SubmitChanges();
            agreement.StartDate = agreement.TrueStartDate();
            agreement.EndDate = agreement.TrueEndDate();
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
            m.StartDate = rdpStartDate.SelectedDate;
            m.EndDate = rdpEndDate.SelectedDate;
            //Check to see if the usgs sign date has changed and it isn't null
            if (m.SignUSGSDate != rdpUSGSSigned.SelectedDate && rdpUSGSSigned.SelectedDate != null)
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
            m.FundingUSGSCMF = Convert.ToDouble(rntbUSGSFunding.Value);
            m.FundingCustomer = Convert.ToDouble(rntbCustomerFunding.Value);
            m.FundingOther = Convert.ToDouble(rntbOtherFunding.Value);
            m.FundingOtherReason = rtbOtherFundingReason.Text;
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
        #region Customer Technical Contact
        protected void rcbctContacts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                agreement.CustomerTechnicalContact = Convert.ToInt32(rcbctContacts.SelectedValue);
                //Submit changes to the DB
                siftaDB.SubmitChanges();
                //Rebind the Technical Contact Grid
                rgctContact.Rebind();
            }
            catch(Exception ex)
            {

            }
        }
        protected void rgctContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Uses the CustomerContactAddressID from the agreement to pull the contact information from the CustomerContacts View 
            rgctContact.DataSource = siftaDB.vCustomerContacts.Where(p => p.CustomerContactAddressID == agreement.CustomerTechnicalContact);
            if (user.CanUpdate && agreement.CustomerTechnicalContact != null) rbctContactRemove.Visible = true; else rbctContactRemove.Visible = false;
        }
        protected void rcbctContacts_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ds = from customerContact in siftaDB.vCustomerContacts
                     where customerContact.CustomerID == agreement.CustomerID
                     orderby customerContact.LastName
                     select new { customerContact.CustomerContactAddressID, FullName = (customerContact.LastName ?? "") + ", " + (customerContact.FirstName ?? "") + " - " + (customerContact.Type ?? "") };
            var words = e.Text.Split(' ');
            var matches = ds.Where(p => p.FullName.Contains(words[0])).ToList();
            for (int idx = 1; idx < words.Count(); idx++)
            {
                matches = matches.Where(p => p.FullName.Contains(words[idx])).ToList();
            }
            foreach (var item in matches)
            {
                rcbctContacts.Items.Add(new RadComboBoxItem() { Text = item.FullName, Value = item.CustomerContactAddressID.ToString() });
            }
        }
        protected void rbctContact_Click(object sender, EventArgs e)
        {
            agreement.CustomerTechnicalContact = null;
            siftaDB.SubmitChanges();
            rcbctContacts.Text = "";
            rgctContact.Rebind();
        }
        #endregion

        #region Customer Billing Contact
        protected void rcbcbContacts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                agreement.CustomerBillingContact = Convert.ToInt32(rcbcbContacts.SelectedValue);
                //Submit changes to the DB
                siftaDB.SubmitChanges();
                //Rebind the Billing Contact Grid
                rgcbContact.Rebind();
            }
            catch(Exception ex)
            {

            }
        }

        protected void rgcbContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgcbContact.DataSource = siftaDB.vCustomerContacts.Where(p => p.CustomerContactAddressID == agreement.CustomerBillingContact);
            if (user.CanUpdate && agreement.CustomerBillingContact != null) rbcbContactRemove.Visible = true; else rbcbContactRemove.Visible = false;
        }
        protected void rcbcbContacts_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ds = from customerContact in siftaDB.vCustomerContacts
                     where customerContact.CustomerID == agreement.CustomerID
                     orderby customerContact.LastName
                     select new { customerContact.CustomerContactAddressID, FullName = (customerContact.LastName ?? "") + ", " + (customerContact.FirstName ?? "") + " - " + (customerContact.Type ?? "") };
            var words = e.Text.Split(' ');
            var matches = ds.Where(p => p.FullName.Contains(words[0])).ToList();
            for (int idx = 1; idx < words.Count(); idx++)
            {
                matches = matches.Where(p => p.FullName.Contains(words[idx])).ToList();
            }
            foreach (var item in matches)
            {
                rcbcbContacts.Items.Add(new RadComboBoxItem() { Text = item.FullName, Value = item.CustomerContactAddressID.ToString() });
            }
        }
        protected void rbcbContact_Click(object sender, EventArgs e)
        {
            agreement.CustomerBillingContact = null;
            siftaDB.SubmitChanges();
            rcbcbContacts.Text = "";
            rgcbContact.Rebind();
        }
        #endregion

        #region USGS Billing Contact
        protected void rcbubContacts_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ds = from employee in siftaDB.Employees
                     orderby employee.LastName
                     select new { employee.EmployeeID, FullName = (employee.LastName ?? "") + ", " + (employee.FirstName ?? "") };
            var words = e.Text.Split(' ');
            var matches = ds.Where(p => p.EmployeeID.Contains(words[0]) || p.FullName.Contains(words[0])).ToList();
            for (int idx = 1; idx < words.Count(); idx++)
            {
                matches = matches.Where(p => p.EmployeeID.Contains(words[idx]) || p.FullName.Contains(words[idx])).ToList();
            }
            foreach (var item in matches)
            {
                //Add a new item to the combo box
                //Justin K Robertson is the text  
                //Employee ID is the value
                //the item is the dataitem
                rcbubContacts.Items.Add(new RadComboBoxItem() { Text = item.FullName, Value = item.EmployeeID });
            }
        }
        protected void rcbubContacts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //If no value is selected set the usgs billing contact to null
            if (rcbubContacts.SelectedValue == "") agreement.USGSBillingContact = null;
            //Set the value to be the EmployeeID from the combobox
            else agreement.USGSBillingContact = rcbubContacts.SelectedValue;
            //Submit Changes to DB
            siftaDB.SubmitChanges();
            //Rebind the USGS Billing Grid
            rgubContact.Rebind();
        }
        protected void rgubContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Set the datasource for the usgs billing grid to be the one matching the employeeid of the agreement
            rgubContact.DataSource = siftaDB.vUSGSContacts.Where(p => p.EmployeeID == agreement.USGSBillingContact);
            if (user.CanUpdate && siftaDB.vUSGSContacts.Where(p => p.EmployeeID == agreement.USGSBillingContact).Count() > 0) rbubContactRemove.Visible = true; else rbubContactRemove.Visible = false;
        }
        protected void rbubContactRemove_Click(object sender, EventArgs e)
        {
            agreement.USGSBillingContact = null;
            siftaDB.SubmitChanges();
            rcbubContacts.Text = "";
            rgubContact.Rebind();
        }
        #endregion

        #region USGS Technical Contact
        protected void rcbutContacts_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ds = from employee in siftaDB.Employees
                     orderby employee.LastName
                     select new { employee.EmployeeID, FullName = (employee.LastName ?? "") + ", " + (employee.FirstName ?? "") };
            var words = e.Text.Split(' ');
            var matches = ds.Where(p => p.EmployeeID.Contains(words[0]) || p.FullName.Contains(words[0])).ToList();
            for (int idx = 1; idx < words.Count(); idx++)
            {
                matches = matches.Where(p => p.EmployeeID.Contains(words[idx]) || p.FullName.Contains(words[idx])).ToList();
            }
            foreach (var item in matches)
            {
                rcbutContacts.Items.Add(new RadComboBoxItem() { Text = item.FullName, Value = item.EmployeeID });
            }
        }
        protected void rcbutContacts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //If no value is selected set the usgs technical contact to null
            if (rcbutContacts.SelectedValue == "") agreement.USGSTechnicalContact = null;
            //Set the value to be the EmployeeID from the combobox
            else agreement.USGSTechnicalContact = rcbutContacts.SelectedValue;
            //Submit Changes to DB
            siftaDB.SubmitChanges();
            //Rebind the USGS Billing Grid
            rgutContact.Rebind();
        }
        protected void rgutContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Set the datasource for the usgs technical grid to be the one matching the employeeid of the agreement
            rgutContact.DataSource = siftaDB.vUSGSContacts.Where(p => p.EmployeeID == agreement.USGSTechnicalContact);
            if (user.CanUpdate && siftaDB.vUSGSContacts.Where(p => p.EmployeeID == agreement.USGSTechnicalContact).Count() > 0) rbutContactRemove.Visible = true; else rbutContactRemove.Visible = false;
        }
        protected void rbutContactRemove_Click(object sender, EventArgs e)
        {
            agreement.USGSTechnicalContact = null;
            siftaDB.SubmitChanges();
            rcbutContacts.Text = "";
            rgutContact.Rebind();
        }
        #endregion

        private void AssignContactEditability()
        {
            if (user.CanInsert || user.CanUpdate)
            {

            }
            else
            {
                rcbcbContacts.Visible = false;
                rcbctContacts.Visible = false;
                rcbubContacts.Visible = false;
                rcbutContacts.Visible = false;
            }
        }
        
        #endregion

        #region Funded Sites
        protected void rgFundedSites_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Grab all Funded Sites for this agreement order by mod number
            rgFundedSites.DataSource = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID == agreement.AgreementID).OrderBy(p => p.SiteNumber);
            //Set Permissions
            if (user.CanInsert)
            {
                rgFundedSites.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            }
            if (user.CanUpdate)
            {
                rgFundedSites.Columns.FindByUniqueName("Edit").Visible = true;
            }
            if (user.CanDelete)
            {
                rgFundedSites.Columns.FindByUniqueName("DeleteSiteFunding").Visible = true;
            }
        }
        protected void rgFundedSites_PreRender(object sender, EventArgs e)
        {
            var headerText = new Queue<string>();
            foreach (GridGroupHeaderItem item in rgFundedSites.MasterTableView.GetItems(GridItemType.GroupHeader))
            {
                var text = item.Cells[1].Text;
                if (!String.IsNullOrEmpty(text)) headerText.Enqueue(text);
            }
            foreach (GridGroupFooterItem item in rgFundedSites.MasterTableView.GetItems(GridItemType.GroupFooter))
            {
                item.Cells[1].Visible = item.Cells[2].Visible = item.Cells[3].Visible = item.Cells[4].Visible = item.Cells[5].Visible = item.Cells[6].Visible = item.Cells[7].Visible = false;
                item.Cells[8].ColumnSpan = 6;
                item.Cells[8].Style.Add("text-align", "right");
                item.Cells[8].Text = String.Format("<b>{0} Total </b>", headerText.Peek());
                headerText.Dequeue();
            }
            foreach (GridFooterItem item in rgFundedSites.MasterTableView.GetItems(GridItemType.Footer))
            {
                item.Cells[1].Visible = item.Cells[2].Visible = item.Cells[3].Visible = item.Cells[4].Visible = item.Cells[5].Visible = item.Cells[6].Visible = item.Cells[7].Visible = false;
                item.Cells[8].ColumnSpan = 6;
                item.Cells[8].Style.Add("text-align", "right");
                item.Cells[8].Text = String.Format("<b>Total </b>");
            }
        }
        protected void rgFundedSites_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Create a New Site Funding Object
            var siteFunding = new FundingSite();
            //Assign Values from User Control
            GrabSiteValuesFromUserControl(userControl, ref siteFunding);
            //Add the Funding Site to the database
            siftaDB.FundingSites.InsertOnSubmit(siteFunding);
            siftaDB.SubmitChanges();
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordAdded, "Site Funding", "New Site Funding");
            metric.SubmitChanges();
            //Update Records with Unique Record ID's
            RecordIdentifiers.UpdateRecords();
        }

        protected void rgFundedSites_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the FundingStudyID of the Study you are editing from the edited item
            int FundingSiteID = Convert.ToInt32(editedItem.GetDataKeyValue("FundingSiteID"));
            //Get the sites to be edited from the database
            var siteFunding = siftaDB.FundingSites.FirstOrDefault(p => p.FundingSiteID == FundingSiteID);
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordUpdate, "Site Funding", String.Format("FundingSiteID = {0}", siteFunding.FundingSiteID));
            metric.SubmitChanges();
            //Assign Values from User Control
            GrabSiteValuesFromUserControl(userControl, ref siteFunding);

            siftaDB.SubmitChanges();
        }

        protected void rgFundedSites_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Get the FundingSiteID of the Site Funding the delete command was clicked for.
            var FundingSiteID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FundingSiteID"];
            //Remove that site from the database
            siftaDB.FundingSites.DeleteOnSubmit(siftaDB.FundingSites.FirstOrDefault(p => p.FundingSiteID == FundingSiteID));
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordRemoved, "Site Funding", "Site Funding Removed");
            metric.SubmitChanges();
            //Submit Changes to DB
            siftaDB.SubmitChanges();
        }
        private void GrabSiteValuesFromUserControl(UserControl userControl, ref FundingSite siteFunding)
        {
            #region Controls
            var rcbMod = (RadComboBox)userControl.FindControl("rcbMod");
            var rtbSiteNumber = (RadTextBox)userControl.FindControl("rtbSiteNumber");
            var rcbCollectionCode = (RadComboBox)userControl.FindControl("rcbCollectionCode");
            var rntbUnits = (RadNumericTextBox)userControl.FindControl("rntbUnits");
            var rntbDifficultyFactor = (RadNumericTextBox)userControl.FindControl("rntbDifficultyFactor");
            var rtbDifficultyFactorReason = (RadTextBox)userControl.FindControl("rtbDifficultyFactorReason");
            var rntbUSGSCMFFunding = (RadNumericTextBox)userControl.FindControl("rntbUSGSCMFFunding");
            var rntbCustomerFunding = (RadNumericTextBox)userControl.FindControl("rntbCustomerFunding");
            var rntbOtherFunding = (RadNumericTextBox)userControl.FindControl("rntbOtherFunding");
            var rtbRemarks = (RadTextBox)userControl.FindControl("rtbRemarks");
            #endregion
            #region Assign Values
            //Add creation date and user if it hasn't been added (new site funding)
            if (siteFunding.CreatedDate == null)
            {
                siteFunding.CreatedDate = DateTime.Now;
                siteFunding.CreatedBy = user.ID;
            }
            //Set the Modified time and by to the current user.
            siteFunding.ModifiedBy = user.ID;
            siteFunding.ModifiedDate = DateTime.Now;
            //Grab the AgreemnetModID from the selected mod value
            siteFunding.SiteNumber = rtbSiteNumber.Text.Trim();
            siteFunding.AgreementModID = Convert.ToInt32(rcbMod.SelectedValue);
            siteFunding.CollectionCodeID = Convert.ToInt32(rcbCollectionCode.SelectedValue);
            siteFunding.CollectionUnits = rntbUnits.Value;
            siteFunding.DifficultyFactor = rntbDifficultyFactor.Value;
            siteFunding.DifficultyFactorReason = rtbDifficultyFactorReason.Text;
            siteFunding.FundingUSGSCMF = rntbUSGSCMFFunding.Value.ToDouble();
            siteFunding.FundingCustomer = rntbCustomerFunding.Value.ToDouble();
            siteFunding.FundingOther = rntbOtherFunding.Value.ToDouble();
            siteFunding.Remarks = rtbRemarks.Text;
            #endregion
        }
        #endregion

        #region Studies / Support
        protected void rgStudiesSupport_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgStudiesSupport.DataSource = siftaDB.vStudiesFundingInformations.Where(p => p.AgreementID == agreement.AgreementID).OrderBy(p => p.Number);
            if (user.CanInsert)
            {
                rgStudiesSupport.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgStudiesSupport.MasterTableView.CommandItemSettings.AddNewRecordText = "Add New Studies/Support Funding";
                rgStudiesSupport.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            if (user.CanUpdate)
            {
                rgStudiesSupport.Columns.FindByUniqueName("Edit").Visible = true;
            }
            if (user.CanDelete)
            {
                rgStudiesSupport.Columns.FindByUniqueName("Delete").Visible = true;
            }
        }

        protected void rgStudiesSupport_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgStudiesSupport_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Create a new Studies Funding
            var study = new FundingStudy();
            //Takes the user control and puts the values from the form into the studies object
            GrabStudiesValuesFromUserControl(userControl, ref study);
            //Adds the study to the DB
            siftaDB.FundingStudies.InsertOnSubmit(study);
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordAdded, "Studies Funding", "Studies Funding Added");
            metric.SubmitChanges();
            //Submits the changes to the database
            siftaDB.SubmitChanges();
            //Update Records with Unique Record ID's
            RecordIdentifiers.UpdateRecords();
        }

        protected void rgStudiesSupport_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the FundingStudyID of the Study you are editing from the edited item
            int FundingStudyID = Convert.ToInt32(editedItem.GetDataKeyValue("FundingStudyID"));
            //grab the study to be edited
            var study = siftaDB.FundingStudies.FirstOrDefault(p => p.FundingStudyID == FundingStudyID);
            //Takes the user control and puts the values from the form into the studies object
            GrabStudiesValuesFromUserControl(userControl, ref study);
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordUpdate, "Studies Funding", String.Format("StudiesFundingID = {0}", study.FundingStudyID));
            metric.SubmitChanges();
            //Submits the changes to the database
            siftaDB.SubmitChanges();
        }
        private void GrabStudiesValuesFromUserControl(UserControl userControl, ref FundingStudy StudiesFunding)
        {
            #region Controls
            var rcbMod = (RadComboBox)userControl.FindControl("rcbMod");
            var rcbType = (RadComboBox)userControl.FindControl("rcbType");
            var rtbBasisProjectNumber = (RadTextBox)userControl.FindControl("rtbBasisProjectNumber");
            var rntbUSGSCMFFunding = (RadNumericTextBox)userControl.FindControl("rntbUSGSCMFFunding");
            var rntbCustomerFunding = (RadNumericTextBox)userControl.FindControl("rntbCustomerFunding");
            var rntbOtherFunding = (RadNumericTextBox)userControl.FindControl("rntbOtherFunding");
            var rtbRemarks = (RadTextBox)userControl.FindControl("rtbRemarks");
            #endregion

            #region Assign Values
            StudiesFunding.AgreementModID = Convert.ToInt32(rcbMod.SelectedValue);
            if (String.IsNullOrEmpty(rcbType.SelectedValue)) StudiesFunding.ResearchCodeID = null; else StudiesFunding.ResearchCodeID = Convert.ToInt32(rcbType.SelectedValue);
            StudiesFunding.FundingUSGSCMF = rntbUSGSCMFFunding.Value.ToDouble();
            StudiesFunding.FundingCustomer = rntbCustomerFunding.Value.ToDouble();
            StudiesFunding.FundingOther = rntbOtherFunding.Value.ToDouble();
            StudiesFunding.BasisProjectNumber = rtbBasisProjectNumber.Text;
            StudiesFunding.Remarks = rtbRemarks.Text;
            if (StudiesFunding.CreatedBy == null || StudiesFunding.CreatedDate == null)
            {
                StudiesFunding.CreatedDate = DateTime.Now;
                StudiesFunding.CreatedBy = user.ID;
            }
            StudiesFunding.ModifiedBy = user.ID;
            StudiesFunding.ModifiedDate = DateTime.Now;
            #endregion
        }
        protected void rgStudiesSupport_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Get the FundingStudyID from the row the delete command was clicked on
            var FundingStudyID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FundingStudyID"];
            //Delete the Study from the table that matches that has that ID
            siftaDB.FundingStudies.DeleteOnSubmit(siftaDB.FundingStudies.FirstOrDefault(p => p.FundingStudyID == FundingStudyID));
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordRemoved, "Studies Funding","Studies Funding Removed");
            metric.SubmitChanges();
            //Submit Changes to the DB
            siftaDB.SubmitChanges();
        }
        #endregion

        #region Documents
        protected void rbDownloadJFADocuments_Click(object sender, EventArgs e)
        {
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.JFADownload, "JFA Download", "JFA Download");
            metric.SubmitChanges();
            Session["QRCodeAgreementID"] = agreement.AgreementID.ToString();
            var url = String.Format("Documents/Download.aspx?Type=JFADocument&a={0}&dr={1}&fr={2}&d={3}&p={4}", agreement.AgreementID, rcbDirector.SelectedValue, rcbFinancialReviewer.SelectedValue, rtbDUNS.Text, rtbProjectNumber.Text);
            Response.Redirect(url);
        }
        protected void rbUploadDocuments_Click(object sender, EventArgs e)
        {
            //\\igskiacwvmgs014\siftaroot\Documents\Agreements\AgreementID\
            var agreementDocumentsPath = FileDirectoryHelper.GetAgreementDirectory(agreement.AgreementID);
            if (!Directory.Exists(agreementDocumentsPath)) Directory.CreateDirectory(agreementDocumentsPath);
            foreach(UploadedFile f in rauFile.UploadedFiles)
            {
                if(cbJFADocument.Checked)
                {
                    f.SaveAs(agreementDocumentsPath + agreement.PurchaseOrderNumber + f.GetExtension(), true);
                    cbJFADocument.Checked = false;
                }
                else
                {
                    f.SaveAs(agreementDocumentsPath + f.GetName(), true);
                }
            }
            rgAgreementDocuments.Rebind();
        }

        protected void rgAgreementDocuments_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var Path = (string)((GridDataItem)e.Item).GetDataKeyValue("Path");
            if (File.Exists(Path)) File.Delete(Path);
        }

        protected void rgAgreementDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            var path = new DirectoryInfo(FileDirectoryHelper.GetAgreementDirectory(agreement.AgreementID));
            if (!path.Exists) path.Create();
            var files = path.GetFiles();
            //Create a Datatable to store the information of the files to be used as a datasource for the grid
            DataTable dt = new DataTable();
            //Name and Path
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("URL", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            dt.Columns["Name"].Caption = "Name";
            dt.Columns["Name"].ColumnName = "Name";

            dt.Columns["URL"].Caption = "URL";
            dt.Columns["URL"].ColumnName = "URL";

            dt.Columns["Path"].Caption = "Path";
            dt.Columns["Path"].ColumnName = "Path";
            //For each file in the folder add a row to the datatable with its name and path
            foreach (var file in files)
            {

                DataRow doc = dt.NewRow();
                doc["Name"] = file.Name;
                doc["URL"] = String.Format("https://sifta.water.usgs.gov/Documents/Agreements/{0}/{1}", agreement.AgreementID, file.Name);
                doc["Path"] = file.FullName;
                dt.Rows.Add(doc);
            }
            //Set that datatable as the datasource for the Agreement Documents Grid on the Documents page. 
            rgAgreementDocuments.DataSource = dt;
            if (user.CanDelete)
            {
                rgAgreementDocuments.Columns.FindByUniqueName("DeleteDocument").Visible = true;
            }
            if (user.CanInsert || user.CanUpdate)
            {
                rauFile.Visible = true; ;
                rbUploadDocuments.Visible = true;
            }
        }
        #endregion

        #region AgreementLog
        private void BindAgreementLog()
        {
            var ctrl = (NationalFundingDev.Controls.Editable.AgreementLogGrid)LoadControl("~/Controls/Editable/AgreementLogGrid.ascx");
            ctrl.Edit = user.CanUpdate;
            ctrl.AddNewRecords = user.CanInsert;
            ctrl.Delete = user.CanDelete;
            ctrl.agreement = agreement;
            phAgreementLog.Controls.Add(ctrl);
        }
        #endregion

        #region Cooperative Funding
        protected void rgCoopFunding_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCoopFunding.DataSource = siftaDB.spCooperativeFunding(agreement.Customer.Center.OrgCode, rsbCoopFunding.Text).Where(p => p.AgreementID == agreement.AgreementID).OrderByDescending(p => p.StartDate);
        }

        protected void rgCoopFunding_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            if (e.DetailTableView.Name == "Accounts")
            {
                var AgreementID = Convert.ToInt32(dataItem.GetDataKeyValue("AgreementID"));
                e.DetailTableView.DataSource = siftaDB.CooperativeFundings.Where(p => p.AgreementID == AgreementID).OrderBy(p => p.FiscalYear);
            }
        }

        protected void rgCoopFunding_InsertCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
            var AgreementID = Convert.ToInt32(parentItem.GetDataKeyValue("AgreementID"));
            var cf = new CooperativeFunding();
            GrabCooperativeFundingValuesFromForm(ref cf, userControl);
            cf.AgreementID = AgreementID;
            cf.CreatedBy = user.ID;
            cf.CreatedDate = DateTime.Now;
            siftaDB.CooperativeFundings.InsertOnSubmit(cf);
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordAdded, "Cooperative Funding", "Cooperative Funding Added");
            metric.SubmitChanges();
            siftaDB.SubmitChanges();
        }

        protected void rgCoopFunding_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
            GridEditableItem editedItem = (GridEditableItem)e.Item;
            var CooperativeFundingID = Convert.ToInt32(editedItem.GetDataKeyValue("CooperativeFundingID"));
            var cf = siftaDB.CooperativeFundings.FirstOrDefault(p => p.CooperativeFundingID == CooperativeFundingID);
            GrabCooperativeFundingValuesFromForm(ref cf, userControl);
            //Add metrics
            var metric = new MetricHandler(agreement.Customer.OrgCode, agreement.CustomerID, agreement.AgreementID, MetricType.RecordUpdate, "Cooperative Funding", String.Format("CooperativeFundingID = {0}", CooperativeFundingID));
            metric.SubmitChanges();
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
            Response.Redirect(String.Format("Reports/Center/CoopFunding.aspx?OrgCode={0}", agreement.Customer.Center.OrgCode).AppendBaseURL());
        }
        #endregion

        #region Inline Code
        public String OverviewURL()
        {
            return String.Format("Reports/Agreement/AgreementReport.aspx?AgreementID={0}", agreement.AgreementID).AppendBaseURL();
        }
        public String GetStationName(object siteNumber, object siteName)
        {
            String name = "";
            if (siteNumber != null) name += siteNumber;
            if (siteName != null) name += " - " + siteName.ToString();
            if (name.Length > FundingExtensions.GridTextLength)
            {
                name = name.Substring(0, FundingExtensions.GridTextLength) + "...";
            }
            return name;
        }
        public String CollectionCodeString(object item)
        {
            if(item == null) return "";
            var CollectionCodeID = Convert.ToInt32(item);
            var collectionCode = siftaDB.lutCollectionCodes.FirstOrDefault(p => p.CollectionCodeID == CollectionCodeID);
            if (collectionCode == null) return "";
            return String.Format("{0} - {1}", collectionCode.Code, collectionCode.Description);
        }
        public String AgreementInfoDateFormat(DateTime? d)
        {
            if (d != null)
            {
                return Convert.ToDateTime(d).ToString("MM/dd/yy");
            }
            return "N/A";
        }
        public DateTime? AgreementEndDate
        {
            get
            {
                var mods = siftaDB.AgreementMods.Where(p => p.AgreementID == agreement.AgreementID && p.EndDate != null).OrderByDescending(p => p.Number).ToList();
                if (mods.Count() != 0)
                {
                    return mods.FirstOrDefault().EndDate;
                }
                else return null;
            }
        }
        public String ModName(object o)
        {
            if (o == null) return "";
            if (o.ToString() == "0") return "Agreement";
            return String.Format("Mod {0}", o);
        }
        public Boolean CanBeDeleted(object o)
        {
            if (o == null) return false;
            if (o.ToString() == "0") return false; else return true;
        }
        public String ModNameFormat(object o)
        {
            if (o == null) return "";
            var mod = o.ToString();
            if (mod == "0") return "Original Agreement"; else return String.Format("Mod {0}", mod);
        }
        /// <summary>
        /// Takes 4 objects of a name and converts them to a well formatted full name.
        /// </summary>
        /// <param name="s">Salutaiton (Mr. Ms.)</param>
        /// <param name="f">First Name</param>
        /// <param name="m">Middle Name</param>
        /// <param name="l">Last Name</param>
        /// <returns></returns>
        public String ContactName(object s, object f, object m, object l)
        {
            var name = "";
            if (!String.IsNullOrEmpty(s.ToString())) name += s.ToString() + " ";
            if (!String.IsNullOrEmpty(f.ToString())) name += f.ToString() + " ";
            if (!String.IsNullOrEmpty(m.ToString())) name += m.ToString() + " ";
            if (!String.IsNullOrEmpty(l.ToString())) name += l.ToString();
            return name;
        }

        public String PhoneFormat(object p)
        {
            if (p == null) return "";
            return p.ToPhoneFormat();
        }
        #endregion


        #region DownloadTemplate

        public class DownloadSite
        {
            public string SiteName { get; set; }
            public string SiteNumber { get; set; }
            public string CollectionCode { get; set; }
            public string CollectionUnits { get; set; }
            public string DifficultyFactor { get; set; }
            public string FundingUSGSCMF { get; set; }
            public string FundingCustomer { get; set; }
            public string Remarks { get; set; }
        }

        /// <summary>
        /// Removing, moving to Documents/AgreementSiteBulkEdit.ashx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void DownloadTemplate(object sender, EventArgs e);

        protected void DeleteEntriesFromDB()
        {
            string agID = Request.QueryString["AgreementID"];
            var modID = siftaDB.AgreementMods.FirstOrDefault(x => x.AgreementID == int.Parse(agID));
            var entries = siftaDB.FundingSites.Where(x => x.AgreementModID == modID.AgreementModID);

            foreach (var entry in entries)
            {
                siftaDB.FundingSites.DeleteOnSubmit(entry);
            }

            try
            {
                siftaDB.SubmitChanges();
            }
            catch (Exception e)
            {
                StatusLabel.Text += e.ToString();
            }
        }

        protected void rbUploadBulkSiteTemplate_Click(object sender, EventArgs e)
        {
            var list = new List<DownloadSite>();
            // Changed it to not depend on a D drive
            var dir = new DirectoryInfo(Context.Server.MapPath("~/Temporary"));
            if (!dir.Exists)
            {
                dir.Create();
            }

            if (rauBulkSiteUpload.UploadedFiles.Count > 0)
            {
                try
                {
                    // unique id to avoid overwritting files
                    var id = Guid.NewGuid().ToString();
                    var path = Path.Combine(dir.FullName, $"{id}.xlsx");

                    rauBulkSiteUpload.UploadedFiles[0].SaveAs(path);

                    StatusLabel.Text = "Upload status: File uploaded!";

                    var file = new FileInfo(path);
                    using (var package = new ExcelPackage(file))
                    {
                        //The actual spread sheets are contained within a work book
                        var workBook = package.Workbook;
                        //Grab the first work sheet in the excel document 
                        var ws = workBook.Worksheets.First();

                        var A1 = ws.Cells["A1"].Value;
                        var testing = ws.Cells.LoadFromText("test");

                        //Select all cells in column
                        var query = (from cell in ws.Cells["f:f"] where cell.Value is double select cell);

                        for (int n = 0; n < query.Count(); n++)
                        {
                            int i = n + 2;
                            StatusLabel.Text += "<br><br>";

                            var temp1 = ws.Cells["A" + i].Value;  // entry.SiteName;          Table: Site
                            var temp2 = ws.Cells["B" + i].Value;  // entry.SiteNumber;        Table: FundingSite
                            var temp3 = ws.Cells["C" + i].Value;  // entry.CollectionCode;    Table: lutCollectionCode
                            var temp4 = ws.Cells["D" + i].Value;  // entry.CollectionUnits;   Table: FundingSite
                            var temp5 = ws.Cells["E" + i].Value;  // entry.DifficultyFactor;  Table: FundingSite
                            var temp6 = ws.Cells["F" + i].Value;  // entry.FundingUSGSCMF;    Table: FundingSite
                            var temp7 = ws.Cells["G" + i].Value;  // entry.FundingCustomer;   Table: FundingSite
                            var temp8 = ws.Cells["H" + i].Value;  // entry.Remarks;           Table: FundingSite

                            list.Add(new DownloadSite
                            {
                                SiteName = temp1?.ToString(),
                                SiteNumber = temp2?.ToString(),
                                CollectionCode = temp3?.ToString(),
                                CollectionUnits = temp4?.ToString(),
                                DifficultyFactor = temp5?.ToString(),
                                FundingUSGSCMF = temp6?.ToString(),
                                FundingCustomer = temp7?.ToString(),
                                Remarks = temp8?.ToString()
                            });
                        }
                    }
                    // Delete Temporary File
                    file.Delete();
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

            DeleteEntriesFromDB();
            InsertEntriesToDB(list);
        }

        protected void InsertEntriesToDB(List<DownloadSite> list)
        {
            string agID = Request.QueryString["AgreementID"];
            var modID = siftaDB.AgreementMods.FirstOrDefault(x => x.AgreementID == int.Parse(agID));

            foreach (var site in list)
            {
                var collections = siftaDB.lutCollectionCodes.FirstOrDefault(x => x.Code == site.CollectionCode);
                double.TryParse(site.CollectionUnits, out double cu);
                double.TryParse(site.CollectionUnits, out double df);
                double.TryParse(site.CollectionUnits, out double fundUSGS);
                double.TryParse(site.CollectionUnits, out double fundCust);
                double total = double.Parse(site.FundingUSGSCMF) + double.Parse(site.FundingCustomer);

                var fs = new FundingSite
                {
                    AgreementModID = modID.AgreementModID,
                    SiteNumber = site.SiteNumber,
                    CollectionCodeID = collections.CollectionCodeID,
                    CollectionUnits = cu,
                    DifficultyFactor = df,
                    FundingUSGSCMF = fundUSGS,
                    FundingCustomer = fundCust,
                    FundingTotal = total,
                    FundingOther = 0,
                    AgencyCode = "USGS",
                    Remarks = site.Remarks
                };

                siftaDB.FundingSites.InsertOnSubmit(fs);
            }

            try
            {
                siftaDB.SubmitChanges();
            }
            catch (Exception e)
            {
                StatusLabel.Text += e.ToString();
            }
        }

        #endregion

    }
}
 