using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private User user = new User();
        public String CenterAdmin, TopBarLoaded;
        protected void Page_Load(object sender, EventArgs e)
        {
            hfConnectionString.Value = siftaDB.Connection.ConnectionString;
            var a = new User(Request.QueryString["OrgCode"]);
            CenterAdmin = a.AdminPortalVisible.ToString();
            rsbQuery.DataSource = new List<String>();
            if (!IsPostBack)
            {
                rmTopNav.AddLinks();
                TopBarLoaded = "true";
            }
        }
        /// <summary>
        /// Returns the Title of the current page 
        /// Value is set in Session["Title"]
        /// </summary>
        public String Title
        {
            get
            {
                if (Session["Title"] != null) return Session["Title"].ToString(); else return "";
            }
        }
        protected void rsbQuery_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Text))
            {
                var results = siftaDB.spSearchEngine(e.Text.Trim()).ToList();
                if (results.Count() == 1)
                {
                    var url = results.FirstOrDefault().URL.AppendBaseURL();
                    Response.Redirect(url);
                }
                else
                {
                    Response.Redirect(String.Format("Search.aspx?Query={0}", HttpUtility.UrlEncode(e.Text)).AppendBaseURL());
                }
            }
        }
    }
}