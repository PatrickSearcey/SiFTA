using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Controls.RadGrid
{
    public partial class ContactEditForm : System.Web.UI.UserControl
    {
        #region DataItem Stuff
        private object _dataItem = null;
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }
        #endregion
        public CustomerContact contact;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Insert
            if (DataItem is GridInsertionObject)
            {
                contact = new CustomerContact();
                btnInsert.Visible = true;
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(CustomerContact))
            {
                //Cast the DataItem as a Customer Contact and store it in contact
                contact = (CustomerContact)DataItem;
                btnUpdate.Visible = true;
                rcbSalutation.SelectedValue = contact.Salutation;
            }
        }
    }
}