using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Reports
{
    public partial class NSIPOperationReport : System.Web.UI.Page
    {
        SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "";
            if(!IsPostBack)
            {
                AddFiscalYearsToDropDown();
            }
        }
        private void AddFiscalYearsToDropDown()
        {
            var currentYear = DateTime.Now.Year;
            // If the month is past October, we are in the next water year
            if (DateTime.Now.Month >= 10) currentYear++;
            // we track from 2007 to the current water year
            for(int year = currentYear; year >= 2007; year--)
            {
                rcbFY.Items.Add(new RadComboBoxItem($"{year} Water Year", year.ToString()));
            }

        }
        protected void rgNSIP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgNSIP.DataSource = siftaDB.spFPSOperationReport(rcbFY.SelectedValue).OrderBy(p => p.Name);
        }
        protected void rcbFY_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgNSIP.Rebind();
        }

        protected void rbMapIt_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Reports/Maps/SiteMap.aspx?Org=FPS&fy={0}", rcbFY.SelectedValue));
        }
        public string AppendBaseURL(String str)
        {
            return str.AppendBaseURL();
        }
        public bool Signed(object obj)
        {
            return obj != null;
        }
    }
}