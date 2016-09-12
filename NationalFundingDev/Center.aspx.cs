using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace NationalFundingDev
{
    public partial class CenterPage : System.Web.UI.Page
    {
        
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public User user;
        private string OrgCode;
        public Center center;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the Organization Code from the url www.url/Customer.aspx?OrgCode={0}
            OrgCode = Request.QueryString["OrgCode"];
            //Set center to be the one from the Database
            center = siftaDB.Centers.FirstOrDefault(p => p.OrgCode == OrgCode);
            //If the OrgCode is not valid send them back to the Default Page
            if (center == null) Response.Redirect("Default.aspx");
            //Set the User to be a User from this OrgCode for Permissions
            user = new User(OrgCode);
            //Set the Session variable Title 
            Session["Title"] = center.Name.Replace(" Water Science Center", " ");
            //Check to see if they have acess to the Admin Page for this center
            if(user.CanInsert || user.CanUpdate)
            {
                if(!Page.IsPostBack)
                {
                    //Check for Texas Users Only
                    if(center.CoopDBAccess)
                    {
                        rtsCenterOptions.Tabs.Add(new RadTab()
                        {
                            Text = "Coop Funding",
                            TabIndex = 1
                        });
                        rtsCenterOptions.SelectedIndex = rmpCenterOptions.SelectedIndex = SelectedIndex;
                    }
                }
            }
            rsbCoopFunding.DataSource = new List<string>();
        }
        protected void rtsCenterOptions_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            rmpCenterOptions.SelectedIndex = rtsCenterOptions.SelectedTab.TabIndex;
            if(rtsCenterOptions.SelectedIndex == 1)
            {
                rgCoopFunding.Rebind();
            }
        }

        #region Customers Section

        #region rcCustomer Combo Box
        /// <summary>
        /// Called when the Customer Active/All combo box has its selection changed.
        /// Has the Grid Rebind
        /// </summary>
        /// <param name="sender">The combobox sending the request</param>
        /// <param name="e">the Index Changed Event Arguments</param>
        protected void rcbCustomer_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgCustomers.Rebind();
        }
        #endregion

        #region Rad Grid
        protected void rgCustomers_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            switch (rcbCustomer.SelectedValue)
            {
                case "Active":
                    //Sets Customer grid to show all customers where the org code matches the center org code and agreements -> mods.end date > todays date
                    rgCustomers.DataSource = siftaDB.Customers.Where(c => c.OrgCode == center.OrgCode && c.Agreements.Where(a => a.AgreementMods.Where(m => m.EndDate > DateTime.Now).Count() > 0).Count() > 0).OrderBy(p => p.Name).ToList();
                    break;
                case "Recent":
                    rgCustomers.DataSource = siftaDB.Customers.Where(c => c.OrgCode == center.OrgCode && 
                        ((c.Agreements.Where(a => a.AgreementMods.Where(m => m.EndDate > DateTime.Now.AddYears(-1)).Count() > 0).Count() > 0 || c.CreatedDate > DateTime.Now.AddYears(-1)) || c.Agreements.Count == 0)).OrderBy(p => p.Name).ToList();
                    break;
                default:
                    //Sets Customer grid to show all customers where the org code matches
                    rgCustomers.DataSource =  siftaDB.Customers.Where(p => p.OrgCode == OrgCode).OrderBy(p => p.Name).ToList();
                    break;
            }
            //Set Edit Visibility for Grid
            if(user.CanInsert)
            {
                //Show the Add new customer item at the top of the grid
                rgCustomers.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                //Hide the Refresh Button
                rgCustomers.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
                //Set the text of the button to be Add New Customer
                rgCustomers.MasterTableView.CommandItemSettings.AddNewRecordText = "Add New Customer";
            }
            if(user.CanUpdate)
            {
                //Set the edit columns visibility to true
                rgCustomers.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
            }
        }
        protected void rgCustomers_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Grab the CustomerID of the customer you are editing from the edited item
            int CustomerID = Convert.ToInt32(editedItem.GetDataKeyValue("CustomerID"));
            //Add metrics
            var metric = new MetricHandler(center.OrgCode, null, null, MetricType.RecordUpdate, "Customer", String.Format("Updated CustomerID = {0}", CustomerID));
            metric.SubmitChanges();
            //Grabs the customer from the database with the matching ID 
            var customer = siftaDB.Customers.FirstOrDefault(p => p.CustomerID == CustomerID);
            //Assigns the values from the user control to the customer
            GrabValuesFromUserControl(userControl, ref customer);
            //Submits the changes to the database
            siftaDB.SubmitChanges();
        }
        protected void rgCustomers_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Add metrics
            var metric = new MetricHandler(center.OrgCode, null, null, MetricType.RecordAdded, "Customer", "Customer Added");
            metric.SubmitChanges();
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Create a new customer
            var customer = new Customer();
            //Takes the user control and puts the values from the form into the customer object
            GrabValuesFromUserControl(userControl, ref customer);
            //Adds the customer to the current center
            siftaDB.Customers.InsertOnSubmit(customer);
            //Submits the changes to the database
            siftaDB.SubmitChanges();
        }
        /// <summary>
        /// Takes a user control and maps the values from it to a customer object.
        /// The user control being used is the Customer.ascx. 
        /// Found at Controls/Customer.ascx
        /// </summary>
        /// <param name="uc">The User Control for editing customer information</param>
        /// <param name="c">The Customer Object</param>
        protected void GrabValuesFromUserControl(UserControl uc, ref Customer c)
        {
            #region User Controls
            //Grab the controls from the user controls
            var rauImage = (uc.FindControl("rauImage") as RadAsyncUpload);
            var rtbCustomerCd = (uc.FindControl("rtbCustomerCd") as RadTextBox);
            var rcbAgreementType = (uc.FindControl("rcbAgreementType") as RadComboBox);
            var rntbCustomerNo = (uc.FindControl("rntbCustomerNo") as RadNumericTextBox);
            var rtbCustomerName = (uc.FindControl("rtbCustomerName") as RadTextBox);
            var rtbCustomerAbbrev = (uc.FindControl("rtbCustomerAbbrev") as RadTextBox);
            var rtbCustomerUrl = (uc.FindControl("rtbCustomerUrl") as RadTextBox);
            var rtbRemarks = (uc.FindControl("rtbRemarks") as RadTextBox);
            var rtbCustomerTin = (uc.FindControl("rtbCustomerTin") as RadTextBox);
            var racbTags = (uc.FindControl("racbTags") as RadAutoCompleteBox);
            #endregion

            #region Assign Values
            c.OrgCode = center.OrgCode;
            c.Code = rtbCustomerCd.Text;
            //If the combo box had a selected value convert it to int and store it as the customer agreement type. If not set the agreementtypeID to null
            if (!String.IsNullOrEmpty(rcbAgreementType.SelectedValue)) c.CustomerAgreementTypeID = Convert.ToInt32(rcbAgreementType.SelectedValue); else c.CustomerAgreementTypeID = null;
            //Convert double to long to store in db
            if (rntbCustomerNo.Value != null) c.Number = Convert.ToInt64(rntbCustomerNo.Value); else c.Number = null;
            c.Name = rtbCustomerName.Text;
            c.Abbreviation = rtbCustomerAbbrev.Text;
            c.URL = rtbCustomerUrl.Text;
            c.Remarks = rtbRemarks.Text;
            c.TaxIdentificationNumber = rtbCustomerTin.Text;
            //Set the Created and Modified date and by fields
            if (c.CreatedDate == null)
            {
                c.CreatedBy = c.ModifiedBy = user.ID;
                c.CreatedDate = c.ModifiedDate = DateTime.Now;
            }
            else
            {
                c.ModifiedDate = DateTime.Now;
                c.ModifiedBy = user.ID;
            }
            #endregion


            #region Customer Image Upload
            //If a file was even uploaded
            if (rauImage.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile file in rauImage.UploadedFiles)
                {
                    var dir = "D:\\siftaroot\\Temp\\";
                    if(!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = dir + file.FileName;
                    if (File.Exists(path))
                    {
                        c.IconType = file.ContentType;
                        c.Icon = File.ReadAllBytes(path);
                        File.Delete(path);
                    }
                }
            }
            #endregion
        }
        #endregion
        #endregion

        #region Cooperative Funding
        protected void rgCoopFunding_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCoopFunding.DataSource = siftaDB.spCooperativeFunding(center.OrgCode, rsbCoopFunding.Text).OrderByDescending(p => p.StartDate);
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
            //Add metrics
            var metric = new MetricHandler(center.OrgCode, null, null, MetricType.RecordAdded, "Cooperative Funding", "Cooperative Funding Added");
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
            var metric = new MetricHandler(center.OrgCode, null, null, MetricType.RecordUpdate, "Cooperative Funding", String.Format("Updated CooperativeFundingID = {0}", CooperativeFundingID));
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
            cf.FundingUSGSCWP = Convert.ToDouble(rntbUSGS.Value);
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
            Response.Redirect(String.Format("Reports/Center/CoopFunding.aspx?OrgCode={0}",center.OrgCode).AppendBaseURL());
        }
        #endregion

        public String CenterAddress
        {
            get
            {
                if (!String.IsNullOrEmpty(center.Address)) return String.Format("{0}, {1} {2} {3}", center.Address, center.City, center.State, center.ZipCode); else return "";
            }
        }
        public byte[] GetIconBytes(object obj)
        {
            if (obj == null) return new byte[0];
            var linqBinary = (obj as System.Data.Linq.Binary).ToArray();
            return linqBinary;
        }
        public int SelectedIndex
        {
            get
            {
                int indx;
                if (Int32.TryParse(Request.QueryString["Selected"], out indx) && indx >= 0 && indx < rtsCenterOptions.Tabs.Count) return indx; else return 0;
            }
        }
        
    }
}