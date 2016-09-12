using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class AgreementLogEditForm : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public vAgreementLog log;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

            //Insert
            if (DataItem is GridInsertionObject)
            {
                log = new vAgreementLog();
                btnInsert.Visible = true;
                rcbMod.DataSource = AgreementModNames();
                rcbMod.DataBind();
                rcbMod.SelectedValue = rcbMod.Items.Last().Value;
                rcbActionAgreementLog.DataSource = siftaDB.lutAgreementLogTypes;
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(vAgreementLog))
            {
                log = (vAgreementLog)DataItem;
                rcbMod.DataSource = AgreementModNames();
                rcbActionAgreementLog.DataSource = siftaDB.lutAgreementLogTypes;
                rcbMod.DataBind();
                rcbActionAgreementLog.DataBind();
                rcbMod.SelectedValue = rcbMod.Items.FirstOrDefault(p => p.Text == log.ModName).Value;
                rcbActionAgreementLog.SelectedValue = log.AgreementLogTypeID.ToString();
                rdtpAgreementLogTime.SelectedDate = log.LoggedDate;
                btnUpdate.Visible = true;
            }
        }
        
        private Dictionary<int, string> AgreementModNames()
        {
            var dict = new Dictionary<int, string>();
            var agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID.ToString() == Request.QueryString["AgreementID"]);
            foreach(var mod in agreement.AgreementMods)
            {
                var modName = "";
                if (mod.Number == 0) modName = "Agreement"; else modName = String.Format("Mod {0}", mod.Number);
                dict.Add(mod.AgreementModID, modName);
            }
            return dict;
        }
    }
}