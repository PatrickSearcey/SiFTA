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
        protected void Page_Load(object sender, EventArgs e)
        {

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
                btnUpdate.Visible = true;
            }
        }
    }
}