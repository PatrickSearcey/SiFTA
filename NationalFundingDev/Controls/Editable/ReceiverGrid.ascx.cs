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
        public int aID;
        private double grandTotal = 0, sirTotal = 0, reimTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            int modID = int.Parse(Request.QueryString["AgreementID"]);
            aID = siftaDB.AgreementMods.First(p => p.AgreementID == modID).AgreementModID;
            var ag = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == aID);
            try
            {
                rcbMatchPair.SelectedValue = ag.MatchPairCode.ToString();
                rcbProgramElementCode.SelectedValue = ag.ProgramElementCode.ToString();
            }
            catch { }
        }

        protected void rgReceiver_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int modID = agreement.AgreementID;
            int aID = siftaDB.AgreementMods.First(p => p.AgreementID == modID).AgreementModID;

            rgReceiver.DataSource = siftaDB.AccountFundSources.Where(p => p.AgreementModID == aID);
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
            var rec = new AccountFundSource();
            GrabValuesFromUserControl(userControl, ref rec);

            try
            {
                int id = siftaDB.AccountFundSources.Max(p => p.AFSID);
                rec.AFSID = id + 1;
            }
            catch(Exception ex)
            {
                rec.AFSID = 0;
            }

            siftaDB.AccountFundSources.InsertOnSubmit(rec);
            siftaDB.SubmitChanges();
        }

        protected void rgReceiver_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            var ID = Convert.ToInt32(editedItem.GetDataKeyValue("AFSID").ToString());
            var rec = siftaDB.AccountFundSources.FirstOrDefault(p => p.AFSID == ID);
            GrabValuesFromUserControl(userControl, ref rec);
            siftaDB.SubmitChanges();
        }

        protected void rgReceiver_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var recID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AFSID"];
            siftaDB.AccountFundSources.DeleteOnSubmit(siftaDB.AccountFundSources.FirstOrDefault(p => p.AFSID == recID));
            siftaDB.SubmitChanges();
        }

        private void GrabValuesFromUserControl(UserControl uc, ref AccountFundSource rec)
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

            rec.AgreementModID = agreement.AgreementID;
            rec.FundSourceFY = Convert.ToInt32(rtbFiscalYear.Text);
            rec.AccountNumber = rcbAccount.SelectedValue;
            rec.CustomerClass = rddlCustomerClass.SelectedValue;
            rec.MatchPair = rcbMatchPair.SelectedValue;
            rec.ProgramElementCode = rcbProgramElementCode.SelectedValue;
            rec.Funding = Convert.ToDouble(rtbFunding.Text);
            rec.FundStatus = rddlStatus.SelectedValue;
            rec.Remarks = rtbRemarks.Text;
            rec.ModifiedBy = user.ID;
            rec.ModifiedDate = DateTime.Now;

            if(String.IsNullOrEmpty(rec.CreatedBy))
            {
                rec.CreatedBy = user.ID;
                rec.CreatedDate = DateTime.Now;
            }
            #endregion
        }

        //grandTotal = 0, sirTotal = 0, reimTotal
        protected void rgReceiver_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            int modID = int.Parse(Request.QueryString["AgreementID"]);
            int aID = siftaDB.AgreementMods.First(p => p.AgreementID == modID).AgreementModID;

            var rec = siftaDB.AccountFundSources.Where(p => p.AgreementModID == aID);
            double sirTotal = rec.Where(p => p.CustomerClass.Contains("SIR")).Sum(p => p.Funding) ?? 0;
            double reimTotal = rec.Where(p => p.CustomerClass.Contains("Reim")).Sum(p => p.Funding) ?? 0;
            grandTotal = sirTotal + reimTotal;

            var funding = siftaDB.vAgreementFundingOverviews.Where(p => p.AgreementID == aID);
            double sumUSGS = funding.Sum(p => p.FundingUSGSCMF) ?? 0;
            double sumCust = funding.Sum(p => p.FundingCustomer) ?? 0;

            GridFooterItem footerItem = e.Item as GridFooterItem;

            dirTd.InnerHtml = "<span>$" + sirTotal.ToString("#,##0") + "</span>";
            cmfTd.InnerHtml = "<span>$" + sumUSGS.ToString("#,##0") + "</span>";

            double dirDiff = sirTotal - sumUSGS;
            string dirStyle = dirDiff < 0 ? "color:red" : "";

            diff1Td.InnerHtml = "<span style='" + dirStyle + "'>$" + dirDiff.ToString("#,##0") + "</span>";

            reimTd.InnerHtml = "<span>$" + reimTotal.ToString("#,##0") + "</span>";
            custTd.InnerHtml = "<span>$" + sumCust.ToString("#,##0") + "</span>";

            double reimDiff = reimTotal - sumCust;
            string reimStyle = reimDiff < 0 ? "color:red" : "";

            diff2Td.InnerHtml = "<span style='" + reimStyle + "'>$" + reimDiff.ToString("#,##0") + "</span>";

            totalsTd.InnerHtml = "<span>$" + grandTotal.ToString("#,##0") + "</span>";
            aogtTd.InnerHtml = "<span>$" + (sumUSGS + sumCust).ToString("#,##0") + "</span>";

            double gDiff = (grandTotal - (sumUSGS + sumCust));
            string gStyle = gDiff < 0 ? "color:red" : "";

            diff3Td.InnerHtml = "<span style='" + gStyle + "'>$" + gDiff.ToString("#,##0") + "</span>";
        }

        public string ProcessMyDataItem(object myValue)
        {
            if (myValue == null)
            {
                return "";
            }

            return myValue.ToString();
        }

        protected void rcbMPC_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = siftaDB.lutMatchPairCodes.Select(p => p);
        }

        protected void rcbMatchPair_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var agMPC = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == aID);
            agMPC.MatchPairCode = e.Value;
            siftaDB.SubmitChanges();
        }

        protected void rcbPEC_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = siftaDB.lutProgramElementCodes.Where(p => p.Active == "Y");
        }

        protected void rcbProgramElementCode_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var agPEC = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == aID);
            agPEC.ProgramElementCode = e.Value;
            siftaDB.SubmitChanges();
        }

    }
}