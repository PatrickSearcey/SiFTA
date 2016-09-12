using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev.Reports.Agreement
{
    public partial class AgreementReport : System.Web.UI.Page
    {
        private int AgreementID;
        private string SalesOrderNumber = "", PurchaseOrderNumber = "";
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public NationalFundingDev.Agreement agreement;
        private User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAgreement();
            user = new NationalFundingDev.User(agreement.Customer.OrgCode);
            //Set Customer Logo Source
            imgCustLogo.ImageUrl = String.Format("http://sifta.water.usgs.gov/Services/REST/Customer/CustomerIcon.ashx?CustomerID={0}", agreement.CustomerID);
            imgEditAgreementInformation.Visible = imgEditAgreementLogs.Visible = imgEditContacts.Visible = imgEditDocuments.Visible = imgEditSiteFunding.Visible = imgEditStudiesFunding.Visible = user.CanUpdate || user.CanInsert;
            imgEditAgreementInformation.BorderWidth = imgEditAgreementLogs.BorderWidth = imgEditContacts.BorderWidth = imgEditDocuments.BorderWidth = imgEditSiteFunding.BorderWidth = imgEditStudiesFunding.BorderWidth = 0;
            PageDataBind();
        }
        private void GetAgreement()
        {
            //Grab the AgreementID from the url www.url/Agreement.aspx?AgreementID={0}
            var agreementID = Request.QueryString["AgreementID"];
            if (!string.IsNullOrEmpty(agreementID))
            {
                AgreementID = Convert.ToInt32(agreementID);
                //Set agreement to be the one from the Database
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
                //If the agreement is not valid send them to the search page
                if (agreement == null) Response.Redirect(String.Format("Search.aspx?Query={0}", AgreementID).AppendBaseURL());
            }
            SalesOrderNumber = Request.QueryString["SalesOrderNumber"];
            if (!String.IsNullOrEmpty(SalesOrderNumber))
            {
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.SalesDocument == SalesOrderNumber);
                //If the agreement is not valid send them to the search page
                if (agreement == null) Response.Redirect(String.Format("Search.aspx?Query={0}", SalesOrderNumber).AppendBaseURL());
            }
            PurchaseOrderNumber = Request.QueryString["PurchaseOrderNumber"];
            if (!String.IsNullOrEmpty(PurchaseOrderNumber))
            {
                agreement = siftaDB.Agreements.FirstOrDefault(p => p.PurchaseOrderNumber == PurchaseOrderNumber);
                //If the agreement is not valid send them to the search page
                if (agreement == null) Response.Redirect(String.Format("Search.aspx?Query={0}", PurchaseOrderNumber).AppendBaseURL());
            }

            //If the agreement is not valid send them back to the Default Page
            if (agreement == null) Response.Redirect("Default.aspx".AppendBaseURL());
        }
        private void PageDataBind()
        {
            var modSummary = (NationalFundingDev.Reports.Modules.ModFundingSummary)LoadControl("~/Reports/Modules/ModFundingSummary.ascx");
            modSummary.agreement = agreement;
            pnlFundingSummary.Controls.Add(modSummary);
            //Map
            iMap.Src = String.Format("Reports/Maps/AgreementSiteMap.aspx?AgreementID={0}&Clean=true&Height=250&Width=512", agreement.AgreementID).AppendBaseURL();
            //Contacts

            //Last Agreement Log
            var agreementLog = siftaDB.vAgreementLogs.Where(p => p.AgreementID == agreement.AgreementID).OrderByDescending(p => p.LoggedDate).FirstOrDefault();
            if (agreementLog != null)
            {
                var name = "";
                if(!String.IsNullOrEmpty(agreementLog.Name)) name = agreementLog.Name; else name = agreementLog.CreatedBy;
                ltlAgreementLog.Text = String.Format("{0} on {1:d} by {2}", agreementLog.Type, agreementLog.LoggedDate, name);
            }
            else ltlAgreementLog.Text = String.Format("");
            
        }

        #region Contacts
        protected void rgctContact_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgctContact.DataSource = siftaDB.vCustomerContacts.Where(p => p.CustomerContactAddressID == agreement.CustomerTechnicalContact).ToList();
        }

        protected void rgcbContact_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgcbContact.DataSource = siftaDB.vCustomerContacts.Where(p => p.CustomerContactAddressID == agreement.CustomerBillingContact).ToList();
        }

        protected void rgutContact_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgutContact.DataSource = siftaDB.vUSGSContacts.Where(p => p.EmployeeID == agreement.USGSTechnicalContact).ToList();
        }

        protected void rgubContact_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgubContact.DataSource = siftaDB.vUSGSContacts.Where(p => p.EmployeeID == agreement.USGSBillingContact).ToList();
        }
        #endregion

        #region Agreement Documents
        protected void rgAgreementDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            var path = new DirectoryInfo(String.Format("D:\\siftaroot\\Documents\\Agreements\\{0}", agreement.AgreementID));
            if (!path.Exists) path.Create();
            var files = path.GetFiles();
            //Create a Datatable to store the information of the files to be used as a datasource for the grid
            DataTable dt = new DataTable();
            //Name and Path
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("URL", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            dt.Columns["Name"].Caption = "Name";
            dt.Columns["Name"].ColumnName = "Name";

            dt.Columns["URL"].Caption = "URL";
            dt.Columns["URL"].ColumnName = "URL";

            dt.Columns["Path"].Caption = "Path";
            dt.Columns["Path"].ColumnName = "Path";
            //For each file in the folder add a row to the datatable with its name and path
            foreach (var file in files)
            {

                DataRow doc = dt.NewRow();
                doc["Name"] = file.Name;
                doc["URL"] = String.Format("http://sifta.water.usgs.gov/Documents/Agreements/{0}/{1}", agreement.AgreementID, file.Name);
                doc["Path"] = file.FullName;
                dt.Rows.Add(doc);
            }
            rgAgreementDocuments.DataSource = dt;
        }
        #endregion

        #region Inline Code
        public String GetStationName(object siteNumber, object siteName)
        {
            String name = "";
            if (siteNumber != null) name += siteNumber;
            if (siteName != null) name += " - " + siteName.ToString();
            if (name.Length > FundingExtensions.GridTextLength)
            {
                name = name.Substring(0, FundingExtensions.GridTextLength) + "...";
            }
            return name;
        }
        public String AppendBaseURL(string url)
        {
            return url.AppendBaseURL();
        }
        public String DateFormat(DateTime? dt, String format)
        {
            if (dt == null) return "";
            return Convert.ToDateTime(dt).ToString(format);
        }
        public String CollectionCodeString(object item)
        {
            if (item == null) return "";
            var CollectionCodeID = Convert.ToInt32(item);
            var collectionCode = siftaDB.lutCollectionCodes.FirstOrDefault(p => p.CollectionCodeID == CollectionCodeID);
            if (collectionCode == null) return "";
            return String.Format("{0} - {1}", collectionCode.Code, collectionCode.Description);
        }
        public String AgreementInfoDateFormat(DateTime? d)
        {
            if (d != null)
            {
                return Convert.ToDateTime(d).ToString("MM/dd/yy");
            }
            return "N/A";
        }
        public DateTime? AgreementEndDate
        {
            get
            {
                var mods = siftaDB.AgreementMods.Where(p => p.AgreementID == agreement.AgreementID && p.EndDate != null).OrderByDescending(p => p.Number).ToList();
                if (mods.Count() != 0)
                {
                    return mods.FirstOrDefault().EndDate;
                }
                else return null;
            }
        }
        public String ModName(object o)
        {
            if (o == null) return "";
            if (o.ToString() == "0") return "Agreement";
            return String.Format("Mod {0}", o);
        }
        public String ShortenSiteName(object o)
        {
            int idealLength = 10;
            if (o == null) return "";
            var str = o.ToString();
            if (str.Length <= idealLength) return str; else return str.Substring(0, idealLength) + "...";
        }
        public Boolean CanBeDeleted(object o)
        {
            if (o == null) return false;
            if (o.ToString() == "0") return false; else return true;
        }
        public String ModNameFormat(object o)
        {
            if (o == null) return "";
            var mod = o.ToString();
            if (mod == "0") return "Original Agreement"; else return String.Format("Mod {0}", mod);
        }
        /// <summary>
        /// Takes 4 objects of a name and converts them to a well formatted full name.
        /// </summary>
        /// <param name="s">Salutaiton (Mr. Ms.)</param>
        /// <param name="f">First Name</param>
        /// <param name="m">Middle Name</param>
        /// <param name="l">Last Name</param>
        /// <returns></returns>
        public String ContactName(object s, object f, object m, object l)
        {
            var name = "";
            if (!String.IsNullOrEmpty(s.ToString())) name += s.ToString() + " ";
            if (!String.IsNullOrEmpty(f.ToString())) name += f.ToString() + " ";
            if (!String.IsNullOrEmpty(m.ToString())) name += m.ToString() + " ";
            if (!String.IsNullOrEmpty(l.ToString())) name += l.ToString();
            return name;
        }
        public String PhoneFormat(object p)
        {
            if (p == null) return "";
            return p.ToPhoneFormat();
        }
        #endregion   

        protected void rgFundedSites_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgFundedSites.DataSource = siftaDB.vSiteFundingInformations.Where(p => p.AgreementID == agreement.AgreementID).OrderBy(p => p.SiteNumber);
        }

        protected void rgStudiesSupport_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgStudiesSupport.DataSource = siftaDB.vStudiesFundingInformations.Where(p => p.AgreementID == agreement.AgreementID);
        }

        protected void rgFundedSites_PreRender(object sender, EventArgs e)
        {
            var headerText = new Queue<string>();
            foreach(GridGroupHeaderItem item in rgFundedSites.MasterTableView.GetItems(GridItemType.GroupHeader))
            {
                var text = item.Cells[1].Text;
                if (!String.IsNullOrEmpty(text)) headerText.Enqueue(text);
            }
            foreach(GridGroupFooterItem item in rgFundedSites.MasterTableView.GetItems(GridItemType.GroupFooter))
            {
                item.Cells[1].Visible = item.Cells[2].Visible = item.Cells[3].Visible = item.Cells[4].Visible = item.Cells[5].Visible = item.Cells[6].Visible = item.Cells[7].Visible = false;
                item.Cells[8].ColumnSpan = 6;
                item.Cells[8].Style.Add("text-align", "right");
                item.Cells[8].Text = String.Format("<b>{0} Total :</b>", headerText.Peek());
                headerText.Dequeue();
            }
            foreach(GridFooterItem item in rgFundedSites.MasterTableView.GetItems(GridItemType.Footer))
            {
                item.Cells[1].Visible = item.Cells[2].Visible = item.Cells[3].Visible = item.Cells[4].Visible = item.Cells[5].Visible = item.Cells[6].Visible = item.Cells[7].Visible = false;
                item.Cells[8].ColumnSpan = 6;
                item.Cells[8].Style.Add("text-align", "right");
                item.Cells[8].Text = String.Format("<b>Total :</b>");
            }
        }
    }
}