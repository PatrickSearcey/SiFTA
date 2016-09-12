using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Modules
{
    public partial class SiteFundingDetails : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public FundingSite siteFunding;
        public vSiteFundingInformation siteFundingInformation;
        protected void Page_Load(object sender, EventArgs e)
        {
            var fundingSiteID = Convert.ToInt32(Request.QueryString["FundingSiteID"]);
            siteFunding = siftaDB.FundingSites.FirstOrDefault(p => p.FundingSiteID == fundingSiteID);
            siteFundingInformation = siftaDB.vSiteFundingInformations.FirstOrDefault(p => p.FundingSiteID == fundingSiteID);
            if (siteFunding == null) Response.Redirect("closePage.html".AppendBaseURL());
        }
    }
}