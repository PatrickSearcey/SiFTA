using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class AgreementControl : System.Web.UI.UserControl
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public Agreement agreement = new Agreement();
        public AgreementMod mod = new AgreementMod();
        #region DataItem Stuff
        /// <summary>
        /// Holds the data item passed from the grid to the control.
        /// This one will be the vAgreementInformation object of the Agreement being edited
        /// </summary>
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //Insert
            if (DataItem is GridInsertionObject)
            {
                //Set agreement variable to a new Agreement object
                agreement = new Agreement();
                //Set the mod to a new AgreementMod object
                mod = new AgreementMod();
                //Show the Insert Button
                btnInsert.Visible = true;
                var customer = siftaDB.Customers.FirstOrDefault(p=>p.CustomerID.ToString() == Request.QueryString["CustomerID"]);
                //Check to see if it is a JFA 1=JFA
                if (customer.CustomerAgreementTypeID != 1)
                {
                    rntbUSGSFunding.ReadOnly = true;
                    rntbUSGSFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(vAgreementInformation))
            {
                //Show the Update Button
                btnUpdate.Visible = true;
                //Cast dataitem as a vAgreementInformation
                var a = DataItem as vAgreementInformation;
                //Grab the agreement matching the ID passed in the dataitem
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == a.AgreementID);
                //Set mod to be the Mod 0 for this agreement
                mod = agreement.AgreementMods.FirstOrDefault(p => p.Number == 0);
                //Set Funds Type ComboBox to agreement funds type
                rcbFundsType.SelectedValue = agreement.FundsType;
                //Set Billing Cycle Frequency ComboBox to agreement billing cycle frequency
                rcbBillingCycle.SelectedValue = agreement.BillingCycleFrequency;
                var customer = siftaDB.Customers.FirstOrDefault(p => p.CustomerID.ToString() == Request.QueryString["CustomerID"]);
                //Check to see if it is a JFA 1=JFA
                if (customer.CustomerAgreementTypeID != 1)
                {
                    rntbUSGSFunding.ReadOnly = true;
                    rntbUSGSFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }

        }
        
    }
}