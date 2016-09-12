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
                rcbFY.DataBind();
                foreach(RadComboBoxItem item in rcbFY.Items)
                {
                    item.Text = item.Text + " Water Year";
                }
            }
        }
        protected void rgNSIP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgNSIP.DataSource = siftaDB.spNSIPOperationReport(rcbFY.SelectedValue).OrderBy(p => p.Name);
        }
        protected void rcbFY_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgNSIP.Rebind();
        }

        protected void rbMapIt_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Reports/Maps/SiteMap.aspx?Org=NSIP&fy={0}", rcbFY.SelectedValue));
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