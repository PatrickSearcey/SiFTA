using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{

    public partial class AgreementDifference : System.Web.UI.Page
    {
        private string htmlFormat = "<table> <tr> <td></td> <td align='center'><b>USGS CMF</b></td> <td align='center'><b>Customer</b></td> <td align='center'><b>Other</b></td> </tr> <tr> <td align='right' style='background-color: #fccc67; color: black;'> Funded Sites<br /> Studies/Support<br /> <b>Total</b> </td> <td align='center' style='background-color: #fccc67; color: black;'> {0:C0}<br /> {1:C0}<br /> <b> {2:C0} </b> </td> <td align='center' style='background-color: #fccc67; color: black;'> {3:C0}<br /> {4:C0}<br /> <b> {5:C0} </b> </td> <td align='center' style='background-color: #fccc67; color: black;'> {6:C0}<br /> {7:C0}<br /> <b>{8:C0}</b> </td> </tr> <tr> <td align='right' style='background-color: black; color: white;'><b>Funding Total</b></td> <td align='center' style='background-color: black; color: white;'> {9:C0} </td> <td align='center' style='background-color: black; color: white;'> {10:C0} </td> <td align='center' style='background-color: black; color: white;'> {11:C0} </td> </tr> <tr> <td align='right' style='background-color: lightgray; color: black;'><b>Difference</b></td> <td align='center' style='background-color: lightgray; color: black;'> {12:C0} </td> <td align='center' style='background-color: lightgray; color: black;'> {13:C0} </td> <td align='center' style='background-color: lightgray; color: black;'> {14:C0} </td> </tr> </table>";
        public Agreement agreement;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var AgreementID = Convert.ToInt32(Request.QueryString["AgreementID"]);
            agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
            if (agreement == null) Response.Redirect("../../closePage.html");
            var summary = agreement.FundingSummary();
            if(!IsPostBack)
            {
                AddTabs(summary["recorded"]);
                rtsMod.SelectedIndex = 0;
                rmpMod.SelectedIndex = 0;
            }
            rmpMod.PageViews.Clear();
            AddPages(summary);
        }
        private void AddTabs(Dictionary<int, FundingExtensions.FundingAmounts> records)
        {
            short idx = 0;
            foreach(var record in records)
            {
                var text = record.Key == 0 ? "Original Agreement" : String.Format("Mod {0}", record.Key);
                rtsMod.Tabs.Add(new Telerik.Web.UI.RadTab()
                {
                    Text = text,
                    TabIndex = idx,
                });
                idx++;
            }
        }
        private void AddPages(Dictionary<string, Dictionary<int, FundingExtensions.FundingAmounts>> summary)
        {
            
            short idx = 0;
            foreach(var record in summary["recorded"])
            {
                //Grab the Different Dictionaries for this mod
                var recorded = summary["recorded"][record.Key];
                var siteFundingAlloted = summary["allocatedSite"][record.Key];
                var studiesFundingAllocated = summary["allocatedStudies"][record.Key];
                var difference = summary["difference"][record.Key];
                

                //Format html
                var fundingOverView = String.Format(htmlFormat, siteFundingAlloted.USGS, studiesFundingAllocated.USGS, siteFundingAlloted.USGS + studiesFundingAllocated.USGS, siteFundingAlloted.Customer, studiesFundingAllocated.Customer, siteFundingAlloted.Customer + studiesFundingAllocated.Customer, siteFundingAlloted.Other, studiesFundingAllocated.Other, siteFundingAlloted.Other + studiesFundingAllocated.Other, recorded.USGS, recorded.Customer, recorded.Other, difference.USGS, difference.Customer, difference.Other);
                var ltl = new System.Web.UI.LiteralControl();
                ltl.ID = String.Format("ltlMod{0}", record.Key);
                ltl.Text = fundingOverView;
                var rpv = new Telerik.Web.UI.RadPageView()
                {
                    TabIndex = idx,
                };
                rpv.Controls.Add(ltl);
                rmpMod.PageViews.Add(rpv);
                idx++;
            }
        }

        protected void rtsMod_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            rmpMod.SelectedIndex = rtsMod.SelectedIndex;
        }
    }
}