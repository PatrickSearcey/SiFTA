
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Metrics
{
    public partial class ImageSearch : System.Web.UI.Page
    {
        private SiftaDBDataContext db = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rbSearchClick_Click(object sender, EventArgs e)
        {
            rgImages.Rebind();
        }

        protected void rgImages_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if(!String.IsNullOrEmpty(rtbSearch.Text))
            {
                rgImages.DataSource = GetDataTable(rtbSearch.Text);
            }else
            {
                rgImages.DataSource = GetDataTable("Austin");
            }
        }
        public String GetWxH(object obj1)
        {
            try
            {
                Byte[] icon = (Byte[])obj1;
                Bitmap bmp;
                using (var ms = new MemoryStream(icon))
                {
                    bmp = new Bitmap(ms);
                }
                return String.Format("{0} x {1}", bmp.Width, bmp.Height);
            }
            catch
            {
                return "Unknown";
            }
        }
        public String GetLinks(object obj1, object obj2)
        {
            try
            {
                String name = obj1.ObjToString();
                Byte[] icon = (Byte[])obj2;
                String urlHTML = "";
                var customers = db.Customers.Where(p => p.Name == name && p.Icon == icon);
                foreach(var customer in customers)
                {
                    urlHTML += String.Format("<a href='http://sifta.water.usgs.gov/NationalFunding/Customer.aspx?CustomerID={0}' target='_blank'>{2} - {1}</a><br/>", customer.CustomerID, customer.Name, customer.Center.Name);
                }
                return urlHTML;
            }catch
            {
                return "";
            }
        }
        private DataTable GetDataTable(String search)
        {
            var dt = new DataTable();
            var query = String.Format("SELECT DISTINCT [Name],[Icon]FROM [siftadb].[dbo].[Customer]WHERE Name like '%{0}%'", search);
            using(SqlConnection conn = new SqlConnection(db.Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dt);
                conn.Close();
                da.Dispose();
            }
            return dt;
        }
    }
}