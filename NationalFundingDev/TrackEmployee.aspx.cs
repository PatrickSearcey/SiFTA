using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{
    public partial class TrackEmployee : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rbValidate_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(rtbID.Text))
            {
                var service = new ActiveDirectoryService();
                var employee = service.GetEmployee(rtbID.Text);
                if(employee != null)
                {
                    if(siftaDB.Employees.FirstOrDefault(p=>p.EmployeeID == employee.EmployeeID) != null)
                    {
                        ltlName.Text = "Employee is already being tracked in SiFTA";
                        btnTrack.Visible = false;
                    }else
                    {
                        btnTrack.Visible = true;
                        ltlID.Text = employee.EmployeeID;
                        ltlName.Text = String.Format("{0} {1} {2}", employee.FirstName, employee.MiddleName, employee.LastName);
                    }
                    pnlWrongInfo.Visible = false;
                    pnlInfo.Visible = true;
                }else
                {
                    pnlWrongInfo.Visible = true;
                    pnlInfo.Visible = false;
                }
                
            }else
            {
                pnlWrongInfo.Visible = true;
                pnlInfo.Visible = false;
            } 
        }

        protected void btnTrack_Click(object sender, EventArgs e)
        {
            //Adds employee id to the database
            ltlID.Text.ValidEmployeeID();
            //Redirects them to close the page.
            Response.Redirect("closePage.html".AppendBaseURL());
        }
    }
}