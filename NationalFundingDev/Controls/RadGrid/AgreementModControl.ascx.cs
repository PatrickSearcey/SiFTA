using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace NationalFundingDev.Controls.RadGrid
{
    public partial class AgreementModControl : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public AgreementMod mod;
        public Agreement agreement;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            AddAgTypeToComboBox();

            //Insert
            if (DataItem is GridInsertionObject)
            {
                rcbFundsTypeDiv.Visible = false;

                var AgreementID = Convert.ToInt32(Request.QueryString["AgreementID"]);
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                //create a new AgreementMod
                mod = new AgreementMod();
                //Show the Insert Button
                btnInsert.Visible = true;
                BindComboBoxes();
                GrayOutAgreementSections();
                var customer = agreement.Customer;
                //Check to see if it is a JFA 1=JFA
                if (agreement.Customer2Group != "23A")
                {
                    rntbUSGSFunding.ReadOnly = true;
                    rntbUSGSFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(vAgreementModInformation))
            {
                
                //grab the agreement mod information
                var agreementMod = (vAgreementModInformation)DataItem;
                //Grab the mod ID for the mod being edited
                mod = siftaDB.AgreementMods.FirstOrDefault(p => p.AgreementModID == agreementMod.AgreementModID);
                //Grab the agreement information from the agreement tied to the mod
                agreement = mod.Agreement;
                //Show the Update Button
                btnUpdate.Visible = true;
                BindComboBoxes();
                //If it isn't the original Agreement Gray Out the Sections they can't edit
                if (mod.Number != 0) GrayOutAgreementSections();
                var customer = agreement.Customer;
                //Check to see if it is a JFA 1=JFA
                if (agreement.Customer2Group != "23A")
                {
                    rntbUSGSFunding.ReadOnly = true;
                    rntbUSGSFunding.BackColor = System.Drawing.Color.LightGray;
                }

                rdpStartDate.SelectedDate = mod.StartDate;
                if (mod.StartDate != null) rdpEndDate.MinDate = Convert.ToDateTime(mod.StartDate);

                if (mod.EndDate > mod.StartDate) rdpEndDate.SelectedDate = mod.EndDate;

                if (mod.Number == 0)
                {
                    var c2g = agreement.Customer2Group;
                    rcbAgType.SelectedValue = c2g;
                }
                else
                {
                    rcbFundsTypeDiv.Visible = false;
                }
            }

        }

        private void GrayOutAgreementSections()
        {
            rtbPurchaseOrderNumber.ReadOnly = true;
            rtbPurchaseOrderNumber.BackColor = System.Drawing.Color.LightGray;
            rtbSalesDocument.ReadOnly = true;
            rtbSalesDocument.BackColor = System.Drawing.Color.LightGray;
            rcbBillingCycle.Enabled = false;
            rcbFundsType.Enabled = false;
        }

        private void BindComboBoxes()
        {
            rcbBillingCycle.DataBind();
            rcbFundsType.DataBind();
            rcbFundsType.SelectedValue = agreement.FundsType;
            rcbBillingCycle.SelectedValue = agreement.BillingCycleFrequency;
        }

        protected void rdpStartDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (rdpStartDate != null) rdpEndDate.MinDate = Convert.ToDateTime(rdpStartDate.SelectedDate);
            else rdpEndDate.MinDate = new DateTime(1700, 1, 1);
        }

        private void AddAgTypeToComboBox()
        {
            var types = siftaDB.lutCustomer2Groups.Where(x => x.Customer2GroupCode != "#");
            foreach (var type in types)
            {
                rcbAgType.Items.Add(new RadComboBoxItem() { Text = type.Customer2Group, Value = type.Customer2GroupCode });
            }
        }
    }
}