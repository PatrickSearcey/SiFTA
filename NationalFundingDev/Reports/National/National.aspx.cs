using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace NationalFundingDev.Reports.National
{
    public partial class National : System.Web.UI.Page
    {
        private User user = new User();
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        #region Page Load Events
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "";
            if(!IsPostBack)
            {
                rtsNational.SelectedIndex = Selected;
                rmpNational.SelectedIndex = Selected;
            }
        }
        private int Selected
        {
            get
            {
                int ret;
                var s = Request.QueryString["Selected"];
                if (Int32.TryParse(s, out ret))
                {
                    if (ret > rtsNational.Tabs.Count() - 1) return 0; else return ret;
                }
                else return 0;
            }
        }
        #endregion

        #region One Sided Agreements
        protected void rgOneSidedAgreements_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgOneSidedAgreements.DataSource = siftaDB.Agreements.Where(p => SqlMethods.Like(p.SalesDocument, "-%")).Select(p => new { OrgCode = p.Customer.Center.OrgCode, CenterName = p.Customer.Center.Name, CustomerID = p.Customer.CustomerID, CustomerName = p.Customer.Name, AgreementID = p.AgreementID, PurchaseOrderNumber = p.PurchaseOrderNumber, MatchPairCode = p.MatchPairCode, SalesDocument = p.SalesDocument, StartDate = p.StartDate, EndDate = p.EndDate, USGSSignDate = p.AgreementMods.FirstOrDefault(s=>s.Number ==0).SignUSGSDate, CustomerSignDate = p.AgreementMods.FirstOrDefault(c=>c.Number ==0).SignCustomerDate, FundingUSGSCWP = p.AgreementMods.Sum(fusgs=>fusgs.FundingUSGSCWP)  }).ToList();
        }
        #endregion

        #region Collection Codes
        protected void rgCollectionCodes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgCollectionCodes.DataSource = siftaDB.vNationalCollectionCodeUsages.OrderBy(p => p.OrgCode).OrderBy(p => p.Category);
        }
        protected void rbDownloadCollectionCodes_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    #region Trimming Data Set
                    var ds = siftaDB.vNationalCollectionCodeUsages.Where(p=>p.Occurences >0).OrderBy(p => p.Name).ToArray();

                    #endregion

                    #region Inserting Data
                    var worksheet = package.Workbook.Worksheets.Add("National Collection Code Usage");
                    for (int idx = 0; idx < ds.Count(); idx++)
                    {
                        worksheet.Cells[idx + 2, 1].Value = ds[idx].OrgCode;
                        worksheet.Cells[idx + 2, 2].Value = ds[idx].Name;
                        worksheet.Cells[idx + 2, 3].Value = ds[idx].Category;
                        worksheet.Cells[idx + 2, 4].Value = ds[idx].Code;
                        worksheet.Cells[idx + 2, 5].Value = ds[idx].Description;
                        worksheet.Cells[idx + 2, 6].Value = ds[idx].Occurences;
                    }
                    #endregion

                    #region Adding and Formatting Headers
                    var headers = new String[] { "Org Code", "Center", "Category", "Code", "Description", "Occurences" };
                    //Add and format Headers
                    for (int idx = 1; idx <= headers.Count(); idx++)
                    {
                        var cell = worksheet.Cells[1, idx];

                        cell.Value = headers[idx - 1];
                        cell.Style.Font.Bold = true;
                        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }
                    #endregion

                    //Auto fit the cells width
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    #region About Section
                    var about = package.Workbook.Worksheets.Add("About");
                    var aboutSection = new List<Tuple<String, String>>();
                    aboutSection.Add(new Tuple<string, string>("Reporter", user.ID));
                    aboutSection.Add(new Tuple<string, string>("Date", DateTime.Now.ToString("d")));
                    for (int idx = 0; idx < aboutSection.Count; idx++)
                    {
                        about.Cells[idx + 1, 1].Value = aboutSection[idx].Item1 + ": ";
                        about.Cells[idx + 1, 1].Style.Font.Bold = true;
                        about.Cells[idx + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        about.Cells[idx + 1, 2].Value = aboutSection[idx].Item2;
                    }
                    about.Cells[about.Dimension.Address].AutoFitColumns();
                    #endregion

                    #region Write Out to Response
                    //Write the file out
                    using (Stream fileStream = Response.OutputStream)
                    {
                        Response.AddHeader("content-disposition", "attachment; filename=\"NationalCollectionCodeUsage_" + DateTime.Now.ToString() + ".xlsx\"");
                        Response.ContentType = "application/vnd.ms-excel";
                        package.SaveAs(fileStream);
                        Response.End();
                    }
                    #endregion
                }
            }
        } 
        #endregion

        public string AppendBaseURL(string s)
        {
            return s.AppendBaseURL();
        }
    }
}