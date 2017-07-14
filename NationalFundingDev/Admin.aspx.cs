using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev
{
    public partial class Admin : System.Web.UI.Page
    {
        #region Local Variables
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public Center center;
        private User user;
        #endregion 

        #region Initialization
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the OrgCode from the URL
            var orgCode = Request.QueryString["OrgCode"];
            //Get the Center with a matching Org Code from the database
            center = siftaDB.Centers.FirstOrDefault(p => p.OrgCode == orgCode);
            //If the center doesn't exist send them back to the default page
            if (center == null) Response.Redirect("Default.aspx");
            //Set the User to be one from this org code (checks for permissions)
            user = new User(orgCode);
            //If they aren't admins for this center send them back to the default page
            if (!user.IsAdmin) Response.Redirect("Default.aspx");
            //Set Information
            if(!IsPostBack)
            {
                SetCenterInformation();
                rtsAdminOptions.SelectedIndex = rmpAdminOptions.SelectedIndex = 0;
            }
            ltlUserAccessErrors.Text = "";
        }
        protected void rtsAdminOptions_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            rmpAdminOptions.SelectedIndex = rtsAdminOptions.SelectedIndex;
            SetCenterInformation();
            BindGrids();
        }
        #endregion

        #region Data Binding for Pages
        private void SetCenterInformation()
        {
            rtbAddress.Text = center.Address;
            rtbCity.Text = center.City;
            rtbState.Text = center.State;
            rtbZipCode.Text = center.ZipCode;
            rtbDUNS.Text = center.DUNS;
            rtbProjectName.Text = center.ProjectName;
            rtbProjectNumber.Text = center.ProjectNumber;
        }
        private void BindGrids()
        {
            rgUserAccess.Rebind();
            rgDirectors.Rebind();
            rgFinancialReviewers.Rebind();
            rgCollectionCodes.Rebind();
            rgCoopAccounts.Rebind();
            rgCenterAccounts.Rebind();
        }
        #endregion

        #region Center Information
        protected void rbSubmitCenterInformation_Click(object sender, EventArgs e)
        {
            center.Address = rtbAddress.Text;
            center.City = rtbCity.Text;
            center.State = rtbState.Text;
            center.ZipCode = rtbZipCode.Text;
            center.DUNS = rtbDUNS.Text;
            center.ProjectNumber = rtbProjectNumber.Text;
            center.ProjectName = rtbProjectName.Text;
            siftaDB.SubmitChanges();
        }
        #endregion

        #region User Access
        protected void rgUserAccess_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgUserAccess.DataSource = siftaDB.EmployeeAccesses.Where(p => p.OrgCode == center.OrgCode);
        }

        protected void rgUserAccess_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Grab the values form the item to be inserted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Create a new User Access Item
            var access = new EmployeeAccess();
            //Assign Values
            access.OrgCode = center.OrgCode;
            access.CanViewAdminPortal = Convert.ToBoolean(values.NewValues["CanViewAdminPortal"]);
            access.CanInsertRecords = Convert.ToBoolean(values.NewValues["CanInsertRecords"]);
            access.CanUpdateRecords = Convert.ToBoolean(values.NewValues["CanUpdateRecords"]);
            access.CanDeleteRecords = Convert.ToBoolean(values.NewValues["CanDeleteRecords"]);
            access.EmployeeID = values.NewValues["EmployeeID"].ObjToString();
            //Tries to validate the employeeid and add it to be tracked
            access.EmployeeID.ValidEmployeeID();
            //Add to database
            siftaDB.EmployeeAccesses.InsertOnSubmit(access);
            //Try to submit changes to the database
            try
            {
                siftaDB.SubmitChanges();
            }
            catch(Exception ex)
            {
                //If there was a problem with the database it was because the user didn't enter a real customerContact's ID add the warning text
                ltlUserAccessErrors.Text += String.Format("*{0} was unable to be added because this is not a valid Employee User ID <br />", access.EmployeeID);
            }
        }

        protected void rgUserAccess_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Grab the values from the item to be updated
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the EmployeAccessID of the edited Item
            var EmployeeAccessID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EmployeeAccessID"];
            //Grab the customerContact acces item from the database that has a matching ID
            var access = siftaDB.EmployeeAccesses.FirstOrDefault(p => p.EmployeeAccessID == EmployeeAccessID);
            //Check to see if the person they are editing is a center admin
            if(!access.CenterAdmin)
            {
                //If they are not a center admin update their information
                //Assign Values
                access.OrgCode = center.OrgCode;
                access.CanViewAdminPortal = Convert.ToBoolean(values.NewValues["CanViewAdminPortal"]);
                access.CanInsertRecords = Convert.ToBoolean(values.NewValues["CanInsertRecords"]);
                access.CanUpdateRecords = Convert.ToBoolean(values.NewValues["CanUpdateRecords"]);
                access.CanDeleteRecords = Convert.ToBoolean(values.NewValues["CanDeleteRecords"]);
                access.EmployeeID = values.NewValues["EmployeeID"].ObjToString();
                //Tries to validate the employeeid and add it to be tracked
                access.EmployeeID.ValidEmployeeID();
                //Try to submit changes to the database
                try
                {
                    siftaDB.SubmitChanges();
                }
                catch (Exception ex)
                {
                    //If there was a problem with the database it was because the user didn't enter a real customerContact's ID add the warning text
                    ltlUserAccessErrors.Text += String.Format("*{0} was unable to be updated because this is not a valid Employee User ID <br />", access.EmployeeID);
                }
            }
            else
            {
                ltlUserAccessErrors.Text += String.Format("*{0} was unable to be edited because of their status as one of the centers main administrators.<br />", access.EmployeeID);
            }
        }

        protected void rgUserAccess_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Grab the values from the item to be deleted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the EmployeAccessID of the item to be deleted
            var EmployeeAccessID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EmployeeAccessID"];
            //Grab the customerContact acces item from the database that has a matching ID
            var access = siftaDB.EmployeeAccesses.FirstOrDefault(p => p.EmployeeAccessID == EmployeeAccessID);
            //Check to see if the person they are editing is a center admin
            if (!access.CenterAdmin)
            {
                //Remove the employees access from the database
                siftaDB.EmployeeAccesses.DeleteOnSubmit(access);
                //Submit changes to the database
                siftaDB.SubmitChanges();
            }
            else
            {
                //You cannot update or delete center admin
                ltlUserAccessErrors.Text += String.Format("*{0} was unable to be deleted because of their status as one of the centers main administrators.<br />", access.EmployeeID);
            }
        }
        #endregion

        #region Directors
        protected void rgDirectors_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgDirectors.DataSource = siftaDB.CenterDirectors.Where(p => p.OrgCode == center.OrgCode);
        }

        protected void rgDirectors_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be Inserted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Add the new center director to the db
            siftaDB.CenterDirectors.InsertOnSubmit(new CenterDirector()
            {
                OrgCode = center.OrgCode,
                Name = values.NewValues["Name"].ObjToString(),
                Title = values.NewValues["Title"].ObjToString(),
                Phone = values.NewValues["Phone"].ObjToString()
            });
            //submit changes to the database
            siftaDB.SubmitChanges();
        }

        protected void rgDirectors_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be Updated
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterDirectorID of the item to be Updated
            var CenterDirectorID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CenterDirectorID"];
            //Grab the Center director from the database that we are going to be updating
            var director = siftaDB.CenterDirectors.FirstOrDefault(p => p.CenterDirectorID == CenterDirectorID);
            //Udpate values
            director.Name = values.NewValues["Name"].ObjToString();
            director.Phone = values.NewValues["Phone"].ObjToString();
            director.Title = values.NewValues["Title"].ObjToString();
            //Submit changes to the database
            siftaDB.SubmitChanges();
        }

        protected void rgDirectors_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be deleted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterDirectorID of the item to be deleted
            var CenterDirectorID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CenterDirectorID"];
            //Grab the Center director from the database that we are going to be deleteing
            var director = siftaDB.CenterDirectors.FirstOrDefault(p => p.CenterDirectorID == CenterDirectorID);
            //Delete that director from the database
            siftaDB.CenterDirectors.DeleteOnSubmit(director);
            //Submit changes to the database
            siftaDB.SubmitChanges();
        }
        #endregion

        #region Financial Reviewer
        protected void rgFinancialReviewers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgFinancialReviewers.DataSource = siftaDB.CenterFinancialReviewers.Where(p => p.OrgCode == center.OrgCode);
        }

        protected void rgFinancialReviewers_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be Inserted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Add the new financial reviewr to the db
            siftaDB.CenterFinancialReviewers.InsertOnSubmit(new CenterFinancialReviewer()
            {
                OrgCode = center.OrgCode,
                Name = values.NewValues["Name"].ObjToString(),
                Phone = values.NewValues["Phone"].ObjToString()
            });
            //submit changes to the database
            siftaDB.SubmitChanges();
        }

        protected void rgFinancialReviewers_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be Updated
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterFinancialReviewerID of the item to be Updated
            var CenterFinancialReviewerID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CenterFinancialReviewerID"];
            //Grab the Center financial reviewer from the database that we are going to be updating
            var director = siftaDB.CenterFinancialReviewers.FirstOrDefault(p => p.CenterFinancialReviewerID == CenterFinancialReviewerID);
            //Udpate values
            director.Name = values.NewValues["Name"].ObjToString();
            director.Phone = values.NewValues["Phone"].ObjToString();
            //Submit changes to the database
            siftaDB.SubmitChanges();
        }

        protected void rgFinancialReviewers_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be deleted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterFinancialReviewerID of the item to be deleted
            var CenterFinancialReviewerID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CenterFinancialReviewerID"];
            //Grab the Center financial reviewer from the database that we are going to be deleteing
            var financialReviewer = siftaDB.CenterFinancialReviewers.FirstOrDefault(p => p.CenterFinancialReviewerID == CenterFinancialReviewerID);
            //Delete that financial reviewer from the database
            siftaDB.CenterFinancialReviewers.DeleteOnSubmit(financialReviewer);
            //Submit changes to the database
            siftaDB.SubmitChanges();
        }
        #endregion

        #region Collection Codes
        protected void rgCollectionCodes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCollectionCodes.DataSource = siftaDB.lutCollectionCodes.Where(p => p.OrgCode == center.OrgCode).OrderBy(p => p.Category).ThenBy(p => p.Code);
        }

        protected void rgCollectionCodes_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be Inserted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Create new collection code
            var code = new lutCollectionCode()
            {
                OrgCode = center.OrgCode,
                Category = values.NewValues["Category"].ObjToString(),
                CreatedBy = user.ID,
                CreatedDate = DateTime.Now,
                ModifiedBy = user.ID,
                ModifiedDate = DateTime.Now
            };
            //Set Values
            code.Code = values.NewValues["Code"].ObjToString();
            code.Description = values.NewValues["Description"].ObjToString();
            code.Remarks = values.NewValues["Remarks"].ObjToString();
            code.Active = Convert.ToBoolean(values.NewValues["Active"]);
            //Add code to the database
            siftaDB.lutCollectionCodes.InsertOnSubmit(code);
            //submit changes to the database
            siftaDB.SubmitChanges();
        }

        protected void rgCollectionCodes_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be Updated
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CollectionCodeID of the item to be Updated
            var CollectionCodeID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CollectionCodeID"];
            //Grab the collection code from the database that we are going to be updating
            var code = siftaDB.lutCollectionCodes.FirstOrDefault(p => p.CollectionCodeID == CollectionCodeID);
            //Update Values
            code.ModifiedBy = user.ID;
            code.ModifiedDate = DateTime.Now;
            code.Category = values.NewValues["Category"].ObjToString();
            code.Code = values.NewValues["Code"].ObjToString();
            code.Description = values.NewValues["Description"].ObjToString();
            code.Remarks = values.NewValues["Remarks"].ObjToString();
            code.Active = Convert.ToBoolean(values.NewValues["Active"]);
            //Submit Changes to the database
            siftaDB.SubmitChanges();
        }
        #endregion

        #region Center Specific Accounts
        protected void rgCoopAccounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCoopAccounts.DataSource = center.CenterSpecificAccounts;
        }

        protected void rgCoopAccounts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values form the item to be inserted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Create new Center Specific Account
            var account = new CenterSpecificAccount();
            //Assign values
            account.OrgCode = center.OrgCode;
            account.AccountNumber = values.NewValues["AccountNumber"].ObjToString();
            account.AccountName = values.NewValues["AccountName"].ObjToString();
            account.OfficeCode = values.NewValues["OfficeCode"].ObjToString();
            account.ManagerName = values.NewValues["ManagerName"].ObjToString();
            foreach (var a in siftaDB.CenterSpecificAccounts.Where(p => p.OrgCode == center.OrgCode && p.OfficeCode == account.OfficeCode))
            {
                a.ManagerName = account.ManagerName;
            }
            siftaDB.CenterSpecificAccounts.InsertOnSubmit(account);
            siftaDB.SubmitChanges();
        }

        protected void rgCoopAccounts_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be updated
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterSpecificAccountID of the edited Item
            var CenterSpecificAccountID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CenterSpecificAccountID"];
            //Grab the account associated with that id
            var account = siftaDB.CenterSpecificAccounts.FirstOrDefault(p => p.CenterSpecificAccountID == CenterSpecificAccountID);
            //update values
            account.AccountName = values.NewValues["AccountName"].ObjToString();
            account.OfficeCode = values.NewValues["OfficeCode"].ObjToString();
            account.ManagerName = values.NewValues["ManagerName"].ObjToString();
            foreach(var a in siftaDB.CenterSpecificAccounts.Where(p=>p.OrgCode == center.OrgCode && p.OfficeCode == account.OfficeCode))
            {
                a.ManagerName = account.ManagerName;
            }
            //Submit changes to database
            siftaDB.SubmitChanges();
        }

        protected void rgCoopAccounts_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be deleted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterSpecificAccountID of the item to be deleted
            var CenterSpecificAccountID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CenterSpecificAccountID"];
            //Delete center specific account with matching ID on submit
            siftaDB.CenterSpecificAccounts.DeleteOnSubmit(siftaDB.CenterSpecificAccounts.FirstOrDefault(p => p.CenterSpecificAccountID == CenterSpecificAccountID));
            //Submit changes to the database
            siftaDB.SubmitChanges();
        }
        
        #endregion

        #region Accounts
        protected void rgCenterAccounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCenterAccounts.DataSource = center.Accounts;
        }

        protected void rgCenterAccounts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values form the item to be inserted
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Create new Account
            var account = new Account();
            //Assign values
            account.OrgCode = center.OrgCode;
            account.AccountNumber = values.NewValues["AccountNumber"].ObjToString();
            account.AccountName = values.NewValues["AccountName"].ObjToString();
            account.OfficeCode = values.NewValues["OfficeCode"].ObjToString();
            siftaDB.Accounts.InsertOnSubmit(account);
            siftaDB.SubmitChanges();
        }

        protected void rgCenterAccounts_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            //Grab the values from the item to be updated
            var values = (GridBatchEditingEventArgument)e.CommandArgument;
            //Grab the CenterSpecificAccountID of the edited Item
            var AccountID = (int)values.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AccountID"];
            //Grab the account associated with that id
            var account = siftaDB.Accounts.FirstOrDefault(p => p.AccountID == AccountID);
            //update values
            account.AccountName = values.NewValues["AccountName"].ObjToString();
            account.OfficeCode = values.NewValues["OfficeCode"].ObjToString();
            //Submit changes to database
            siftaDB.SubmitChanges();
        }
        #endregion

    }
}