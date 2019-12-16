using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace NationalFundingDev.Controls.RadGrid
{
    public partial class CustomerControl : System.Web.UI.UserControl
    {
        public List<String> emptyList = new List<string>();
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public Customer customer;

        #region DataItem Stuff
        /// <summary>
        /// Holds the data item passed from the grid to the control.
        /// This one will be the Customer object of the customer being edited
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
            var dir = FileDirectoryHelper.GetTempDirectory();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            rauImage.TargetFolder = dir;
            //Insert
            if (DataItem is GridInsertionObject)
            {
                //Set customer to a new Customer object
                customer = new Customer();
                //Show the Insert Button
                btnInsert.Visible = true;
            }
            //Update
            else if (DataItem != null && DataItem.GetType() == typeof(Customer))
            {
                //Set customer to the dataitem cast as a Customer object being passed in from the grid
                customer = DataItem as Customer;
                //Show Update Button
                btnUpdate.Visible = true;
                //Set the Agreement Type selcted value to what the customers agreement type is
            }
        }
    }
}