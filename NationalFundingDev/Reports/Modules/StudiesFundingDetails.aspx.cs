using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Modules
{
    public partial class StudiesFundingDetails : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public FundingStudy studyFunding;
        protected void Page_Load(object sender, EventArgs e)
        {
            var fundingStudiesID = Convert.ToInt32(Request.QueryString["FundingStudyID"]);
            studyFunding = siftaDB.FundingStudies.FirstOrDefault(p => p.FundingStudyID == fundingStudiesID);
            if (studyFunding == null) Response.Redirect("closePage.html".AppendBaseURL());
        }
    }
}