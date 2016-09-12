using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Metrics
{
    public partial class National : System.Web.UI.Page
    {
        #region Local Variables
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PageDataBind();
            }
        }
        #endregion

        #region Page Events
        protected void rcbTimeRange_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PageDataBind();
        }
        #endregion

        #region Page Binding
        private void PageDataBind()
        {
            var bd = BaseData;
            var tc = TotalsCount;
            var dc = DaysCount;
            BindTotalsChart(tc);
            BindUserCommunityPieChart(bd);
            BindCenterActivityPieChart(bd);
            BindDayActivityChart(dc);
        }

        private void BindDayActivityChart(DataTable dc)
        {
            rchartDayBreakDown.DataSource = dc;
            rchartDayBreakDown.DataBind();
        }
        private void BindTotalsChart(DataTable totals)
        {
            rchartTotals.DataSource = totals;
            rchartTotals.DataBind();
        }
        private void BindUserCommunityPieChart(List<vMetric> bd)
        {
            var ds = bd.Where(p=>p.EmployeeTitle != null).GroupBy(p => p.EmployeeTitle.Trim()).Select(g => new { Title = g.Key.Trim(), Count = g.Count() }).OrderBy(p=>p.Title).ToDictionary(p => p.Title, p => p.Count);
            rchartUserCommunity.DataSource = ds;
            rchartUserCommunity.DataBind();
        }
        private void BindCenterActivityPieChart(List<vMetric> bd)
        {
            var ds = bd.GroupBy(p => p.CenterName).Select(g => new { Title = g.Key, Count = g.Count() }).ToDictionary(p => p.Title, p => p.Count);
            rchartCenterParticipation.DataSource = ds;
            rchartCenterParticipation.DataBind();
        }
        #endregion

        #region Grabbing Data
        /// <summary>
        /// Gets Base Data set for the metrics that meet the date Criteria. 
        /// </summary>
        private List<vMetric> BaseData
        {
            get
            {
                //If the user selected all, return everything in the database.
                if (rcbTimeRange.SelectedValue == "all") return siftaDB.vMetrics.ToList();
                //If the user wants todays data then just return the records created today
                if (rcbTimeRange.SelectedValue == "today") return siftaDB.vMetrics.Where(p => p.RecordedDate >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).ToList();
                //Get all records that exist past the current date minus the date range specified by the user.
                return siftaDB.vMetrics.Where(p => p.RecordedDate >= DateTime.Now.AddDays(-(Convert.ToDouble(rcbTimeRange.SelectedValue)))).ToList();
            }
        }
        private DataTable TotalsCount
        {
            get
            {
                var ret = new DataTable();
                ret.Columns.Add("UniqueUsers");
                ret.Columns.Add("RecordsAdded");
                ret.Columns.Add("RecordsUpdated");
                ret.Columns.Add("RecordsDeleted");
                ret.Columns.Add("AgreementsCopied");
                ret.Columns.Add("JFADownloads");
                var bd = BaseData;
                var row = ret.NewRow();
                row["UniqueUsers"] = bd.Select(p => p.EmployeeName).Distinct().Count();
                row["RecordsAdded"] = bd.Where(p => p.MetricTypeID == MetricType.RecordAdded).Count();
                row["RecordsUpdated"]= bd.Where(p => p.MetricTypeID == MetricType.RecordUpdate).Count();
                row["RecordsDeleted"] = bd.Where(p => p.MetricTypeID == MetricType.RecordRemoved).Count();
                row["AgreementsCopied"] = bd.Where(p => p.MetricTypeID == MetricType.RecordCopied).Count();
                row["JFADownloads"] = bd.Where(p => p.MetricTypeID == MetricType.JFADownload).Count();
                ret.Rows.Add(row);
                return ret;
            }
        }
        private DataTable DaysCount
        {
            get
            {
                var ret = new DataTable();
                ret.Columns.Add("Sunday");
                ret.Columns.Add("Monday");
                ret.Columns.Add("Tuesday");
                ret.Columns.Add("Wednesday");
                ret.Columns.Add("Thursday");
                ret.Columns.Add("Friday");
                ret.Columns.Add("Saturday");
                var bd = BaseData;
                var row = ret.NewRow();
                row["Sunday"] = bd.Where(p=>p.Day == "Sunday").Count();
                row["Monday"] = bd.Where(p => p.Day == "Monday").Count();
                row["Tuesday"] = bd.Where(p => p.Day == "Tuesday").Count();
                row["Wednesday"] = bd.Where(p => p.Day == "Wednesday").Count();
                row["Thursday"] = bd.Where(p => p.Day == "Thursday").Count();
                row["Friday"] = bd.Where(p => p.Day == "Friday").Count();
                row["Saturday"] = bd.Where(p => p.Day == "Saturday").Count();
                ret.Rows.Add(row);
                return ret;
            }
        }
        #endregion
    }
}