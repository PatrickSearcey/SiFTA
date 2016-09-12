using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace NationalFundingDev.Documents
{
    public partial class Download : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            TypeHandler(Request.QueryString["Type"]);
        }
        /// <summary>
        /// Uses the Doc string to determine what document the user is requesting for download
        /// </summary>
        private void TypeHandler(string doc)
        {
            switch(doc)
            {
                case "JFADocument":
                    if(!String.IsNullOrEmpty(Request.QueryString["a"]))
                    {
                        if(!String.IsNullOrEmpty(Request.QueryString["dumb"]))
                        {
                            GetDocument(JFADocument(true));
                        }else GetDocument(JFADocument());
                    }
                    break;
                case "ReportFundingOverview":
                    if(!String.IsNullOrEmpty(Request.QueryString["OrgCode"]))
                    {
                        var OrgCode = Request.QueryString["OrgCode"];
                        var sf = siftaDB.vReportFundingOverviews.Where(p => p.OrgCode == OrgCode && p.EndDate > DateTime.Now).ToList();
                        var x = new ExcelFundingOverviewReport(sf, OrgCode);
                        x.Download(this.Context, this.Context.Server);
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Creates a JFA Document from the 
        /// </summary>
        /// <returns></returns>
        private JFADocument JFADocument(bool Dumb = false)
        {
            var AgreementID = Convert.ToInt32(Request.QueryString["a"]);
            var doc = new JFADocument(AgreementID, Dumb);
            doc.CenterDirectorID = Request.QueryString["dr"];
            doc.FinancialReviewerID = Request.QueryString["fr"];
            doc.DUNS = Request.QueryString["d"];
            doc.ProjectNumber = Request.QueryString["p"];
            return doc;
        }
        private void GetDocument(JFADocument doc)
        {
            doc.DownloadDocument();
        }
    }
}