using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Reports.Thematic
{
    public partial class GroupSiteDetails : System.Web.UI.Page
    {
        private SiftaDBDataContext db = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialDatabind();
            }
        }
        private void InitialDatabind()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Group"]))
            {
                rcbGroup.SelectedValue = Request.QueryString["Group"].ToUpper();
            }
            rcbYear_Rebind();
            if (!String.IsNullOrEmpty(Request.QueryString["Year"]))
            {
                var year = Request.QueryString["Year"];
                foreach (RadComboBoxItem item in rcbYear.Items)
                {
                    if (item.Text == year)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
        private void rcbYear_Rebind()
        {
            rcbYear.Items.Clear();
            //All agreements are of the form YY-GroupID-RandomStuff find the fiscal years by making a substring from 0 to the first index of -
            var years = db.Agreements.Where(p => p.PurchaseOrderNumber.Contains(rcbGroup.SelectedValue)).Select(p => p.PurchaseOrderNumber).Select(p => p.Substring(0, p.IndexOf("-")).Trim()).Distinct().OrderByDescending(p => p).ToList();
            foreach (var year in years)
            {
                //They sometimes code years as 2014 other times as just 14
                var properYear = "";
                if (year.Length == 2) properYear = "20" + year; else properYear = year;
                var comboboxItem = new RadComboBoxItem(properYear, year);
                rcbYear.Items.Add(comboboxItem);
            }
            rcbGroup.DataBind();
        }
        protected void rgResults_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            var year = rcbYear.SelectedValue;
            var group = GetAgreementQuery(rcbGroup.SelectedValue, year);
            rgResults.DataSource = db.spAgreementSetSiteDetail(year, group).OrderBy(p => p.WSC);
            AssignDownloadName();
        }
        protected void rcbGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbYear_Rebind();
            rgResults.Rebind();
        }

        protected void rcbYear_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgResults.Rebind();
        }
        private String GetAgreementQuery(String groupAbbreviation, String year)
        {
            var lookUp = new Dictionary<String, String>()
            {
                {"NSIP", "NSIP-G[A-Z][A-Z][A-Z]"},
                {"USACE", "USACE[A-Z][A-Z][A-Z]-G[A-Z][A-Z][A-Z]"},
                {"IJC", "IJC-G[A-Z][A-Z][A-Z]"},
                {"USGSCRN", "USGSCRN-G[A-Z][A-Z][A-Z]"}
            };
            return String.Format(lookUp[groupAbbreviation], year);
        }
        private void AssignDownloadName()
        {
            rgResults.ExportSettings.FileName = String.Format("{0}_{1}", rcbGroup.SelectedValue, rcbYear.SelectedValue);
        }
        public String GetURL(object o)
        {
            if (o != null)
            {
                return o.ToString().AppendBaseURL();
            }
            else return "";
        }
    }
}