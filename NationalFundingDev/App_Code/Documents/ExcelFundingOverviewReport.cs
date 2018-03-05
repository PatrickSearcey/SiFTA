using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using OfficeOpenXml;

namespace NationalFundingDev
{
    public class ExcelFundingOverviewReport
    {
        private List<vReportFundingOverview> _data;
        private String _orgCode;
        private String CenterName = "";
        public ExcelFundingOverviewReport(List<vReportFundingOverview> data, String OrgCode)
        {
            _data = data;
            _orgCode = OrgCode;
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            var center = siftaDB.Centers.FirstOrDefault(p => p.OrgCode == _orgCode);
            if (center != null) CenterName = center.State + "_WSC";
        }
        public void Download(HttpContext context, HttpServerUtility utility)
        {
            var path = utility.MapPath("/NationalFundingDev/Documents/Forms/FundingOverviewReportTemplate.xlsx");
            if (!File.Exists(path)) throw new FileNotFoundException(String.Format("The Template was not found for path: {0}", path));
            var excelBytes = File.ReadAllBytes(path);
            using(MemoryStream ms = new MemoryStream(excelBytes))
            {
                using(ExcelPackage package = new ExcelPackage(ms))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    FillData(ref worksheet);
                    //Write the file out
                    using (Stream fileStream = context.Response.OutputStream)
                    {
                        context.Response.AddHeader("content-disposition", "attachment; filename=\"" + CenterName + "_FundingOverview.xlsx\"");
                        context.Response.ContentType = "application/vnd.ms-excel";
                        package.SaveAs(fileStream);
                    }
                    context.Response.End();
                }
            }
        }

        private void FillData(ref ExcelWorksheet worksheet)
        {
            //Counts the row
            var idx = 2;
            foreach(var row in _data)
            {
                worksheet.Cells[String.Format("A{0}", idx)].Formula = String.Format("HYPERLINK(\"{0}\",\"{1}\")", "https://sifta.water.usgs.gov/NationalFunding/Customer.aspx?CustomerId=" + row.CustomerID.ToString(), row.CustomerName);
                worksheet.Cells[String.Format("A{0}", idx)].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(40, 149, 168));
                worksheet.Cells[String.Format("B{0}", idx)].Formula = String.Format("HYPERLINK(\"{0}\",\"{1}\")", "https://sifta.water.usgs.gov/NationalFunding/Agreement.aspx?AgreementId=" + row.AgreementID.ToString(), row.PurchaseOrderNumber);
                worksheet.Cells[String.Format("B{0}", idx)].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(40,149,168));
                worksheet.Cells[String.Format("C{0}", idx)].Value = row.CustomerCode;
                worksheet.Cells[String.Format("D{0}", idx)].Value = row.SalesDocument;
                worksheet.Cells[String.Format("E{0}", idx)].Value = row.StartDate;
                worksheet.Cells[String.Format("F{0}", idx)].Value = row.EndDate;
                worksheet.Cells[String.Format("G{0}", idx)].Value = row.SignUSGSDate;
                worksheet.Cells[String.Format("H{0}", idx)].Value = row.SignCustomerDate;
                worksheet.Cells[String.Format("I{0}", idx)].Value = row.FundsType;
                worksheet.Cells[String.Format("J{0}", idx)].Value = row.BillingCycleFrequency;
                worksheet.Cells[String.Format("K{0}", idx)].Value = row.FundingUSGSCMFSum;
                worksheet.Cells[String.Format("L{0}", idx)].Value = row.FundingCustomerSum;
                worksheet.Cells[String.Format("M{0}", idx)].Value = row.FundingOtherSum;
                worksheet.Cells[String.Format("N{0}", idx)].Value = row.FundingCustomerSum + row.FundingOtherSum + row.FundingUSGSCMFSum;
                idx++;
            }
            if(_data.Count() >= 1)
            {
                worksheet.Column(1).Width = _data.OrderByDescending(p => p.CustomerName.Length).First().CustomerName.Length;
            }
        }
    }
}