using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class ReceiverEditForm : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public Receiver rec;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public string OrgCode;
        private int AgreementID;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetOrgCode();

            //Insert
            if (DataItem is GridInsertionObject)
            {
                rec = new Receiver();
                btnInsert.Visible = true;
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(Receiver))
            {
                rec = (Receiver)DataItem;
                AgreementID = rec.AgreementModID.GetValueOrDefault();
                rcbAccount.SelectedValue = rec.AccountNumber;
                rddlCustomerClass.SelectedValue = rec.CustomerClass;
                rddlStatus.SelectedValue = rec.Status;
                rcbMod.SelectedValue = rec.ModNumber;
                btnUpdate.Visible = true;
            }
        }

        private void GetOrgCode()
        {
            var oc = Request.QueryString["OrgCode"];
            var cid = Request.QueryString["CustomerID"];
            var aid = Request.QueryString["AgreementID"];
            if (!string.IsNullOrEmpty(oc))
            {
                OrgCode = oc;
            }
            if (!string.IsNullOrEmpty(cid))
            {
                OrgCode = siftaDB.Customers.FirstOrDefault(p => p.CustomerID.ToString() == cid).OrgCode;
            }
            if (!string.IsNullOrEmpty(aid))
            {
                OrgCode = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID.ToString() == aid).Customer.OrgCode;
            }
        }

        protected void ldsAccounts_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = siftaDB.vAccounts.Where(p => p.OrgCode == OrgCode);
        }

        protected void rcbMPC_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = siftaDB.lutMatchPairCodes.Select(p => p);
        }

        protected void rcbPEC_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = siftaDB.lutProgramElementCodes.Where(p => p.Active == "Y");
        }

        public string ProcessMyDataItem(object myValue)
        {
            if (myValue == null)
            {
                return "";
            }

            return myValue.ToString();
        }

        private void AddModsToComboBox()
        {
            if (AgreementID > 0)
            {
                var agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                foreach (var mod in agreement.AgreementMods)
                {
                    if (mod.Number == 0)
                    {
                        rcbMod.Items.Add(new RadComboBoxItem() { Text = "Agreement", Value = mod.AgreementModID.ToString() });
                    }
                    else
                    {
                        rcbMod.Items.Add(new RadComboBoxItem() { Text = String.Format("Mod {0}", mod.Number), Value = mod.AgreementModID.ToString() });
                    }
                }
            }
            else
            {
                rcbMod.Items.Add(new RadComboBoxItem() { Text = "Error", Value = "Error" });
            }
        }
    }
}