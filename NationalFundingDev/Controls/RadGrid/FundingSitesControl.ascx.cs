using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class FundingSitesControl : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public vSiteFundingInformation siteFunding;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private int AgreementID;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the AgreementID from the URL
            AgreementID = Convert.ToInt32(Request.QueryString["AgreementID"]);
            //Insert
            if (DataItem is GridInsertionObject)
            {
                //Set the site funding equal to a new site funding object
                siteFunding = new vSiteFundingInformation();
                //Make the insert button visible
                btnInsert.Visible = true;
                //Binds the Mod Numbers
                rcbModNumber_DataBind();
                //Set the index to be the most recent mod
                rcbMod.SelectedIndex = rcbMod.Items.Count() - 1;
                //Set the collection codes
                rcbCollectionCode_DataBind();
                var customer = siftaDB.Agreements.FirstOrDefault(p=>p.AgreementID == AgreementID).Customer;
                //Check to see if it is a JFA 1=JFA
                if (customer.CustomerAgreementTypeID != 1)
                {
                    rntbUSGSCMFFunding.ReadOnly = true;
                    rntbUSGSCMFFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(vSiteFundingInformation))
            {
                //Cast the DataItem as a SiteFunding Item and set siteFunding to be that item.
                siteFunding = (vSiteFundingInformation)DataItem;
                //Make the Update button visible
                btnUpdate.Visible = true;
                //Binds the Mod Numbers
                rcbModNumber_DataBind();
                //Set it to the mods default value
                rcbMod.SelectedValue = siteFunding.AgreementModID.ToString();
                //Get the Collection Code
                var code = siftaDB.lutCollectionCodes.FirstOrDefault(p => p.CollectionCodeID == siteFunding.CollectionCodeID);
                //Set the collection code category = to the one for the site
                rcbCollectionCodeCategory.SelectedValue = code.Category;
                //Databind the collection codes
                rcbCollectionCode_DataBind();
                //Set the selected value to be the one from the site funding
                if (rcbCollectionCode.Items.FirstOrDefault(p => p.Value == siteFunding.CollectionCodeID.ToString()) != null)
                {
                    //The selected value is in the list
                    rcbCollectionCode.SelectedValue = siteFunding.CollectionCodeID.ToString();
                }else
                {
                    //The selected value is an inactive collection code so it must be added to the list before a value is assigned
                    var cc = siftaDB.lutCollectionCodes.FirstOrDefault(p=>p.CollectionCodeID == siteFunding.CollectionCodeID);
                    var item = new RadComboBoxItem() { Value = siteFunding.CollectionCodeID.ToString(), Text = String.Format("{0} - {1}", cc.Code, cc.Description) };
                    rcbCollectionCode.Items.Add(item);
                    rcbCollectionCode.SelectedValue = siteFunding.CollectionCodeID.ToString();
                }
                var customer = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID).Customer;
                //Check to see if it is a JFA 1=JFA
                if (customer.CustomerAgreementTypeID != 1)
                {
                    rntbUSGSCMFFunding.ReadOnly = true;
                    rntbUSGSCMFFunding.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
        private void rcbModNumber_DataBind()
        {
            
            var mods = siftaDB.AgreementMods.Where(p => p.AgreementID == AgreementID);
            foreach(var mod in mods)
            {
                if(mod.Number == 0)
                {
                    rcbMod.Items.Add(new RadComboBoxItem() { Text = "Original Agreement", Value = mod.AgreementModID.ToString() });
                }
                else
                {
                    rcbMod.Items.Add(new RadComboBoxItem() { Text = String.Format("Mod {0}", mod.Number), Value = mod.AgreementModID.ToString() });
                }
            }
        }
        private void rcbCollectionCode_DataBind()
        {
            //Grabs the collection codes 
            // collectionCodes( OrgCode = agreement(AgreementID).customer.orgcode)
            var collectionCodes = siftaDB.lutCollectionCodes.Where(p => p.Active && p.OrgCode == siftaDB.Agreements.FirstOrDefault(a => a.AgreementID == AgreementID).Customer.OrgCode).Where(p => p.Category == rcbCollectionCodeCategory.SelectedValue);
            foreach(var collectionCode in collectionCodes)
            {
                // Value = CollectionCodeID
                // Text  = DQ - Streamflow gaging station with data collection platform (DCP)
                rcbCollectionCode.Items.Add(new RadComboBoxItem() { Text = String.Format("{0} - {1}", collectionCode.Code, collectionCode.Description), Value = collectionCode.CollectionCodeID.ToString() });
            }

        }

        protected void rcbCollectionCodeCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCollectionCode.Items.Clear();
            rcbCollectionCode_DataBind();
        }
    }
}