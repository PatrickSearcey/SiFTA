using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Reports
{
    public partial class CoopFunding : System.Web.UI.Page
    {
        #region Local Variables
        private User user = new User();
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private List<vCooperativeFundingReport> report;
        public Center center;
        #endregion

        #region Page Initialization
        protected void Page_Load(object sender, EventArgs e)
        {
            center = siftaDB.Centers.FirstOrDefault(p => p.OrgCode == Request.QueryString["OrgCode"]);
            if (center == null) Response.Redirect("Default.aspx".AppendBaseURL());
            ltlTitle.Text = String.Format("{0} Cooperative Funding Report", center.Name);
            report = siftaDB.vCooperativeFundingReports.Where(p => p.OrgCode == center.OrgCode).ToList();
            if(!IsPostBack)
            {
                FiscalYearDataBind();
                rcbYear.SelectedValue = CurrentFiscalYear.ToString();
                OfficeFiltersDataBind();
                rcbFormat_SelectedIndexChanged(null, null);
            }
        }
        private void FiscalYearDataBind()
        {
            var years = report.Select(p => p.FiscalYear).Distinct().ToList();
            if (!years.Contains(CurrentFiscalYear)) years.Add(CurrentFiscalYear);
            years = years.OrderByDescending(p => p).ToList();
            foreach(var year in years)
            {
                if(year != 0) rcbYear.Items.Add(new Telerik.Web.UI.RadComboBoxItem() { Text = String.Format("Fiscal Year {0}", year), Value = year.ToString() });
            }
        }
        private void OfficeFiltersDataBind()
        {
            var offices = report.Select(p => p.OfficeCode).Distinct().OrderBy(p => p).ToList();
            foreach(var office in offices)
            {
                if(!String.IsNullOrEmpty(office))rlbOfficeFilters.Items.Add(new Telerik.Web.UI.RadListBoxItem()
                {
                    Text = office,
                    Value = office,
                    Checked = true
                });
            }
        }
        #endregion

        #region rcbYear
        protected void rcbYear_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgCooperativeFunding.Rebind();
        }
        #endregion

        #region Format
        protected void rcbFormat_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cbFiscalYear = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "FiscalYear");
            var cbOffice = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "Office");
            var cbCustomerInfo = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "CustomerInfo");
            var cbAgreementType = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "AgreementType");
            var cbAgreementInfo = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "AgreementInfo");
            var cbSignatures = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "Signatures");
            var cbAccountInfo = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "AccountInfo");
            var cbFundsStatus = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "FundsStatus");
            var cbComments = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "Comments");
            var cbSalesDocument = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "SalesDocument");
            var cbMatchPairCode = rlbColumnSelection.Items.FirstOrDefault(p => p.Value == "MatchPairCode");

            switch(rcbFormat.SelectedValue)
            {
                case "TotalBasic":
                    cbFiscalYear.Checked = false;
                    cbOffice.Checked = true;
                    cbCustomerInfo.Checked = true;
                    cbAgreementType.Checked = false;
                    cbAgreementInfo.Checked = true;
                    cbSignatures.Checked = false;
                    cbAccountInfo.Checked = true;
                    cbFundsStatus.Checked = true;
                    cbComments.Checked = true;
                    cbAgreementType.Checked = true;
                    cbSalesDocument.Checked = true;
                    cbMatchPairCode.Checked = true;
                    break;
                case "TotalBottom":
                    cbFiscalYear.Checked = false;
                    cbOffice.Checked = false;
                    cbCustomerInfo.Checked = false;
                    cbAgreementType.Checked = false;
                    cbAgreementInfo.Checked = false;
                    cbSignatures.Checked = false;
                    cbAccountInfo.Checked = false;
                    cbFundsStatus.Checked = false;
                    cbComments.Checked = false;
                    cbSalesDocument.Checked = false;
                    cbMatchPairCode.Checked = false;
                    break;
                case "UnsignedCustomer":
                    cbFiscalYear.Checked = false;
                    cbOffice.Checked = true;
                    cbCustomerInfo.Checked = true;
                    cbAgreementType.Checked = false;
                    cbAgreementInfo.Checked = true;
                    cbSignatures.Checked = true;
                    cbAccountInfo.Checked = false;
                    cbFundsStatus.Checked = true;
                    cbComments.Checked = true;
                    cbSalesDocument.Checked = false;
                    cbMatchPairCode.Checked = false;
                    break;
                case "Proposed":
                    cbFiscalYear.Checked = false;
                    cbOffice.Checked = true;
                    cbCustomerInfo.Checked = true;
                    cbAgreementType.Checked = true;
                    cbAgreementInfo.Checked = false;
                    cbSignatures.Checked = false;
                    cbAccountInfo.Checked = false;
                    cbFundsStatus.Checked = true;
                    cbComments.Checked = true;
                    cbSalesDocument.Checked = false;
                    cbMatchPairCode.Checked = false;
                    break;
                case "CoopBalance":
                    cbFiscalYear.Checked = false;
                    cbOffice.Checked = false;
                    cbCustomerInfo.Checked = false;
                    cbAgreementType.Checked = true;
                    cbAgreementInfo.Checked = false;
                    cbSignatures.Checked = false;
                    cbAccountInfo.Checked = false;
                    cbFundsStatus.Checked = true;
                    cbComments.Checked = false;
                    cbSalesDocument.Checked = false;
                    cbMatchPairCode.Checked = false;
                    break;
            }
            rgCooperativeFunding.Rebind();
        }
        #endregion

        #region Column Selection
        protected void rlbColumnSelection_CheckAllCheck(object sender, Telerik.Web.UI.RadListBoxCheckAllCheckEventArgs e)
        {
            rcbFormat.SelectedValue = "Custom";
            rgCooperativeFunding.Rebind();
        }

        protected void rlbColumnSelection_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            rcbFormat.SelectedValue = "Custom";
            rgCooperativeFunding.Rebind();
        }
        #endregion

        #region Office Filters
        protected void rlbOfficeFilters_CheckAllCheck(object sender, Telerik.Web.UI.RadListBoxCheckAllCheckEventArgs e)
        {
            rgCooperativeFunding.Rebind();
        }

        protected void rlbOfficeFilters_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            rgCooperativeFunding.Rebind();
        }
        #endregion

        #region Cooperative Funding Grid
        protected void rgCooperativeFunding_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            SetGridColumnVisibility();
            rgCooperativeFunding.DataSource = CooperativeFundingDataSource;
        }
        private DataTable CooperativeFundingDataSource
        {
            get
            {
                var dt = new DataTable();
                using (SqlConnection conn = new SqlConnection("Data Source=IGSKIACWVMGS014;Initial Catalog=siftadb;Integrated Security=True"))
                {
                    var da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(CoopFundingQuery, conn);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return dt;
            }
        }
        private void SetGridColumnVisibility()
        {
            foreach(RadListBoxItem col in rlbColumnSelection.Items)
            {
                switch(col.Value)
                {
                    case "FiscalYear":
                        rgCooperativeFunding.Columns.FindByUniqueName("FiscalYear").Visible = col.Checked;
                        break;
                    case "Office":
                        rgCooperativeFunding.Columns.FindByUniqueName("OfficeCode").Visible = col.Checked;
                        break;
                    case "CustomerInfo":
                        rgCooperativeFunding.Columns.FindByUniqueName("CustomerCode").Visible = col.Checked;
                        rgCooperativeFunding.Columns.FindByUniqueName("CustomerName").Visible = col.Checked;
                        break;
                    case "AgreementType":
                        rgCooperativeFunding.Columns.FindByUniqueName("CustomerAgreementType").Visible = col.Checked;
                        break;
                    case "AgreementInfo":
                        rgCooperativeFunding.Columns.FindByUniqueName("PurchaseOrderNumber").Visible = col.Checked;
                        rgCooperativeFunding.Columns.FindByUniqueName("ModNumber").Visible = col.Checked;
                        break;
                    case "Signatures":
                        rgCooperativeFunding.Columns.FindByUniqueName("SignUSGSDate").Visible = col.Checked;
                        rgCooperativeFunding.Columns.FindByUniqueName("SignCustomerDate").Visible = col.Checked;
                        break;
                    case "AccountInfo":
                        rgCooperativeFunding.Columns.FindByUniqueName("AccountNumber").Visible = col.Checked;
                        rgCooperativeFunding.Columns.FindByUniqueName("AccountName").Visible = col.Checked;
                        break;
                    case "FundsStatus":
                        rgCooperativeFunding.Columns.FindByUniqueName("Status").Visible = col.Checked;
                        break;
                    case "Comments":
                        rgCooperativeFunding.Columns.FindByUniqueName("Remarks").Visible = col.Checked;
                        break;
                    case "MatchPairCode":
                        rgCooperativeFunding.Columns.FindByUniqueName("MatchPairCode").Visible = col.Checked;
                        break;
                    case "SalesDocument":
                        rgCooperativeFunding.Columns.FindByUniqueName("SalesDocument").Visible = col.Checked;
                        break;
                }
            }
            //Unique Case for Format being Coop Funding
            if (rcbFormat.SelectedValue == "CoopBalance")
            {
                rgCooperativeFunding.Columns.FindByUniqueName("FundingCustomer").Visible = false;
                rgCooperativeFunding.Columns.FindByUniqueName("FundingTotal").Visible = false;
                rgCooperativeFunding.Columns.FindByUniqueName("Balance").Visible = true;
            }
            else
            {
                rgCooperativeFunding.Columns.FindByUniqueName("FundingTotal").Visible = true;
                rgCooperativeFunding.Columns.FindByUniqueName("FundingCustomer").Visible = true;
                rgCooperativeFunding.Columns.FindByUniqueName("Balance").Visible = false;
            }
            //Bottom Line
            if (rcbFormat.SelectedValue != "CoopBalance")
            {
                rgCooperativeFunding.Columns.FindByUniqueName("FundingUSGSAllocation").Visible = true;
                (rgCooperativeFunding.Columns.FindByUniqueName("FundingUSGSAllocation") as GridBoundColumn).Aggregate = GridAggregateFunction.Sum;
            }
            else
            {
                rgCooperativeFunding.Columns.FindByUniqueName("FundingUSGSAllocation").Visible = false;
                (rgCooperativeFunding.Columns.FindByUniqueName("FundingUSGSAllocation") as GridBoundColumn).Aggregate = GridAggregateFunction.None;
            }
        }
        private String CoopFundingQuery
        {
            get
            {
                if (rcbFormat.SelectedValue == "TotalBottom")
                {
                    var cmd = "SELECT  COALESCE(dtUSGSCMF.FundingUSGSCMF,0) AS FundingUSGSCMF, COALESCE(dtUSGSAllocation.FundingUSGSAllocation, 0) AS FundingUSGSAllocation, COALESCE(dtCustomer.FundingCustomer, 0) AS FundingCustomer, COALESCE(dtTotal.FundingTotal,0) AS FundingTotal " +
                              "FROM    (SELECT SUM(FundingUSGSCMF) AS FundingUSGSCMF " +
                                        "FROM  siftadb.dbo.vCooperativeFundingReport " +
                                        "WHERE (FiscalYear = '{0}') AND (OrgCode = '{1}')) AS dtUSGSCMF CROSS JOIN " +
                                      "(SELECT SUM(FundingCustomer) AS FundingUSGSAllocation " +
                                        "FROM siftadb.dbo.vCooperativeFundingReport AS vCooperativeFundingReport_3 " +
                                        "WHERE (CustomerAgreementType = 'FED') AND (FiscalYear = '{0}') AND (OrgCode = '{1}')) AS dtUSGSAllocation CROSS JOIN " +
                                      "(SELECT SUM(FundingCustomer) AS FundingCustomer " +
                                        "FROM siftadb.dbo.vCooperativeFundingReport AS vCooperativeFundingReport_2 " +
                                        "WHERE (CustomerAgreementType <> 'FED') AND (FiscalYear = '{0}') AND (OrgCode = '{1}')) AS dtCustomer CROSS JOIN " +
                                      "(SELECT SUM(FundingTotal) AS FundingTotal " +
                                        "FROM siftadb.dbo.vCooperativeFundingReport AS vCooperativeFundingReport_1 " +
                                        "WHERE (FiscalYear = '{0}') AND (OrgCode = '{1}')) AS dtTotal";
                    return String.Format(cmd, rcbYear.SelectedValue, center.OrgCode);
                }
                String select = "SELECT ",
                       from = "FROM [siftadb].dbo.vCooperativeFundingReport ",
                       orderBy = " ",
                       where = String.Format("WHERE (OrgCode = '{0}') AND (FiscalYear = {1})", center.OrgCode, rcbYear.SelectedValue),
                       groupBy = "GROUP BY FiscalYear";
                //Format the Selection and Group By based on the check boxes clicked
                foreach(RadListBoxItem item in rlbColumnSelection.CheckedItems)
                {
                    switch (item.Value)
                    {
                        case "FiscalYear":
                            select += ", FiscalYear";
                            groupBy += ", FiscalYear";
                            break;
                        case "Office":
                            select += ", OfficeCode";
                            groupBy += ", OfficeCode";
                            break;
                        case "CustomerInfo":
                            select += ", CustomerCode";
                            groupBy += ", CustomerCode";
                            select += ", CustomerName";
                            groupBy += ", CustomerName";
                            break;
                        case "AgreementType":
                            select += ", CustomerAgreementType";
                            groupBy += ", CustomerAgreementType";
                            break;
                        case "AgreementInfo":
                            select += ", PurchaseOrderNumber";
                            groupBy += ", PurchaseOrderNumber";
                            select += ", ModNumber";
                            groupBy += ", ModNumber";
                            break;
                        case "Signatures":
                            select += ", SignUSGSDate";
                            groupBy += ", SignUSGSDate";
                            select += ", SignCustomerDate";
                            groupBy += ", SignCustomerDate";
                            break;
                        case "AccountInfo":
                            select += ", AccountNumber";
                            groupBy += ", AccountNumber";
                            select += ", AccountName";
                            groupBy += ", AccountName";
                            break;
                        case "FundsStatus":
                            select += ", Status";
                            groupBy += ", Status";
                            select += ", AccountStatusID";
                            groupBy += ", AccountStatusID";
                            orderBy += "ORDER BY AccountStatusID DESC";
                            break;
                        case "Comments":
                            select += ", Remarks";
                            groupBy += ", Remarks";
                            break;
                        case "MatchPairCode":
                            select += ", MatchPairCode";
                            groupBy += ", MatchPairCode";
                            break;
                        case "SalesDocument":
                            select += ", SalesDocument";
                            groupBy += ", SalesDocument";
                            break;
                    }
                }
                //Proposed Funding
                if (rcbFormat.SelectedValue == "Proposed")
                {
                    where += " AND ([Status] = 'DRAFT' OR [Status] = 'LOW')";
                }
                //Unsigned Customer
                if (rcbFormat.SelectedValue == "UnsignedCustomer")
                {
                    where += " AND ([SignCustomerDate] is null)";
                }
                //No offices were selected
                if(rlbOfficeFilters.CheckedItems.Count == 0)
                {
                    where += " AND (1 = 0)";
                }
                else
                {
                    var officeWhere = " (";
                    //Format the WHERE based on the office codes selected (officeCode = x or OfficeCode = y or...)
                    foreach (RadListBoxItem item in rlbOfficeFilters.CheckedItems)
                    {
                        officeWhere += String.Format("OfficeCode = '{0}' OR ", item.Value);
                    }
                    officeWhere = officeWhere.Substring(0, officeWhere.LastIndexOf(" OR "));
                    officeWhere += ") ";
                    where += String.Format(" AND ({0})", officeWhere);
                }
                //Check to see if it is the coopAvailable balance report add JFA to where clause
                if (rcbFormat.SelectedValue == "CoopBalance")
                {
                    where += String.Format(" AND (CustomerAgreementType = 'JFA')");
                }
                //Add totals to the select
                select += ", SUM(FundingUSGSCMF) AS FundingUSGSCMF, SUM(CASE WHEN CustomerAgreementType = 'FED' THEN FundingCustomer ELSE 0 END) AS FundingUSGSAllocation, SUM(CASE WHEN CustomerAgreementType <> 'FED' THEN FundingCustomer ELSE 0 END) AS FundingCustomer, SUM(FundingTotal) AS FundingTotal ";
                select = select.Replace("SELECT ,", "SELECT ");
                return String.Format("{0} {1} {2} {3} {4}", select, from, where, groupBy, orderBy);
            }
        }
        #endregion

        #region Download Excel
        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    #region Trimming Data Set
                    var ds = CooperativeFundingDataSource;
                    if (ds.Columns.Contains("AccountStatusID"))
                    {
                        ds.Columns.Remove("AccountStatusID");
                    }
                    if (ds.Columns.Contains("CooperativeFundingID"))
                    {
                        ds.Columns.Remove("CooperativeFundingID");
                    }
                    #endregion

                    #region Inserting Data
                    var worksheet = package.Workbook.Worksheets.Add(rcbFormat.Text);
                    //Add data to worksheet
                    for (int colIDX = 0; colIDX < ds.Columns.Count; colIDX++)
                    {
                        for(int rowIDX = 0; rowIDX < ds.Rows.Count; rowIDX++)
                        {
                            var cell = worksheet.Cells[rowIDX + 2, colIDX + 1];
                            cell.Value = ds.Rows[rowIDX][colIDX];
                        }
                    }
                    #endregion

                    #region Adding and Formatting Headers
                    //Add and format Headers
                    for (int idx = 1; idx <= ds.Columns.Count; idx++)
                    {
                        //If it is a date time set that column to be a datetime
                        if (ds.Columns[idx - 1].DataType == typeof(DateTime))
                        {
                            worksheet.Column(idx).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        }
                        if(ds.Columns[idx-1].ColumnName.Contains("Funding"))
                        {
                            worksheet.Column(idx).Style.Numberformat.Format = "$###,###,##0.00";
                        }
                        var cell = worksheet.Cells[1, idx];
                        var name = ds.Columns[idx - 1].ColumnName;
                        name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2").Replace("USGSCMF", "USGS CMF").Replace("USGSA","USGS A");
                        cell.Value = name;
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
                    aboutSection.Add(new Tuple<string, string>("Center", center.Name));
                    aboutSection.Add(new Tuple<string, string>("Reporter", user.ID));
                    aboutSection.Add(new Tuple<string, string>("Date", DateTime.Now.ToString("d")));
                    aboutSection.Add(new Tuple<string, string>("Fiscal Year", rcbYear.SelectedValue));
                    aboutSection.Add(new Tuple<string, string>("Format", rcbFormat.Text));
                    aboutSection.Add(new Tuple<string, string>("Offices", SelectedOffices));
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
                        Response.AddHeader("content-disposition", "attachment; filename=\"" + rcbFormat.Text + "_" + center.Name.Replace(" Water Science Center", "") + "_" + rcbYear.SelectedValue + ".xlsx\"");
                        Response.ContentType = "application/vnd.ms-excel";
                        package.SaveAs(fileStream);
                        Response.End();
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region Properties
        public int CurrentFiscalYear
        {
            get
            {
                if (DateTime.Now.Month >= 10) return DateTime.Now.Year + 1; else return DateTime.Now.Year;
            }
        }
        public string SelectedOffices
        {
            get
            {
                var str = "";
                var selectedOffices = rlbOfficeFilters.CheckedItems;
                foreach(var item in selectedOffices)
                {
                    str += String.Format(",{0} ", item.Text);
                }
                if (String.IsNullOrEmpty(str)) return "None";
                str = str.Substring(1, str.Length - 1);
                return str;
            }
        }
        public double CoopAvailableFunds
        {
            get
            {
                var funds = siftaDB.JFAFundings.FirstOrDefault(p => p.OrgCode == center.OrgCode && p.FiscalYear.ToString() == rcbYear.SelectedValue);
                if (funds != null) return funds.FundingAmount; else return 0;
            }
        }
        public double USGSFundsTotal
        {
            get
            {
                var ds = CooperativeFundingDataSource;
                if(ds.Rows.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(ds.Compute("SUM(FundingUSGSCMF)", null));
                }
            }
        }
        public String CoopFundsString
        {
            get
            {
                if (rcbFormat.SelectedValue == "CoopBalance")
                {
                    return string.Format("{0:c}", CoopAvailableFunds);
                }
                else return "";
            }
        }
        #endregion

        #region Inline Functions
        public String ObjectToString(object item, string field)
        {
            try
            {
                return (item as DataRowView)[field].ToString();
            }catch(Exception ex)
            {
                return "";
            }
        }
        #endregion

        protected void rbDownloadAllCurrentAndFuture_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    #region Trimming Data Set
                    var ds = CooperativeFundingDataSourceAllCurrentAndFutureEntries;
                    if (ds.Columns.Contains("AccountStatusID"))
                    {
                        ds.Columns.Remove("AccountStatusID");
                    }
                    if (ds.Columns.Contains("CooperativeFundingID"))
                    {
                        ds.Columns.Remove("CooperativeFundingID");
                    }
                    #endregion

                    #region Inserting Data
                    var worksheet = package.Workbook.Worksheets.Add(rcbFormat.Text);
                    //Add data to worksheet
                    for (int colIDX = 0; colIDX < ds.Columns.Count; colIDX++)
                    {
                        for (int rowIDX = 0; rowIDX < ds.Rows.Count; rowIDX++)
                        {
                            var cell = worksheet.Cells[rowIDX + 2, colIDX + 1];
                            cell.Value = ds.Rows[rowIDX][colIDX];
                        }
                    }
                    #endregion

                    #region Adding and Formatting Headers
                    //Add and format Headers
                    for (int idx = 1; idx <= ds.Columns.Count; idx++)
                    {
                        //If it is a date time set that column to be a datetime
                        if (ds.Columns[idx - 1].DataType == typeof(DateTime))
                        {
                            worksheet.Column(idx).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        }
                        if (ds.Columns[idx - 1].ColumnName.Contains("Funding"))
                        {
                            worksheet.Column(idx).Style.Numberformat.Format = "$###,###,##0.00";
                        }
                        var cell = worksheet.Cells[1, idx];
                        var name = ds.Columns[idx - 1].ColumnName;
                        name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2").Replace("USGSCMF", "USGS CMF").Replace("USGSA", "USGS A");
                        cell.Value = name;
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
                    aboutSection.Add(new Tuple<string, string>("Center", center.Name));
                    aboutSection.Add(new Tuple<string, string>("Reporter", user.ID));
                    aboutSection.Add(new Tuple<string, string>("Date", DateTime.Now.ToString("d")));
                    aboutSection.Add(new Tuple<string, string>("Fiscal Year", rcbYear.SelectedValue));
                    aboutSection.Add(new Tuple<string, string>("Format", "Current and Future Entries Download"));
                    aboutSection.Add(new Tuple<string, string>("Offices", SelectedOffices));
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
                        Response.AddHeader("content-disposition", "attachment; filename=\"" + rcbQuickDownloadOptions.SelectedValue + "CooperativeFunding" + ".xlsx\"");
                        Response.ContentType = "application/vnd.ms-excel";
                        package.SaveAs(fileStream);
                        Response.End();
                    }
                    #endregion
                }
            }
        }
        private DataTable CooperativeFundingDataSourceAllCurrentAndFutureEntries
        {
            get
            {
                var query = String.Format("SELECT FiscalYear, OfficeCode, CustomerCode, CustomerName, CustomerAgreementType, PurchaseOrderNumber, ModNumber, MatchPairCode, SalesDocument, AccountNumber, AccountName, [Status], AccountStatusID, Remarks , SUM(FundingUSGSCMF) AS FundingUSGSCMF, SUM(CASE WHEN CustomerAgreementType = 'FED' THEN FundingCustomer ELSE 0 END) AS FundingUSGSAllocation, SUM(CASE WHEN CustomerAgreementType <> 'FED' THEN FundingCustomer ELSE 0 END) AS FundingCustomer, SUM(FundingTotal) AS FundingTotal, [ModifiedBy] ,[ModifiedDate] FROM [siftadb].[dbo].[vCoopFundingReportMultiYear] WHERE OrgCode = '{0}' AND FiscalYear >= {1} GROUP BY FiscalYear, OfficeCode, CustomerCode, CustomerName, CustomerAgreementType,PurchaseOrderNumber, ModNumber, MatchPairCode, SalesDocument, AccountNumber, AccountName,[Status], AccountStatusID, Remarks,[ModifiedBy],[ModifiedDate]", center.OrgCode, GetFiscalYear());
                var dt = new DataTable();
                using (SqlConnection conn = new SqlConnection("Data Source=IGSKIACWVMGS014;Initial Catalog=siftadb;Integrated Security=True"))
                {
                    var da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, conn);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return dt;
            }
        }
        private int GetFiscalYear()
        {
            int ret = 1900;
            switch(rcbQuickDownloadOptions.SelectedValue)
            {
                case "All":
                    return 1900;
                case "Current":
                    return DateTime.Now.Year;
                case "Recent&Future":
                    return DateTime.Now.Year - 1;
            }
            return ret;
        }
    }
}