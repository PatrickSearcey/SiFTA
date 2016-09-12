using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace NationalFundingDev
{
    public partial class AgreementOldPage : System.Web.UI.Page
    {
        #region Local Variables
        
        private String AgreementID;
        public Agreement agreement;
        private User user;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private Dictionary<String, String> AgreementLogTypes;
        #endregion

        #region Page Load Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the Agreement ID from the url www.url/Agreement.aspx?AgreementID={0}
            AgreementID = Request.QueryString["AgreementID"];
            //Set agreement to be the one from the Database
            agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID.ToString() == AgreementID);
            //If the Agreement is not valid send them back to the Default Page
            if (agreement == null) Response.Redirect("Default.aspx");
            //Set the User to be a User from this OrgCode for Permissions
            user = new User(agreement.Customer.Center.OrgCode);
            //Set the Session variable Title 
            Session["Title"] = agreement.Customer.Center.Name.Replace(" Water Science Center", " ");
            //Set Customer Logo Source
            imgCustLogo.ImageUrl = String.Format("http://sifta.water.usgs.gov/Services/CustomerIcon?CustomerID={0}", agreement.Customer.CustomerID);
            //\\igskiacwvmgs014\siftaroot\Documents\Agreements\AgreementID\
            var agreementDocumentsPath = String.Format("D:\\siftaroot\\Documents\\Agreements\\{0}", agreement.AgreementID);
            if(!IsPostBack)
            {
                AssignContactEditability();
                //Assign the Directors Datasource to be the Center Directors attached to the agrements Center
                rcbDirector.DataSource = agreement.Customer.Center.CenterDirectors;
                rcbDirector.DataBind();
                //Assign the Financial Reviewer for the center attached to the agreement
                rcbFinancialReviewer.DataSource = agreement.Customer.Center.CenterFinancialReviewers;
                rcbFinancialReviewer.DataBind();
                //Set the DUNS Text for the JFA Document Generator to the default value stored for the Center
                rtbDUNS.Text = agreement.Customer.Center.DUNS;
                //Set the Project Number Text for the JFA Document Generator to the default value stored for the Center
                rtbProjectNumber.Text = agreement.Customer.Center.ProjectNumber;
                //The Path of the Agreement Documents to be uploaded
                //If the directory doesn't exist create it
                if (!Directory.Exists(agreementDocumentsPath)) Directory.CreateDirectory(agreementDocumentsPath);
                if(user.CanUpdate)
                {
                    //Check for Texas Users Only
                    if (agreement.Customer.Center.OrgCode == "GCSJ")
                    {
                        rtsAgreementOptions.Tabs.Add(new RadTab() { Text = "Coop Funding", TabIndex = 6 });
                    }
                    rtsAgreementOptions.Tabs.Add(new RadTab() { Text = "Edit Agreement", TabIndex = 7 });
                }
            }
            //Check to see if they have acess to the Admin Page for this center
            if (user.IsAdmin && !Page.IsPostBack)
            {
                //Grab the Top Navigation Menu from the Master Page
                var menu = (RadMenu)this.Master.FindControl("rmTopNav");
                //Create a new menu Item for the Admin tab
                var adminMenuItem = new RadMenuItem();
                //Set the text for the menu Item to be Center Admin
                adminMenuItem.Text = "Center Admin";
                //Set the Navigate path to be that of hte page with the Org Code for the center they have access to.
                adminMenuItem.NavigateUrl = String.Format("Admin.aspx?OrgCode={0}".AppendBaseURL(), agreement.Customer.Center.OrgCode);
                //Sets the CSS to be the adminMenuItem (floats it to the right)
                adminMenuItem.OuterCssClass = "adminMenuItem";
                //Adds Item to menu
                menu.Items.Add(adminMenuItem);
            }
            //Set the rad uploaders target folder to the agreement document folder
            rauFile.TargetFolder = agreementDocumentsPath;
            //rauFile.TemporaryFolder = agreementDocumentsPath;
            hfMap.Value = GetMapData();
            BindAgreementLogInformation();
            //Set the search box to be a blank list
            rsbCoopFunding.DataSource = new List<string>();
        }
        #endregion

        #region Tab Events
        protected void rtsAgreementOptions_TabClick(object sender, RadTabStripEventArgs e)
        {
            rmpAgreementOptions.SelectedIndex = rtsAgreementOptions.SelectedTab.TabIndex;
            rgAgreementLog.Rebind();
            PageDataRebind();
        }
        /// <summary>
        /// Rebinds all grids and data on page
        /// </summary>
        private void PageDataRebind()
        {
            switch(rtsAgreementOptions.SelectedTab.TabIndex)
            {
                case 0:
                    //Overview
                    rgAgreementFundingOverView.Rebind();
                    rgOverViewDocuments.Rebind();
                    rgcbContactOverView.Rebind();
                    rgctContactOverView.Rebind();
                    rgubContactOverView.Rebind();
                    rgutContactOverView.Rebind();
                    rgStudiesFundingOverView.Rebind();
                    rgSiteFundingOverView.Rebind();
                    break;
                case 1:
                    //Contacts
                    rgctContact.Rebind();
                    rgcbContact.Rebind();
                    rgubContact.Rebind();
                    rgutContact.Rebind();
                    break;
                case 2:
                    //Mods
                    rgMods.Rebind();
                    break;
                case 3:
                    //Funded Sites
                    rgFundedSites.Rebind();
                    break;
                case 4:
                    //Studies/Support
                    rgStudiesSupport.Rebind();
                    break;
                case 5:
                    //Documents
                    rgAgreementDocuments.Rebind();
                    break;
                case 6:
                    //Coop Funding
                    rgCoopFunding.Rebind();
                    break;
                case 7:
                    //Edit Agreement
                    BindAgreementEditInformation();
                    break;
                case 8:
                    BindAgreementLogInformation();
                    break;
            }
        }
        private void BindAgreementLogInformation()
        {
            //if(!IsPostBack)
            //{
            //    foreach(var mod in agreement.AgreementMods)
            //    {
            //        //Add all the mods to the drop down with the number being the text and the ID being the value
            //        rcbModNumberAgreementLog.Items.Add(new RadComboBoxItem() { Text = mod.Number.ToString(), Value = mod.AgreementModID.ToString() });
            //    }
            //    rcbActionAgreementLog.Items.Add(new RadComboBoxItem() { Text = "Select an Action", Value = "" });
            //    foreach(var action in siftaDB.lutAgreementLogTypes)
            //    {
            //        rcbActionAgreementLog.Items.Add(new RadComboBoxItem() { Text = action.Type, Value = action.AgreementLogTypeID.ToString() });
            //    }
            //    rdtpAgreementLogTime.SelectedDate = DateTime.Now;
            //}
            //AgreementLogTypes = siftaDB.lutAgreementLogTypes.ToDictionary(p => p.AgreementLogTypeID.ToString(), p => p.Type);
        }
        private void BindAgreementEditInformation()
        {
            rtbPurchaseOrderNumber.Text = agreement.PurchaseOrderNumber;
            rtbMPC.Text = agreement.MatchPairCode;
            rtbSalesDocument.Text = agreement.SalesDocument;
            rcbFundsType.SelectedValue = agreement.FundsType;
            rcbBillingCycle.SelectedValue = agreement.BillingCycleFrequency;
            //AddItemsToTags(agreement.Tags);
        }
        ///// <summary>
        ///// Takes a string like #WWF#Panda and adds the items as different entries to the Telerik Auto Complete Box.
        ///// </summary>
        ///// <param name="tagString">The Tags String</param>
        //private void AddItemsToTags(string tagString)
        //{
        //    if (!String.IsNullOrEmpty(tagString))
        //    {
        //        var tags = tagString.Split('#');
        //        foreach (var tag in tags)
        //        {
        //            if (!String.IsNullOrEmpty(tag)) racbTags.Entries.Insert(0, new AutoCompleteBoxEntry(tag));
        //        }
        //    }
        //}
        #endregion

        #region Overview
        #region Agreement Information Section
        protected void rapAgreementInfo_Load(object sender, EventArgs e)
        {
            if(rtsAgreementOptions.SelectedIndex == 0)
            {
                BindAgreementInformationSection();
            }
        }
        private void BindAgreementInformationSection()
        {
            var siteFunding = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID == agreement.AgreementID).ToList();
            var studiesFunding = siftaDB.vStudiesFundingInformations.Where(p => p.AgreementID == agreement.AgreementID).ToList();

            ltlAgreementStartDate.Text = AgreementInfoDateFormat(agreement.StartDate);
            ltlAgreementEndDate.Text = AgreementInfoDateFormat(agreement.EndDate);
            ltlUSGSSigned.Text = AgreementInfoDateFormat(agreement.AgreementMods.FirstOrDefault(p => p.Number == 0).SignUSGSDate);
            ltlCustomerSigned.Text = AgreementInfoDateFormat(agreement.AgreementMods.FirstOrDefault(p => p.Number == 0).SignCustomerDate);
            ltlPurchaseOrderNumber.Text = agreement.PurchaseOrderNumber;
            ltlMPC.Text = agreement.MatchPairCode;
            ltlFBMS.Text = agreement.SalesDocument;
            //Financial Overview
            
            var siteFundingUSGSCWP = siteFunding.Sum(p => p.FundingUSGSCWP).ToDouble();
            var siteFundingCustomer = siteFunding.Sum(p => p.FundingCustomer).ToDouble();
            var studiesFundingUSGSCWP = studiesFunding.Sum(p => p.FundingUSGSCWP).ToDouble();
            var studiesFundingCustomer = studiesFunding.Sum(p => p.FundingCustomer).ToDouble();
            var recordedUSGSCWP = agreement.AgreementMods.Sum(p => p.FundingUSGSCWP).ToDouble();
            var recordedCustomer = agreement.AgreementMods.Sum(p => p.FundingCustomer).ToDouble();
            var recordedOther = agreement.AgreementMods.Sum(p => p.FundingOther).ToDouble();
            //Top Portion
            ltlSiteFundingUSGSCWP.Text = siteFundingUSGSCWP.ToDollars();
            ltlSiteFundingCustomer.Text = siteFundingCustomer.ToDollars();
            ltlStudiesFundingUSGSCWP.Text = studiesFundingUSGSCWP.ToDollars();
            ltlStudiesFundingCustomer.Text = studiesFundingCustomer.ToDollars();
            ltlUSGSCWPFundingSum.Text = (siteFundingUSGSCWP + studiesFundingUSGSCWP).ToDollars();
            ltlCustomerFundingSum.Text = (siteFundingCustomer + studiesFundingCustomer).ToDollars();
            //Funding Total Portion
            ltlFundingUSGSCWP.Text = recordedUSGSCWP.ToDollars();
            ltlFundingCustomer.Text = recordedCustomer.ToDollars();
            ltlFundingOther.Text = recordedOther.ToDollars();
            //Difference
            ltlDifferenceUSGS.Text = (recordedUSGSCWP - siteFundingUSGSCWP - studiesFundingUSGSCWP).ToDollars();
            ltlDifferenceCustomer.Text = (recordedCustomer - siteFundingCustomer - studiesFundingCustomer).ToDollars();
            ltlDifferenceOther.Text = recordedOther.ToDollars();
        }
        #endregion

        #region Funding Section
        protected void rgAgreementFundingOverView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgAgreementFundingOverView.DataSource = siftaDB.vAgreementFundingOverviews.Where(p => p.AgreementID == agreement.AgreementID);
        }
        
        protected void rgAgreementFundingOverView_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            int AgreementModID = Convert.ToInt32(dataItem.GetDataKeyValue("AgreementModID"));
            e.DetailTableView.DataSource = siftaDB.vAgreementFundingOverviews.Where(p => p.AgreementModID == AgreementModID);
        }
        #endregion

        #region Studies Funding Section
        protected void rgStudiesFundingOverView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgStudiesFundingOverView.DataSource = siftaDB.vStudiesFundingInformations.Where(p => p.AgreementID == agreement.AgreementID);
        }
        #endregion

        #region Site Funding Section
        protected void rgSiteFundingOverView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgSiteFundingOverView.DataSource = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID == agreement.AgreementID);
        }
        public string NSIPImageURL(object score)
        {
            if (score == null) return "";
            switch (score.ToString())
            {
                case "0":
                    return "";
                case "1":
                    return "http://sifta.water.usgs.gov/NationalFunding/images/NSIPScores/NSIP1.gif";
                case "2":
                    return "http://sifta.water.usgs.gov/NationalFunding/images/NSIPScores/NSIP2.gif";
                case "3":
                    return "http://sifta.water.usgs.gov/NationalFunding/images/NSIPScores/NSIP3.gif";
                case "4":
                    return "http://sifta.water.usgs.gov/NationalFunding/images/NSIPScores/NSIP4.gif";
                default:
                    return "";
            }

        }
        public bool NSIPImageVisible(object score)
        {
            if (score == null) return false;
            if (score.ToString() == "0") return false; else return true;

        }
        #endregion

        #region Site Map
        /// <summary>
        /// Grabs the site funding for a given agrerement and returns the formatted JSON 
        /// </summary>
        /// <returns>Formatted JSON of all the sites</returns>
        private String GetMapData()
        {
            //Grab Sites funded by this agreement
            var siteFunding = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID == agreement.AgreementID);
            //Hold all the sites needing to be mapped
            var sites = new List<Site>();
            //Add all the sites from the funding record to the sites List
            foreach(var siteFundingRecord in siteFunding)
            {
                var site = siftaDB.Sites.FirstOrDefault(p => p.SiteNumber == siteFundingRecord.SiteNumber);
                //Check to see if the site isn't null (The site actually exists)
                if(site != null)
                {
                    //Check to make sure it wasn't already added (Don't add duplicates
                    if(sites.FirstOrDefault(p=>p.SiteNumber == site.SiteNumber) == null)
                    {
                        //Add site to the list of sites
                        sites.Add(site);
                    }
                }
            }
            //No sites were added to be mapped return a blank string
            if (sites.Count == 0) return "";
            //Stores the JSON string
            var json = "[";
            foreach(var site in sites)
            {
                json += site.ToJson() + ",";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }
        #endregion
        #endregion

        #region Contacts
        #region Customer Technical Contact
        protected void rcbctContacts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //If no value is selected set the customer technical contact to null 
            if (rcbctContacts.SelectedValue == "") agreement.CustomerTechnicalContact = null;
            //If it has a selected value bind its value
            else agreement.CustomerTechnicalContact = Convert.ToInt32(rcbctContacts.SelectedValue);
            //Submit changes to the DB
            siftaDB.SubmitChanges();
            //Rebind the Technical Contact Grid
            rgctContact.Rebind();
        }
        protected void rgctContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Set the selected value for the contacts drop down
            rcbctContacts.SelectedValue = agreement.CustomerTechnicalContact.ToString();
            //Uses the CustomerContactAddressID from the agreement to pull the contact information from the CustomerContacts View 
            rgctContact.DataSource = siftaDB.vCustomerContacts.Where(p => p.CustomerContactAddressID == agreement.CustomerTechnicalContact);
            //Sets main page contact to have the same datasource
            rgctContactOverView.DataSource = rgctContact.DataSource;
        }
        #endregion

        #region Customer Billing Contact
        protected void rcbcbContacts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //If no value is selected set the customer billing contact to null 
            if (rcbcbContacts.SelectedValue == "") agreement.CustomerBillingContact = null;
            //If it has a selected value bind its value
            else agreement.CustomerBillingContact = Convert.ToInt32(rcbcbContacts.SelectedValue);
            //Submit changes to the DB
            siftaDB.SubmitChanges();
            //Rebind the Billing Contact Grid
            rgcbContact.Rebind();
        }

        protected void rgcbContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Set the selected value for the contacts drop down
            rcbcbContacts.SelectedValue = agreement.CustomerBillingContact.ToString();
            //Uses the CustomerContactAddressID from the agreement to pull the contact information from the CustomerContacts View 
            rgcbContact.DataSource = siftaDB.vCustomerContacts.Where(p => p.CustomerContactAddressID == agreement.CustomerBillingContact);

            rgcbContactOverView.DataSource = rgcbContact.DataSource;
        }
        #endregion

        #region USGS Billing Contact
        protected void rcbubContacts_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ds = from employee in siftaDB.Employees
                     select new { employee.EmployeeID, FullName = (employee.FirstName ?? "") + " " + (employee.MiddleName ?? "")  + " " + (employee.LastName ?? "") };
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
            //Set the datasource for the usgs billing grid on the overview page to be the same as the one here
            rgubContactOverView.DataSource = rgubContact.DataSource;
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
                     select new { employee.EmployeeID, FullName = (employee.FirstName ?? "") + " " + (employee.MiddleName ?? "") + " " + (employee.LastName ?? "") };
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
            //Set the datasource for the usgs technical grid on the overview to be the one here
            rgutContactOverView.DataSource = rgutContact.DataSource;

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
            if(user.CanInsert || user.CanUpdate)
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
        protected void ldsCustomerContacts_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            //Return all the customer contacts associated with this agreements customer
            e.Result = siftaDB.vCustomerContacts.Where(p => p.CustomerID == agreement.CustomerID);
        }
        #endregion

        #region Mods
        protected void rgMods_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgMods.DataSource = siftaDB.AgreementMods.Where(p => p.AgreementID.ToString() == AgreementID).OrderBy(p=>p.Number);
            if(user.CanInsert)
            {
                rgMods.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgMods.MasterTableView.CommandItemSettings.AddNewRecordText = "Add a Mod";
                rgMods.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            if(user.CanUpdate)
            {
                rgMods.Columns.FindByUniqueName("EditMod").Visible = true;
            }
            if(user.CanDelete)
            {
                rgMods.Columns.FindByUniqueName("DeleteMod").Visible = true;
            }
        }
        protected void rgMods_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                if(item.ItemIndex == 0)
                {
                    item["DeleteMod"].Visible = false;
                }
            }
        }
        protected void rgMods_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Create a new Mod
            var mod = new AgreementMod();
            //Takes the user control and puts the values from the form into the AgrementMod object
            GrabModValuesFromUserControl(userControl, ref mod);
            mod.CreatedDate = DateTime.Now;
            mod.CreatedBy = user.ID;
            mod.Number = agreement.AgreementMods.Max(p => p.Number) + 1;
            //Adds the mod to the AgreementMods
            siftaDB.AgreementMods.InsertOnSubmit(mod);
            //Submits the changes to the database
            siftaDB.SubmitChanges();
            //Assign Agreement ENd Date
            agreement.EndDate = AgreementEndDate;
            siftaDB.SubmitChanges();
        }
        protected void rgMods_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the AgreementModID of the mod you are editing from the edited item
            int AgreementModID = Convert.ToInt32(editedItem.GetDataKeyValue("AgreementModID"));
            //Grabs the mod from the database with the matching ID 
            var mod = siftaDB.AgreementMods.FirstOrDefault(p => p.AgreementModID == AgreementModID);
            //Assigns the values from the user control to the mod
            GrabModValuesFromUserControl(userControl, ref mod);
            //Submits the changes to the database
            siftaDB.SubmitChanges();
            //Assign End Date
            agreement.EndDate = AgreementEndDate;
            //Update Agreement Start Date if it was Mod 0 that was edited
            if(mod.Number == 0)
            {
                agreement.StartDate = mod.StartDate;
            }
            siftaDB.SubmitChanges();
        }
        private void GrabModValuesFromUserControl(UserControl userControl, ref AgreementMod mod)
        {
            #region Controls
            var rdpStartDate = (RadDatePicker)userControl.FindControl("rdpStartDate");
            var rdpEndDate = (RadDatePicker)userControl.FindControl("rdpEndDate");
            var rdpUSGSSigned = (RadDatePicker)userControl.FindControl("rdpUSGSSigned");
            var rdpCustomerSigned = (RadDatePicker)userControl.FindControl("rdpCustomerSigned");
            var rntbFundingUSGSCWP = (RadNumericTextBox)userControl.FindControl("rntbFundingUSGSCWP");
            var rntbFundingCustomer = (RadNumericTextBox)userControl.FindControl("rntbFundingCustomer");
            var rntbFundingOther = (RadNumericTextBox)userControl.FindControl("rntbFundingOther");
            var rtbOtherFundingReason = (RadTextBox)userControl.FindControl("rtbOtherFundingReason");
            #endregion

            #region Assign Values to Mod
            mod.AgreementID = agreement.AgreementID;
            mod.StartDate = rdpStartDate.SelectedDate;
            mod.EndDate = rdpEndDate.SelectedDate;
            mod.SignUSGSDate = rdpUSGSSigned.SelectedDate;
            mod.SignCustomerDate = rdpCustomerSigned.SelectedDate;
            mod.FundingCustomer = rntbFundingCustomer.Value;
            mod.FundingUSGSCWP = rntbFundingUSGSCWP.Value;
            mod.FundingOther = rntbFundingOther.Value;
            mod.FundingOtherReason = rtbOtherFundingReason.Text;
            mod.ModifiedBy = user.ID;
            mod.ModifiedDate = DateTime.Now;
            #endregion
        }
        protected void rgMods_DeleteCommand(object sender, GridCommandEventArgs e)
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
            foreach(var mod in agreement.AgreementMods)
            {
                mod.Number = idx;
                idx++;
            }
            //Assign the Agreements End date
            agreement.EndDate = AgreementEndDate;
            //Submit renumbering of mods change to DB
            siftaDB.SubmitChanges();
        }
        #endregion

        #region Funded Sites
        protected void rgFundedSites_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Grab all Funded Sites for this agreement order by mod number
            rgFundedSites.DataSource = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID.ToString() == AgreementID).OrderBy(p => p.ModNumber);
            //Set Permissions
            if(user.CanInsert)
            {
                rgFundedSites.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgFundedSites.MasterTableView.CommandItemSettings.AddNewRecordText = "Add new Site Funding";
            }
            if (user.CanUpdate)
            {
                rgFundedSites.Columns.FindByUniqueName("Edit").Visible = true;
            }
            if(user.CanDelete)
            {
                rgFundedSites.Columns.FindByUniqueName("DeleteSiteFunding").Visible = true;
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
            var rntbUSGSCWPFunding = (RadNumericTextBox)userControl.FindControl("rntbUSGSCWPFunding");
            var rntbCustomerFunding = (RadNumericTextBox)userControl.FindControl("rntbCustomerFunding");
            var rtbRemarks = (RadTextBox)userControl.FindControl("rtbRemarks");
            #endregion
            #region Assign Values
            //Add creation date and user if it hasn't been added (new site funding)
            if(siteFunding.CreatedDate == null)
            {
                siteFunding.CreatedDate = DateTime.Now;
                siteFunding.CreatedBy = user.ID;
            }
            //Set the Modified time and by to the current user.
            siteFunding.ModifiedBy = user.ID;
            siteFunding.ModifiedDate = DateTime.Now;
            //Grab the AgreemnetModID from the selected mod value
            siteFunding.SiteNumber = rtbSiteNumber.Text;
            siteFunding.AgreementModID = Convert.ToInt32(rcbMod.SelectedValue);
            siteFunding.CollectionCodeID = Convert.ToInt32(rcbCollectionCode.SelectedValue);
            siteFunding.CollectionUnits = rntbUnits.Value;
            siteFunding.DifficultyFactor = rntbDifficultyFactor.Value;
            siteFunding.DifficultyFactorReason = rtbDifficultyFactorReason.Text;
            siteFunding.FundingUSGSCWP = rntbUSGSCWPFunding.Value;
            siteFunding.FundingCustomer = rntbCustomerFunding.Value;
            siteFunding.Remarks = rtbRemarks.Text;
            #endregion
        }
        #endregion

        #region Studies / Support
        protected void rgStudiesSupport_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgStudiesSupport.DataSource = siftaDB.vStudiesFundingInformations.Where(p => p.AgreementID.ToString() == AgreementID).OrderBy(p => p.Number);
            if(user.CanInsert)
            {
                rgStudiesSupport.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgStudiesSupport.MasterTableView.CommandItemSettings.AddNewRecordText = "Add New Studies/Support Funding";
                rgStudiesSupport.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            if(user.CanUpdate)
            {
                rgStudiesSupport.Columns.FindByUniqueName("Edit").Visible = true;
            }
            if(user.CanDelete)
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
            //Submits the changes to the database
            siftaDB.SubmitChanges();
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
            //Submits the changes to the database
            siftaDB.SubmitChanges();
        }
        private void GrabStudiesValuesFromUserControl(UserControl userControl, ref FundingStudy StudiesFunding)
        {
            #region Controls
            var rcbMod = (RadComboBox)userControl.FindControl("rcbMod");
            var rcbType = (RadComboBox)userControl.FindControl("rcbType");
            var rtbBasisProjectNumber = (RadTextBox)userControl.FindControl("rtbBasisProjectNumber");
            var rntbUSGSCWPFunding = (RadNumericTextBox)userControl.FindControl("rntbUSGSCWPFunding");
            var rntbCustomerFunding = (RadNumericTextBox)userControl.FindControl("rntbCustomerFunding");
            var rtbRemarks = (RadTextBox)userControl.FindControl("rtbRemarks");
            #endregion

            #region Assign Values
            StudiesFunding.AgreementModID = Convert.ToInt32(rcbMod.SelectedValue);
            if (String.IsNullOrEmpty(rcbType.SelectedValue)) StudiesFunding.ResearchCodeID = null; else StudiesFunding.ResearchCodeID = Convert.ToInt32(rcbType.SelectedValue);
            StudiesFunding.FundingUSGSCWP = rntbUSGSCWPFunding.Value;
            StudiesFunding.FundingCustomer = rntbCustomerFunding.Value;
            StudiesFunding.BasisProjectNumber = rtbBasisProjectNumber.Text;
            StudiesFunding.Remarks = rtbRemarks.Text;
            if(StudiesFunding.CreatedBy == null || StudiesFunding.CreatedDate == null)
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
            //Submit Changes to the DB
            siftaDB.SubmitChanges();
        }
        #endregion

        #region Documents
        protected void rbDownloadJFADocuments_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("Documents/Download.aspx?Type=JFADocument&a={0}&dr={1}&fr={2}&d={3}&p={4}", agreement.AgreementID, rcbDirector.SelectedValue, rcbFinancialReviewer.SelectedValue, rtbDUNS.Text, rtbProjectNumber.Text));
        }
        protected void rbUploadDocuments_Click(object sender, EventArgs e)
        {
            rgAgreementDocuments.Rebind();
        }

        protected void rgAgreementDocuments_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var Path = (string)((GridDataItem)e.Item).GetDataKeyValue("Path");
            if(File.Exists(Path)) File.Delete(Path);
        }

        protected void rgAgreementDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var path = new DirectoryInfo(String.Format("D:\\siftaroot\\Documents\\Agreements\\{0}", agreement.AgreementID));
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
                doc["URL"] = String.Format("http://sifta.water.usgs.gov/Documents/Agreements/{0}/{1}", agreement.AgreementID, file.Name);
                doc["Path"] = file.FullName;
                dt.Rows.Add(doc);
            }
            //Set that datatable as the datasource for the Agreement Documents Grid on the Documents page. 
            rgAgreementDocuments.DataSource = dt;
            //Set that datatable as the datasource for the Agreement Documents Grid on the Overview page
            rgOverViewDocuments.DataSource = dt;
            if(user.CanDelete)
            {
                rgAgreementDocuments.Columns.FindByUniqueName("DeleteDocument").Visible = true;
            }
            if(user.CanInsert || user.CanUpdate)
            {
                rauFile.Visible = true;;
                rbUploadDocuments.Visible = true;
            }
        }
        #endregion

        #region Cooperative Funding
        protected void rgCoopFunding_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCoopFunding.DataSource = siftaDB.spCooperativeFunding(agreement.Customer.Center.OrgCode, rsbCoopFunding.Text).Where(p => p.AgreementID == agreement.AgreementID);
        }

        protected void rgCoopFunding_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            if (e.DetailTableView.Name == "Accounts")
            {
                var AgreementID = Convert.ToInt32(dataItem.GetDataKeyValue("AgreementID"));
                e.DetailTableView.DataSource = siftaDB.CooperativeFundings.Where(p => p.AgreementID == AgreementID);
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
            siftaDB.SubmitChanges();
        }
        private void GrabCooperativeFundingValuesFromForm(ref CooperativeFunding cf, UserControl control)
        {
            var rntbFiscalYear = (RadNumericTextBox)control.FindControl("rntbFiscalYear");
            var rntbModNumber = (RadNumericTextBox)control.FindControl("rntbModNumber");
            var rcbAccount = (RadComboBox)control.FindControl("rcbAccount");
            var rntbUSGS = (RadNumericTextBox)control.FindControl("rntbUSGS");
            var rntbCooperator = (RadNumericTextBox)control.FindControl("rntbCooperator");
            var rcbStatus = (RadComboBox)control.FindControl("rcbStatus");
            var rtbRemarks = (RadTextBox)control.FindControl("rtbRemarks");

            cf.FiscalYear = Convert.ToInt32(rntbFiscalYear.Value);
            cf.ModNumber = Convert.ToInt32(rntbModNumber.Value);
            cf.AccountNumber = rcbAccount.Text;
            cf.FundingUSGSCWP = rntbUSGS.Value;
            cf.FundingCustomer = rntbCooperator.Value;
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
        #endregion

        #region Edit Agreement
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            #region Assign Values
            //Agreement Info
            agreement.BillingCycleFrequency = rcbBillingCycle.SelectedValue;
            agreement.FundsType = rcbFundsType.SelectedValue;
            agreement.MatchPairCode = rtbMPC.Text;
            agreement.SalesDocument = rtbSalesDocument.Text;
            agreement.PurchaseOrderNumber = rtbPurchaseOrderNumber.Text;
            agreement.ModifiedBy = user.ID;
            agreement.ModifiedDate = DateTime.Now;
            #endregion

            //Removed 11/4/2014 to remove tags 
            //#region Tags
            ////Remove any existing tags so it doesn't duplicate
            //agreement.Tags = "";
            ////Creates a dictionary of existing tags
            //var TagDictionary = siftaDB.lutTags.ToDictionary(p => p.Tag, p => p.Tag);
            ////Add each tag to the tags section
            //foreach (AutoCompleteBoxEntry tag in racbTags.Entries)
            //{
            //    //Checks to see if the tag exists in the database
            //    if (!TagDictionary.ContainsKey(tag.Text.Replace("#", "")))
            //    {
            //        //Create a new Tag
            //        var newTag = new lutTag();
            //        //Set the tag = to the new tag
            //        newTag.Tag = tag.Text.Replace("#", "");
            //        //Add it to the look up table for tags
            //        siftaDB.lutTags.InsertOnSubmit(newTag);
            //    }
            //    //Removes the # if they used one
            //    agreement.Tags += string.Format("#{0}", tag.Text.Replace("#", ""));
            //}
            //#endregion
            siftaDB.SubmitChanges();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        #endregion

        #region Agreement Log
        protected void rgAgreementLog_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgAgreementLog.DataSource = siftaDB.AgreementModLogs.Where(p => p.AgreementMod.AgreementID == agreement.AgreementID);
        }
        public String AgreementLogType(String ID)
        {
            if (AgreementLogTypes.ContainsKey(ID)) return AgreementLogTypes[ID]; else return "";
        }
        protected void rbSubmitAgreementLog_Click(object sender, EventArgs e)
        {
            //if(rcbActionAgreementLog.SelectedIndex != 0)
            //{
            //    var newLog = new AgreementModLog();
            //    newLog.CreatedBy = newLog.ModifiedBy = user.ID;
            //    newLog.CreatedDate = newLog.ModifiedDate = DateTime.Now;
            //    newLog.LoggedDate = Convert.ToDateTime(rdtpAgreementLogTime.SelectedDate);
            //    newLog.AgreementModID = Convert.ToInt32(rcbModNumberAgreementLog.SelectedValue);
            //    newLog.AgreementLogTypeID = Convert.ToInt32(rcbActionAgreementLog.SelectedValue);
            //    newLog.Remarks = rtbRemarksAgreementLog.Text;
            //    siftaDB.AgreementModLogs.InsertOnSubmit(newLog);
            //    siftaDB.SubmitChanges();
            //    //Set values in form back to default
            //    rtbRemarksAgreementLog.Text = "";
            //    rcbModNumberAgreementLog.SelectedIndex = 0;
            //    rcbActionAgreementLog.SelectedIndex = 0;
            //    rdtpAgreementLogTime.SelectedDate = DateTime.Now;
            //    rgAgreementLog.Rebind();
            //}
        }
        #endregion

        #region Inline Code
        public String AgreementInfoDateFormat(DateTime? d)
        {
            if(d != null)
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
                if(mods.Count() != 0)
                {
                    return mods.FirstOrDefault().EndDate;
                }
                else return null;
            }
        }
        //Removed 11/4/2014 to remove tags 
        //public String Tags
        //{
        //    get
        //    {
        //        if (!String.IsNullOrEmpty(agreement.Tags)) return agreement.Tags.Replace("#", " #"); else return "";
        //    }
        //}
        public String ModName(object o)
        {
            if (o == null) return "";
            if (o.ToString() == "0") return "Agreement";
            return String.Format("Mod {0}", o);
        }
        public String ShortenSiteName(object o)
        {
            int idealLength = 10;
            if (o == null) return "";
            var str = o.ToString();
            if (str.Length <= idealLength) return str; else return str.Substring(0, idealLength) + "...";
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
       
    }
}