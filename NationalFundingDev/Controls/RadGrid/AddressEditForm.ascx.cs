using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class AddressEditForm : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public CustomerContactAddress address;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Insert
            if (DataItem is GridInsertionObject)
            {
                address = new CustomerContactAddress();
                btnInsert.Visible = true;
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(CustomerContactAddress))
            {
                //Cast the DataItem as a Customer Contact and store it in contact
                address = (CustomerContactAddress)DataItem;
                btnUpdate.Visible = true;
                rcbAddressType.SelectedValue = address.Type;
            }
        }
    }
}