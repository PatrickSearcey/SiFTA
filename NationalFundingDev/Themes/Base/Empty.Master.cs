using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{
    public partial class Empty : System.Web.UI.MasterPage
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private User user = new User();
        private String path;
        protected void Page_Load(object sender, EventArgs e)
        {
            path = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/') + 1);
            rsbQuery.DataSource = new List<String>();
            if (!IsPostBack)
            {
                rmTopNav.AddLinks();
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
                var results = siftaDB.spSearchEngine(e.Text).ToList();
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