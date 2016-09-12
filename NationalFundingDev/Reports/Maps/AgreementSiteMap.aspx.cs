using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Maps
{
    public partial class AgreementSiteMap : System.Web.UI.Page
    {
        private int defaultSize = 500;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var map = (MapControlClean)LoadControl("~/SiftaMapUtils/MapControlClean.ascx");
            map.Height = Height;
            map.Width = Width;
            var sites = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID == AgreementID).Select(p => p.SiteNumber).Distinct().ToList();
            var siteList = new List<Site>();
            foreach(var site in sites)
            {
                var s = siftaDB.Sites.FirstOrDefault(p => p.SiteNumber == site);
                if (s != null) siteList.Add(s);
            }
            map.Sites = siteList;
            phMap.Controls.Add(map);
        }
        public int Width
        {
            get
            {
                int v;
                var temp = Request.QueryString["Width"];
                if (String.IsNullOrEmpty(temp)) return defaultSize;
                if (int.TryParse(temp, out v)) return v; else return defaultSize;
            }
        }
        public int Height
        {
            get
            {
                int v;
                var temp = Request.QueryString["Height"];
                if (String.IsNullOrEmpty(temp)) return defaultSize;
                if (int.TryParse(temp, out v)) return v; else return defaultSize;
            }
        }
        public int AgreementID
        {
            get
            {
                int v;
                var temp = Request.QueryString["AgreementID"];
                if (String.IsNullOrEmpty(temp)) return 0;
                if (int.TryParse(temp, out v)) return v; else return 0;
            }
        }
    }
}