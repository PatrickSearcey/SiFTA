using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
namespace NationalFundingDev
{
    public partial class CenterReport : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private Center center;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the Center
            center = siftaDB.Centers.FirstOrDefault(p => p.OrgCode == Request.QueryString["OrgCode"]);
            //If the center doesn't exist return them to the default page
            if (center == null) Response.Redirect("Default.aspx".AppendBaseURL());
            //Set the Session variable Title 
            Session["Title"] = center.Name.Replace(" Water Science Center", " ");
            //Set the title
            ltlTitle.Text = String.Format("{0} Report", center.Name);
            if(!IsPostBack)
            {
                rdpFOStart.SelectedDate = DateTime.Now;
                rdpAgreementStatusEndDate.SelectedDate = DateTime.Now;
                rdpUnfundedRTSitesDate.SelectedDate = DateTime.Now;
                rdpAgreementDifference.SelectedDate = DateTime.Now;
                rdpAgreementsMissingDocuments.SelectedDate = DateTime.Now;
            }
        }

        #region Tab Clicking
        protected void rtsCenterReportOptions_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Tabs.Count > 0)
            {
                //make the first child selected
                e.Tab.Tabs.First().Selected = true;
                //Set the multipage to show the selected childs tabindex
                rmpCenterReportOptions.SelectedIndex = e.Tab.Tabs.First().TabIndex;
            }
            else
            {
                //Set the multipage to show the selected Page
                rmpCenterReportOptions.SelectedIndex = e.Tab.TabIndex;
            }
            DataRebind();
        }
        private void DataRebind()
        {
            switch(rmpCenterReportOptions.SelectedIndex)
            {
                case 0:
                    rgUnfundedSites.Rebind();
                    break;
                case 1:
                    rgFundingOverview.Rebind();
                    break;
                case 2:
                    rgContacts.Rebind();
                    break;
                case 3:
                    rgCollectionCodes.Rebind();
                    break;
                case 4:
                    rgCustomersMissingIcons.Rebind();
                    break;
                case 5:
                    rgAgreementsMissingDocuments.Rebind();
                    break;
                case 6:
                    rgAgreementStatus.Rebind();
                    break;
                case 7:
                    rgAgreementDifference.Rebind();
                    break;
            }
        }
        #endregion

        #region Unfunded Real-Time Site
        protected void rgUnfundedSites_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DateTime dt;
            if (rdpUnfundedRTSitesDate.SelectedDate != null) dt = Convert.ToDateTime(rdpUnfundedRTSitesDate.SelectedDate); else dt = DateTime.Now;
            try
            {
                Dictionary<String, String> fundedSites = siftaDB.vSiteFundings.Where(p => p.EndDate >= dt).Select(p => p.SiteNumber).Distinct().ToDictionary(p => p.Trim(), p => p.Trim());
                var rtSites = siftaDB.Sites.Where(p => p.OrgCode == center.OrgCode && p.RealTime == true).ToList();
                var finalSites = rtSites.Where(p => !fundedSites.ContainsKey(p.SiteNumber));
                rgUnfundedSites.DataSource = finalSites;
                ltlRTSiteUnfundedNumber.Text = finalSites.Count().ToString();
            }
            catch(Exception ex)
            {
                rgUnfundedSites.DataSource = new List<string>();
                ltlRTSiteUnfundedNumber.Text = "0";
            }
            
        }

        protected void rdpUnfundedRTSitesDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgUnfundedSites.Rebind();
        }

        protected void btnViewMap_Click(object sender, EventArgs e)
        {
            Response.Redirect("../National/RealTimeSitesFunding.aspx");
        }
        #endregion

        #region Funding Overview
        protected void rbFundingOverviewDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("Documents/Download.aspx?Type=ReportFundingOverview&OrgCode={0}", center.OrgCode).AppendBaseURL());
        }
        protected void rgFundingOverview_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var data = siftaDB.vReportFundingOverviews.Where(p => p.OrgCode == center.OrgCode);
            if(rdpFOStart.SelectedDate > rdpFOEnd.SelectedDate)
            {
                rdpFOEnd.SelectedDate = rdpFOStart.SelectedDate;
            }
            if(rdpFOEnd.SelectedDate != null)
            {
                data = data.Where(p => p.EndDate <= rdpFOEnd.SelectedDate);
            }
            if(rdpFOStart.SelectedDate != null)
            {
                data = data.Where(p => p.EndDate >= rdpFOStart.SelectedDate);
            }
            rgFundingOverview.DataSource = data.ToList();
            ltlAgreementsEnding.Text = data.Count().ToString();
        }
        protected void agreementEndingChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgFundingOverview.Rebind();
        }
        #endregion

        #region Contacts
        protected void rgContacts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgContacts.DataSource = siftaDB.vCustomerContacts.Where(p => p.OrgCode == center.OrgCode).OrderBy(p => p.CustomerName);
        }
        #endregion

        #region Collection Codes
        protected void rgCollectionCodes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCollectionCodes.DataSource = siftaDB.vReportCollectionCodes.Where(p => p.OrgCode == center.OrgCode).OrderBy(p => p.ProperCategory).OrderBy(p => p.ProperName);
        }
        #endregion

        #region Exceptions

        #region Missing Icons
        protected void rgCustomersMissingIcons_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCustomersMissingIcons.DataSource = siftaDB.Customers.Where(p => p.OrgCode == center.OrgCode && p.Icon == null);
        }
        #endregion

        #region Agreements Missing Documents
        protected void rdpAgreementsMissingDocuments_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgAgreementsMissingDocuments.Rebind();
        }
        protected void rgAgreementsMissingDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Holds all the agreements that don't have documents
            var ds = new Dictionary<int, String>();
            //Grabs all the JFA agreements for this center
            var dt = rdpAgreementsMissingDocuments.SelectedDate;
            if (dt == null) dt = new DateTime(1800, 1, 25);
            var agreements = siftaDB.vReportAgreementDocuments.Where(p => p.Type != "FED" && p.OrgCode == center.OrgCode && p.EndDate >= dt).ToDictionary(p => p.AgreementID, p => p.PurchaseOrderNumber);
            //THe base path for all Documents
            foreach(var agreement in agreements)
            {
                var path = String.Format("D:\\siftaroot\\Documents\\Agreements\\{0}\\{1}.pdf", agreement.Key, agreement.Value);
                if(!File.Exists(path))
                {
                    ds.Add(agreement.Key, agreement.Value);
                }
            }
            rgAgreementsMissingDocuments.DataSource = ds;
        }
        #endregion

        #region Agreement Status
        protected void rdpAgreementStatusEndDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgAgreementStatus.Rebind();
        }
        protected void rgAgreementStatus_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var status = siftaDB.vAgreementStatus.Where(p => p.Request == null || p.ReviewReturn == null || p.ReviewSent == null || p.SignCustomerDate == null || p.SignUSGSDate == null).Where(p=>p.OrgCode == center.OrgCode).ToList();
            if (rdpAgreementStatusEndDate.SelectedDate != null) status = status.Where(p => Convert.ToDateTime(p.EndDate) >= rdpAgreementStatusEndDate.SelectedDate).ToList();
            rgAgreementStatus.DataSource = status;
        }
        protected void btnClearFilters_Click(object sender, EventArgs e)
        {
            foreach(GridColumn column in rgAgreementStatus.MasterTableView.OwnerGrid.Columns)
            {
                column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                column.CurrentFilterValue = string.Empty;
            }
            rgAgreementStatus.MasterTableView.FilterExpression = string.Empty;
            rgAgreementStatus.Rebind();
        }
        #endregion

        #region Agreement Difference
        protected void rbRefreshAgreementDifference_Click(object sender, EventArgs e)
        {
            rgAgreementDifference.Rebind();
        }
        protected void rdpAgreementDifference_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgAgreementDifference.Rebind();
        }
        protected void rgAgreementDifference_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var ds = siftaDB.vAgreementModDifferences.Where(p => p.OrgCode == center.OrgCode).Where(p => p.CustomerDifference != 0 || p.USGSCMFDifference != 0 || p.OtherDifference != 0).OrderBy(p=>p.Name).ToList();
            if (rdpAgreementDifference.SelectedDate != null) ds = ds.Where(p => p.EndDate >= rdpAgreementDifference.SelectedDate).ToList();
            rgAgreementDifference.DataSource = ds;
        }
        protected void rgAgreementDifference_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var ColumnNames = new List<String>() { "USGS", "Customer", "Other", "Total" };
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //Iterate throught the columns
                foreach (string ColumnName in ColumnNames)
                {
                    Double val;
                    if (Double.TryParse(item[ColumnName].Text.Replace("$", "").Replace(",", "").Replace("(", "-").Replace(")", ""), out val))
                    {
                        //The value is higher, Recorded > Allocated
                        if (val > 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFCEC2");
                        }
                        //The value is lower, Allocated > Recorded
                        if (val < 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFFFAD");
                        }
                    }
                }
            }
        }
        protected void rgAgreementDifference_PreRender(object sender, EventArgs e)
        {
            var ColumnNames = new List<String>() { "USGS", "Customer", "Other", "Total" };
            foreach(GridDataItem item in rgAgreementDifference.Items)
            {
                //Iterate throught the columns
                foreach (string ColumnName in ColumnNames)
                {
                    Double val;
                    if (Double.TryParse(item[ColumnName].Text.Replace("$", "").Replace(",", "").Replace("(", "-").Replace(")", ""), out val))
                    {
                        //The value is higher, Recorded > Allocated
                        if (val > 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFCEC2");
                        }
                        //The value is lower, Allocated > Recorded
                        if (val < 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFFFAD");
                        }
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Inline Code
        public string AppendBaseURL(String path)
        {
            return path.AppendBaseURL();
        }
        public String FormatPhone(object obj)
        {
            if (obj == null) return "";
            return obj.ToString().ToPhoneFormat();
        }
        public String DateLink(object agreementID, object date, int target)
        {
            //If the date isn't null set it to the date and return it
            if (date != null)
            {
                return String.Format("<b>{0}</b>", Convert.ToDateTime(date).ToString("d"));
            }
            else //The date is null return an add as a link with the proper link
            {
                var url = String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreementID.ToString(), target).AppendBaseURL();
                return String.Format("<center><a style='color: #4a95a1;' href='{0}' target='_blank'>Add</a></center>", url);
            }  
        }
        #endregion

        
    }
}