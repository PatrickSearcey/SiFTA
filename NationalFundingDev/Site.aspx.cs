using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev
{
    public partial class SitePage : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public Site site;
        public User user = new User();
        public List<vSiteFunding> funding;
        private List<String> fiscalYears;

        #region Page Load Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab site number from URL
            var siteNumber = Request.QueryString["SiteNumber"];
            // Grab the site from the database
            site = siftaDB.Sites.FirstOrDefault(p => p.SiteNumber == siteNumber);
            //If the site doesn't exist redirect them
            if (site == null)
            {
                //If they typed in their own site number send them back to the main page
                if (String.IsNullOrEmpty(Request.UrlReferrer.ObjToString())) Response.Redirect("Default.aspx");
                //They were directed here by some part of the site, send them back to that site
                Response.Redirect(Request.UrlReferrer.ObjToString());
            }
            //Store the funding information for the site in the public variable funding
            funding = siftaDB.vSiteFundings.Where(p => p.SiteNumber == site.SiteNumber).ToList();
            //Grabs the fiscal years List
            GetFiscalYearsList();
            if(!IsPostBack)
            {
                BindFiscalYearComboBox();
            }
        }
        /// <summary>
        /// Takes the sites funding information stored in funding and stores the fiscal years in fiscalYears
        /// </summary>
        private void GetFiscalYearsList()
        {
            fiscalYears = new List<string>();
            foreach(var fund in funding)
            {
                //Stores the lower FY
                int lowFY = FiscalYear(fund.StartDate);
                //Stores the higher FY
                int hiFY = FiscalYear(fund.EndDate);
                //Check to see that at least one of them has a fiscal year
                if(lowFY != 0 || hiFY !=0)
                {
                    //Both have fiscal years
                    if(lowFY != 0 && hiFY !=0)
                    {
                        //Add each year to the fiscal years list
                        for(int fy = lowFY; lowFY <= hiFY; lowFY++)
                        {
                            fiscalYears.Add(fy.ToString());
                        }
                    }
                    else
                    {
                        //Only one has a fiscal year add both together to get that year
                        fiscalYears.Add((lowFY + hiFY).ToString());
                    }
                }
            }
            //Select Distinct fiscal years and order them in descending order
            fiscalYears = fiscalYears.Distinct().OrderByDescending(p=>p).ToList();
        }
        /// <summary>
        /// Returns the fiscal year for this date as an integer
        /// </summary>
        /// <param name="dt">The date you want to find </param>
        /// <returns>The Fiscal Year as an Integer, 0 for null</returns>
        private int FiscalYear(DateTime? dt)
        {
            //Check if the datetime is null, return 0 if it is
            if (dt == null) return 0;
            //The Date isn't null so convert it to a real datetime
            DateTime date = Convert.ToDateTime(dt);
            //New Fiscal Year starts October 1st
            //If the month is October or higher Add 1 to the current year and return it. If its before, return the current year
            return (date.Month >= 10 ? date.Year + 1 : date.Year);
        }
        private void BindFiscalYearComboBox()
        {
            foreach(String fy in fiscalYears)
            {
                rcbFiscalYear.Items.Add(new RadComboBoxItem() { Text = fy, Value = fy });
            }
        }
        #endregion

        #region TabClick Events
        protected void rtsSiteOptions_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            rmpSiteOptions.SelectedIndex = rtsSiteOptions.SelectedIndex;
        }
        #endregion

        #region Site Funding Overview
        protected void rcbFiscalYear_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgSiteFundingOverView.Rebind();
        }
        protected void rgSiteFundingOverView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                //Get the fiscal year
                var fy = Convert.ToInt32(rcbFiscalYear.SelectedValue);
                //The Start of the selected fiscal year
                DateTime startFY = Convert.ToDateTime(String.Format("10/01/{0}", fy - 1));
                //The end of the selected fiscal year
                DateTime endFY = Convert.ToDateTime(String.Format("9/30/{0}", fy));
                //Grab all records that overlap for that fiscal year
                rgSiteFundingOverView.DataSource = funding.Where(p => p.EndDate > startFY && p.StartDate < endFY).OrderBy(p => p.Name);
            }catch(Exception ex)
            {
                rgSiteFundingOverView.DataSource = new List<String>();
            }
            
        }
        protected void rgSiteFundingOverView_PreRender(object sender, EventArgs e)
        {
            foreach(GridDataItem row in rgSiteFundingOverView.Items)
            {
                var expandColumn = row.Cells[0];
                expandColumn.BackColor = expandColumn.BorderColor = System.Drawing.Color.White;
                var customerColumn = row.Cells[3];
                customerColumn.BackColor = System.Drawing.Color.White;
            }
            //Created to not have duplicated Customer/Agreement information for each site funding
            //The Row Index is the Row above the bottom row, walk up the Grid
            for (int rowIndex = rgSiteFundingOverView.Items.Count - 2; rowIndex >= 0; rowIndex += -1)
            {
                //The Current Row is the row index
                GridDataItem row = rgSiteFundingOverView.Items[rowIndex];
                //The Previous Row is one below it
                GridDataItem previousRow = rgSiteFundingOverView.Items[rowIndex + 1];
                //Compare the AgreementID's to see if they are the same
                if (row.GetDataKeyValue("AgreementID").ToString() == previousRow.GetDataKeyValue("AgreementID").ToString())
                {
                    //The Top Row Absorbs the previous Rowspan
                    row["AgreementID"].RowSpan = previousRow["AgreementID"].RowSpan < 2 ? 2 : previousRow["AgreementID"].RowSpan + 1;
                    //The previous row then goes away.
                    previousRow["AgreementID"].Visible = false;
                }
            }
        }
        #endregion

        #region Breadcrumbs
        /// <summary>
        /// Tries to get the org code for the site if not defaults to users home
        /// </summary>
        /// <returns>An Org Code</returns>
        public String GetSiteCentersHomeURL()
        {
            var newestFunding = funding.OrderBy(p => p.EndDate).FirstOrDefault();
            if (newestFunding != null) return siftaDB.Customers.FirstOrDefault(p => p.CustomerID == newestFunding.CustomerID).OrgCode;
            return user.Home;
        }
        #endregion

    }
}