using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace NationalFundingDev
{
    public class MetricHandler
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private User user = new User();
        int month, date, year, week;
        string day;
        DateTime dt;

        public MetricHandler()
        {
        }
        public MetricHandler(String OrgCode, int? CustomerID, int? AgreementID, int TypeID, string SourceType,  string Remarks)
        {
            GetDateTimeData();
            siftaDB.Metrics.InsertOnSubmit(new Metric() { SourceID = "", OrgCode = OrgCode, CustomerID = CustomerID, AgreementID = AgreementID, MetricTypeID = TypeID, SourceRemarks = Remarks, RecordedBy = user.ID, RecordedDate = dt, Date = date, Month = month, Year = year, Day = day, Week = week, SourceType = SourceType });
            
        }
        public void SubmitChanges()
        {
            siftaDB.SubmitChanges();
        }
        private void GetDateTimeData()
        {
            dt = DateTime.Now;
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var cal = dfi.Calendar;
            date = cal.GetDayOfMonth(dt);
            month = cal.GetMonth(dt);
            year = cal.GetYear(dt);
            week = cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            day = dt.DayOfWeek.ToString();
        }
    }
    public static class MetricType
    {
        public static int JFADownload { get { return 1; } }
        public static int RecordAdded { get { return 2; } }
        public static int RecordUpdate { get { return 3; } }
        public static int RecordRemoved { get { return 4; } }
        public static int PageVisited { get { return 5; } }
        public static int RecordCopied { get { return 6; } }
    }
}