using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class CoopFundingControl : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public String OrgCode;
        private int AgreementID;
        private Agreement agreement;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetOrgCode();
            //Adding a newe funding entry
            if(DataItem is GridInsertionObject)
            {
                GetAgreementID();
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                
                AddModsToComboBox();
                rntbCooperator.Value = 0;
                rntbUSGS.Value = 0;
                btnInsert.Visible = true;
                if (agreement != null && agreement.Customer.CustomerAgreementTypeID != null)
                {
                    if(agreement.Customer.lutCustomerAgreementType.Type != "JFA")
                    {
                        rntbUSGS.Value = null;
                        rntbUSGS.EmptyMessage = "JFA Customer Only";
                        rntbUSGS.BackColor = System.Drawing.Color.LightGray;
                    }
                    rntbUSGS.Enabled = agreement.Customer.lutCustomerAgreementType.Type == "JFA";
                }
            }
            //Editing an existing Funding entry
            if(DataItem  is CooperativeFunding)
            {
                CooperativeFunding x = (DataItem as CooperativeFunding);
                AgreementID = x.AgreementID;
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                
                AddModsToComboBox();
                rcbMod.SelectedValue = x.AgreementModID.ToString();
                btnUpdate.Visible = true;
                //Cast the DataItem as a cooperative funding
                var cf = (CooperativeFunding)DataItem;
                rcbAccount.DataBind();
                rcbAccount.SelectedValue = cf.AccountNumber;
                rntbFiscalYear.Value = cf.FiscalYear;
                rntbUSGS.Value = cf.FundingUSGSCMF;
                rntbCooperator.Value = cf.FundingCustomer;
                rcbStatus.SelectedValue = cf.Status;
                rtbRemarks.Text = cf.Remarks;
                if (agreement.Customer.lutCustomerAgreementType.Type != "JFA")
                {
                    rntbUSGS.Value = null;
                    rntbUSGS.EmptyMessage = "JFA Customer Only";
                    rntbUSGS.BackColor = System.Drawing.Color.LightGray;
                }
                rntbUSGS.Enabled = agreement.Customer.lutCustomerAgreementType.Type == "JFA";
            }
        }

        private void AddModsToComboBox()
        {
            if(AgreementID > 0)
            {
                var agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                foreach(var mod in agreement.AgreementMods)
                {
                    if(mod.Number == 0)
                    {
                        rcbMod.Items.Add(new RadComboBoxItem() { Text = "Agreement", Value = mod.AgreementModID.ToString() });
                    }else
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

        private void GetAgreementID()
        {
            var aid = AgreementIDKey().Replace("{AgreementID:\"", "").Replace("\"}", "");
            if (!Int32.TryParse(aid, out AgreementID)) AgreementID = 0;
        }
        private String AgreementIDKey()
        {
            //The Nested View of the Accounts we were editing
            var nestedView = (GridNestedViewItem)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
            //The Table that nested view belongs too
            var tbl = nestedView.Parent.Controls;
            //Check to make sure that the nested view exists in the table and that its index is greater than 0 so you can iterate backwards to find the GridDataItem
            if (tbl.IndexOf(nestedView) > 0)
            {
                for (int idx = tbl.IndexOf(nestedView); idx >= 0; idx--)
                {
                    if(tbl[idx] is GridDataItem)
                    {
                        var dataItem = (GridDataItem)tbl[idx];
                        return dataItem.KeyValues;
                    }
                }
                
            }
            return "";
        }
        private void GetOrgCode()
        {
            var oc = Request.QueryString["OrgCode"];
            var cid = Request.QueryString["CustomerID"];
            var aid = Request.QueryString["AgreementID"];
            if(!String.IsNullOrEmpty(oc))
            {
                OrgCode = oc;
            }
            if (!String.IsNullOrEmpty(cid))
            {
                OrgCode = siftaDB.Customers.FirstOrDefault(p => p.CustomerID.ToString() == cid).OrgCode;
            }
            if (!String.IsNullOrEmpty(aid))
            {
                OrgCode = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID.ToString() == aid).Customer.OrgCode;
            }
        }

        protected void rcbAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            rcbAccount.DataSource = siftaDB.spAccounts(OrgCode, e.Text).Take(5);
            rcbAccount.DataBind();
        }
        public string ProcessMyDataItem(object myValue)
        {
            if (myValue == null)
            {
                return "";
            }

            return myValue.ToString();
        }

        protected void ldsAccounts_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = siftaDB.vAccounts.Where(p => p.OrgCode == OrgCode);
        }
    }
}