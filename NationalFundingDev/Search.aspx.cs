using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev
{
    public partial class Search : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private string query;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("rsbQuery") as RadSearchBox).Visible = false;
            rsbSearch.DataSource = new List<string>();
            if (!IsPostBack)
            {
                query = HttpUtility.UrlDecode(Request.QueryString["Query"]);
                rsbSearch.Text = query;
                //Set the Session variable Title 
                Session["Title"] = "";
            }
        }
        protected void rsbSearch_Search(object sender, SearchBoxEventArgs e)
        {
            query = e.Text;
            rgResults.Rebind();
        }
        protected void rgResults_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgResults.DataSource = siftaDB.spSearchEngine(rsbSearch.Text).OrderBy(p => p.Type);
        }
        public Boolean CustomerImage(object obj)
        {
            var type = obj.ToString();
            return type == "Customer";
        }
        public String ImageURL(object obj)
        {
            var CustomerID = obj.ToString().Replace("Customer.aspx?CustomerID=", "");
            return String.Format("https://sifta.water.usgs.gov/Services/CustomerIcon?CustomerID={0}", CustomerID);
        }
        public String AppendURL(object obj)
        {
            if (obj == null) return "";
            var str = obj.ToString();
            if (String.IsNullOrEmpty(str)) return "";
            return str.AppendBaseURL();
        }
        public String NameLink(object type, object url, object name)
        {
            var text = String.Format("<a href='{0}' style='color:#0082CC; font-size:large; padding-right:10px;'>{1}</a>", url, name);
            var URL = url.ToString();
            var Type = type.ToString();
            var agreementID = "";
            if(Type == "Agreement")
            {
                try
                {
                    agreementID = URL.Substring(URL.IndexOf('=') + 1, URL.Length - URL.IndexOf('=') - 1);
                    text = String.Format("<a href='{0}' style='color:#0082CC; font-size:large; padding-right:10px;'>{1}</a>", String.Format("Reports/Agreement/AgreementReport.aspx?AgreementID={0}", agreementID).AppendBaseURL(), name);
                    var pencilImage = "/Images/editPencil.png".AppendBaseURL();
                    text += String.Format("<a href='{0}'><img src='{1}' style='width:15px;height:15px;' /></a>", url , pencilImage);
                }
                catch(Exception ex)
                {

                }
            }
            return text;
        }
    }
}