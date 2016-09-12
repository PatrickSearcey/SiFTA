using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Maps
{
    public partial class SiteMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "";
            mapFrame.Src = String.Format("SiteFundingMap.aspx?Org={0}&fy={1}", Request.QueryString["Org"], Request.QueryString["fy"]);
        }
    }
}