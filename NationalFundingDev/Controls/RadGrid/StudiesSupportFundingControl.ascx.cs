using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class StudiesSupportFundingControl : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public vStudiesFundingInformation research;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (rcbType.Items.Count <= 1)
            {
                //Set the Types to be the Look up table of research codes
                rcbType.DataSource = siftaDB.lutResearchCodes.OrderBy(p => p.Description);
            }
            //Insert
            if (DataItem is GridInsertionObject)
            {
                //Set research to be a new instance of studies funding
                research = new vStudiesFundingInformation();
                //Bind the data for the Comboboxes
                BindComboBoxes();
                //Set the Mod to be the most recent mod
                rcbMod.SelectedIndex = rcbMod.Items.Count - 1;
                //Show the Insert Button
                btnInsert.Visible = true;
                var customer = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID.ToString() == Request.QueryString["AgreementID"]).Customer;
                //Check to see if it is a JFA 1=JFA
                if (customer.CustomerAgreementTypeID != 1)
                {
                    rntbUSGSCMFFunding.ReadOnly = true;
                    rntbUSGSCMFFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(vStudiesFundingInformation))
            {
                //Cast the DataItem as a StudiesFunding item and set research to be that item
                research = (vStudiesFundingInformation)DataItem;
                //Bind the Data for the Comboboxes
                BindComboBoxes();
                //Set the Mod to be that from the Dataitem we are editing
                rcbMod.SelectedValue = research.AgreementModID.ToString();
                //Set the Type Combo Box to be that from the DataItem we are editing
                rcbType.SelectedValue = research.ResearchCodeID.ToString();
                //Show the update button
                btnUpdate.Visible = true;
                var customer = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID.ToString() == Request.QueryString["AgreementID"]).Customer;
                //Check to see if it is a JFA 1=JFA
                if (customer.CustomerAgreementTypeID != 1)
                {
                    rntbUSGSCMFFunding.ReadOnly = true;
                    rntbUSGSCMFFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
        private void BindComboBoxes()
        {
            //Grabs AgreementID from URL
            var AgreementID = Convert.ToInt32(Request.QueryString["AgreementID"]);
            //Grab all the mods associated with this agreement
            var mods = siftaDB.AgreementMods.Where(p => p.AgreementID == AgreementID);
            //Format and Add each mod to the combo box of mods
            foreach(var mod in mods)
            {
                //Mod 0 should be called Original Agreement (so we don't confuse the admin)
                if(mod.Number == 0)
                {
                    rcbMod.Items.Add(new RadComboBoxItem() { Text = "Original Agreement", Value = mod.AgreementModID.ToString() });
                }
                //For all other mod numbers the text should be Mod # and the value should be the AgreementModID
                else
                {
                    rcbMod.Items.Add(new RadComboBoxItem() { Text = String.Format("Mod {0}", mod.Number), Value = mod.AgreementModID.ToString() });
                }
            }
        }
    }
}