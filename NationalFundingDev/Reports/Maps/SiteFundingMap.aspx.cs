using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports
{
    public partial class SiteFundingMap : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private MapControl map;
        protected void Page_Init(Object sender, EventArgs e)
        {
            map = (MapControl)LoadControl("~/SiftaMapUtils/MapControl.ascx");
            map.Height = 700;
            map.Width = 1000;
            mapPanel.Controls.Clear();
            mapPanel.Controls.Add(map);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var org = Request.QueryString["Org"];
                var fy = Request.QueryString["fy"];
                if(!String.IsNullOrEmpty(org))
                {
                    rcbOrganization.SelectedValue = org;
                }
                if(!string.IsNullOrEmpty(fy))
                {
                    try
                    {
                        var startDate = Convert.ToDateTime(String.Format("10/1/{0}", fy)).AddYears(-1);
                        var endDate = Convert.ToDateTime(String.Format("9/30/{0}", fy));
                        rdpSiteMapStartDate.SelectedDate = startDate;
                        rdpSiteMapEndDate.SelectedDate = endDate;
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            List<Site> sites = new List<Site>();
            List<String> siteNumbers = siftaDB.vSiteFundings.Where(p => p.CustomerGroupAbbreviation == rcbOrganization.SelectedValue && p.EndDate >= rdpSiteMapStartDate.SelectedDate && p.StartDate <= rdpSiteMapEndDate.SelectedDate).Select(p => p.SiteNumber).Distinct().ToList();
            foreach (var siteNumber in siteNumbers)
            {
                var site = siftaDB.Sites.FirstOrDefault(p => p.SiteNumber == siteNumber);
                if (site != null) sites.Add(site);
            }
            map.Sites = sites.Distinct().ToList();
        }
    }
}