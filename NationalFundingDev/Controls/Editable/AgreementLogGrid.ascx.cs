using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.Editable
{
    public partial class AgreementLogGrid : System.Web.UI.UserControl
    {
        public Boolean Edit = false, AddNewRecords = false, Delete = false;
        public Agreement agreement;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void rgAgreementLog_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgAgreementLog.DataSource = siftaDB.vAgreementLogs.Where(p => p.AgreementID == agreement.AgreementID).OrderByDescending(p => p.LoggedDate);
            rgAgreementLog.Columns.FindByUniqueName("Edit").Visible = Edit;
            rgAgreementLog.Columns.FindByUniqueName("Delete").Visible = Delete;
            if(AddNewRecords)
            {
                rgAgreementLog.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgAgreementLog.MasterTableView.CommandItemSettings.ShowRefreshButton = true;
            }
        }

        protected void rgAgreementLog_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Cast the GridCommandEventArgs item as an editable item
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Find the user control used by that item save it as a UserControl
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            var modLog = new AgreementModLog();
            GrabValuesFromUserControl(userControl, ref modLog);
            siftaDB.AgreementModLogs.InsertOnSubmit(modLog);
            siftaDB.SubmitChanges();
        }

        protected void rgAgreementLog_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            var AgreementModLogID = Convert.ToInt32(editedItem.GetDataKeyValue("AgreementModLogID").ToString());
            var agreementLog = siftaDB.AgreementModLogs.FirstOrDefault(p => p.AgreementModLogID == AgreementModLogID);
            GrabValuesFromUserControl(userControl, ref agreementLog);
            siftaDB.SubmitChanges();
        }

        protected void rgAgreementLog_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var AgreementModLogID = (int)(e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AgreementModLogID"];
            siftaDB.AgreementModLogs.DeleteOnSubmit(siftaDB.AgreementModLogs.FirstOrDefault(p => p.AgreementModLogID == AgreementModLogID));
            siftaDB.SubmitChanges();
        }
        private void GrabValuesFromUserControl(UserControl uc, ref AgreementModLog m)
        {
            #region User Controls
            //Grab the controls from the user controls
            var rcbMod = (uc.FindControl("rcbMod") as RadComboBox);
            var rdtpAgreementLogTime = (uc.FindControl("rdtpAgreementLogTime") as RadDateTimePicker);
            var rcbActionAgreementLog = (uc.FindControl("rcbActionAgreementLog") as RadComboBox);
            var rtbRemarksAgreementLog = (uc.FindControl("rtbRemarksAgreementLog") as RadTextBox);
            
            #endregion
            
            #region Assign Values
            m.AgreementModID = Convert.ToInt32(rcbMod.SelectedValue);
            if (rdtpAgreementLogTime.SelectedDate != null) m.LoggedDate = Convert.ToDateTime(rdtpAgreementLogTime.SelectedDate); else m.LoggedDate = DateTime.Now;
            m.AgreementLogTypeID = Convert.ToInt32(rcbActionAgreementLog.SelectedValue);
            m.ModifiedBy = user.ID;
            m.ModifiedDate = DateTime.Now;
            m.Remarks = rtbRemarksAgreementLog.Text;
            if(String.IsNullOrEmpty(m.CreatedBy))
            {
                m.CreatedBy = user.ID;
                m.CreatedDate = DateTime.Now;
            }
            #endregion
        }

        public String LoggedBy(object name, object id)
        {
            if (name == null && id == null) return "";
            if (name == null) return id.ToString();
            return name.ToString();
        }

        
    }
}