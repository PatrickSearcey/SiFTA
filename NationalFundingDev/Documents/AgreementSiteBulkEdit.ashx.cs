using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NationalFundingDev.Documents
{
    /// <summary>
    /// Summary description for AgreementSiteBulkEdit
    /// </summary>
    public class AgreementSiteBulkEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var siftaDB = new SiftaDBDataContext();
            string agID = context.Request.QueryString["AgreementID"];
            if(agID == null)
            {
                context.Response.Write("Invalid AgreementID");
            }
            var entries = siftaDB.vSiteFundingInformations.Where(x => x.AgreementID == int.Parse(agID));

            var modID = siftaDB.Agreements.FirstOrDefault(x => x.AgreementID == int.Parse(agID));
            var pID = modID.PurchaseOrderNumber;

            //Create an excel package from the file stream
            using (ExcelPackage package = new ExcelPackage())
            {
                //A workbook must have at least on cell, so lets add one... 
                var ws = package.Workbook.Worksheets.Add(pID);

                //To set values in the spreadsheet use the Cells indexer.
                ws.Cells["A1"].Value = "SiteName";
                ws.Cells["B1"].Value = "SiteNumber";
                ws.Cells["C1"].Value = "CollectionCode";
                ws.Cells["D1"].Value = "Units";
                ws.Cells["E1"].Value = "DifficultyFactor";
                ws.Cells["F1"].Value = "USGSCMFFunding";
                ws.Cells["G1"].Value = "CustomerFunds";
                ws.Cells["H1"].Value = "StartDate";
                ws.Cells["I1"].Value = "EndDate";
                ws.Cells["J1"].Value = "Remarks";

                int i = 2;
                foreach (var entry in entries)
                {
                    ws.Cells["A" + i].Value = entry.SiteName;
                    ws.Cells["B" + i].Value = entry.SiteNumber;
                    ws.Cells["C" + i].Value = entry.Code;
                    ws.Cells["D" + i].Value = entry.CollectionUnits;
                    ws.Cells["E" + i].Value = entry.DifficultyFactor;
                    ws.Cells["F" + i].Value = entry.FundingUSGSCMF;
                    ws.Cells["G" + i].Value = entry.FundingCustomer;
                    ws.Cells["H" + i].Value = entry.StartDate.ToString().Replace(" 12:00:00 AM", "");
                    ws.Cells["I" + i].Value = entry.EndDate.ToString().Replace(" 12:00:00 AM", "");
                    ws.Cells["J" + i].Value = entry.Remarks;

                    i++;
                }

                // Changed it to not depend on a D drive
                var dir = new DirectoryInfo(context.Server.MapPath("~/Temporary"));
                if (!dir.Exists)
                {
                    dir.Create();
                }

                package.Save();
                // Create an id for the temporary file
                var id = Guid.NewGuid().ToString();
                var tempPath = Path.Combine(dir.FullName, $"{id}.xlsx");
                var tempFile = new FileInfo(tempPath);
                package.SaveAs(tempFile);

                //Write excel file to http web response 
                context.Response.Clear();
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                context.Response.AddHeader("content-disposition", String.Format("attachment;  filename=agreements" + agID + ".xlsx"));
                context.Response.BinaryWrite(File.ReadAllBytes(tempFile.FullName));
                context.Response.Flush();
                // Added to clean up file after it has finished downloading
                tempFile.Delete();
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}