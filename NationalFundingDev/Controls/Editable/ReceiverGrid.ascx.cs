using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.Editable
{
    public partial class ReceiverGrid : System.Web.UI.UserControl
    {
        public Boolean Edit = false, AddNewRecords = false, Delete = false;
        public Agreement agreement;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private User user = new User();
        private int grandTotal = 0, sirTotal = 0, reimTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void rgReceiver_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgReceiver.DataSource = siftaDB.Receivers.Where(p => p.AgreementID == agreement.AgreementID);
            rgReceiver.Columns.FindByUniqueName("Edit").Visible = Edit;
            rgReceiver.Columns.FindByUniqueName("Delete").Visible = Delete;
            if (AddNewRecords)
            {
                rgReceiver.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgReceiver.MasterTableView.CommandItemSettings.ShowRefreshButton = true;
            }
        }

        protected void rgReceiver_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            var rec = new Receiver();
            GrabValuesFromUserControl(userControl, ref rec);

            int id = siftaDB.Receivers.Max(p => p.RecID);
            rec.RecID = id + 1;

            siftaDB.Receivers.InsertOnSubmit(rec);
            siftaDB.SubmitChanges();
        }

        protected void rgReceiver_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            var ID = Convert.ToInt32(editedItem.GetDataKeyValue("RecID").ToString());
            var rec = siftaDB.Receivers.FirstOrDefault(p => p.RecID == ID);
            GrabValuesFromUserControl(userControl, ref rec);
            siftaDB.SubmitChanges();
        }

        protected void rgReceiver_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var recID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecID"];
            siftaDB.Receivers.DeleteOnSubmit(siftaDB.Receivers.FirstOrDefault(p => p.RecID == recID));
            siftaDB.SubmitChanges();
        }

        private void GrabValuesFromUserControl(UserControl uc, ref Receiver rec)
        {
            #region User Controls
            //Grab the controls from the user controls
            var rtbAgreementMod = (uc.FindControl("rtbAgreementMod") as RadTextBox);
            var rtbFiscalYear = (uc.FindControl("rtbFiscalYear") as RadTextBox);
            var rcbAccount = (uc.FindControl("rcbAccount") as RadComboBox);
            var rddlCustomerClass = (uc.FindControl("rddlCustomerClass") as RadDropDownList);
            var rcbMatchPair = (uc.FindControl("rcbMatchPair") as RadComboBox);
            var rcbProgramElementCode = (uc.FindControl("rcbProgramElementCode") as RadComboBox);
            var rtbFunding = (uc.FindControl("rtbFunding") as RadTextBox);
            var rcbMod = (uc.FindControl("rcbMod") as RadComboBox);
            var rddlStatus = (uc.FindControl("rddlStatus") as RadDropDownList);
            var rtbRemarks = (uc.FindControl("rtbRemarks") as RadTextBox);

            #endregion

            #region Assign Values
            int? mod;
            if(rtbAgreementMod.Text.Length > 0)
            {
                mod = Convert.ToInt32(rtbAgreementMod.Text);
            }
            else
            {
                mod = null;
            }

            rec.AgreementID = agreement.AgreementID;
            rec.FY = Convert.ToInt32(rtbFiscalYear.Text);
            rec.AccountNumber = rcbAccount.SelectedValue;
            rec.CustomerClass = rddlCustomerClass.SelectedValue;
            rec.MatchPair = rcbMatchPair.SelectedValue;
            rec.ProgramElementCode = rcbProgramElementCode.SelectedValue;
            rec.Funding = Convert.ToDecimal(rtbFunding.Text);
            rec.ModNumber = rcbMod.SelectedValue;
            rec.Status = rddlStatus.SelectedValue;
            rec.Remarks = rtbRemarks.Text;
            rec.EditedBy = user.ID;
            rec.EditedWhen = DateTime.Now;
            #endregion
        }

        //grandTotal = 0, sirTotal = 0, reimTotal
        protected void rgReceiver_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                int fieldValue = int.Parse(dataItem["Funding"].Text.Replace("$", "").Replace(",", ""));

                if(dataItem["CustomerClass"].Text.Contains("SIR"))
                {
                    sirTotal += fieldValue;
                }
                if (dataItem["CustomerClass"].Text.Contains("Reim"))
                {
                    reimTotal += fieldValue;
                }

                grandTotal += fieldValue;
            }
            if (e.Item is GridFooterItem)
            {
                GridFooterItem footerItem = e.Item as GridFooterItem;

                footerItem["ProgramElementCode"].Text = "Direct (SIR) Total: $" + sirTotal.ToString();
                footerItem["Funding"].Text += "Reimbursable Total: $" + reimTotal.ToString();

                footerItem["Remarks"].Text += "Grand Total: $" + grandTotal.ToString();
            }
        }

    }
}