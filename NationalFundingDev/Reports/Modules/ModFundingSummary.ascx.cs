using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev.Reports.Modules
{
    public partial class ModFundingSummary : System.Web.UI.UserControl
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        public NationalFundingDev.Agreement agreement;
        private string htmlFormat = "<tr>" +
                                        "<td align='right' style='background-color: #fccc67; color: black;padding-right:5px; border-bottom-style:hidden;'> Funded Sites<br /> Studies/Support </td>" +
                                        "<td align='center' style='background-color: #fccc67; color: black; border-bottom-style:hidden;'> {0:C0}<br /> {1:C0} </td>" +
                                        "<td align='center' style='background-color: #fccc67; color: black; border-bottom-style:hidden;'> {3:C0}<br /> {4:C0} </td>" +
                                        "<td align='center' style='background-color: #fccc67; color: black; border-bottom-style:hidden;'> {6:C0}<br /> {7:C0}</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td align='right' style='background-color: #bd8f04; color: white;padding-right: 5px; border-top-style: dotted; border-top-color:black;'><b>Planned Total</b></td>" +
                                        "<td align='center' style='background-color: #bd8f04; color: white; border-top-style: dotted; border-top-color:black;'> <b> {2:C0} </b></td>" +
                                        "<td align='center' style='background-color: #bd8f04; color: white; border-top-style: dotted; border-top-color:black;'> <b> {5:C0} </b> </td>" +
                                        "<td align='center' style='background-color: #bd8f04; color: white; border-top-style: dotted; border-top-color:black;'> <b>{8:C0}</b> </td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td align='right' style='background-color: black; color: white;padding-right: 5px;'><b>Funding Total</b></td>" +
                                        "<td align='center' style='background-color: black; color: white;'> {9:C0} </td>" + 
                                        "<td align='center' style='background-color: black; color: white;'> {10:C0} </td>" +
                                        "<td align='center' style='background-color: black; color: white;'> {11:C0} </td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td align='right' style='background-color: lightgray; color: black; padding-right:5px;'><b>Difference</b></td> {12} {13} {14}" + 
                                    "</tr>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(agreement == null)
            {
                ltlTable.Text = "An agreement was not specified in code";
            }
            else
            {
                ltlTable.Text = GetFundingSummary();
            }
        }

        private string GetFundingSummary()
        {
            //Holds the totals to be displayed at the top
            var trecorded = new FundingExtensions.FundingAmounts() { Customer = 0, Other = 0, USGS = 0 };
            var tsiteFundingAlloted = new FundingExtensions.FundingAmounts() { Customer = 0, Other = 0, USGS = 0 };
            var tstudiesFundingAllocated = new FundingExtensions.FundingAmounts() { Customer = 0, Other = 0, USGS = 0 };
            var tdifference =  new FundingExtensions.FundingAmounts() { Customer = 0, Other = 0, USGS = 0 };
            //Holds the formatted String
            var formatString = "";
            //Grab the summary
            var summary = agreement.FundingSummary();
            //Go through each record in the summary
            foreach(var record in summary["recorded"])
            {
                
                //Grab the Different Dictionaries for this mod
                var recorded = summary["recorded"][record.Key];
                var siteFundingAlloted = summary["allocatedSite"][record.Key];
                var studiesFundingAllocated = summary["allocatedStudies"][record.Key];
                var difference = summary["difference"][record.Key];
                #region Add to Totals
                //Recorded
                trecorded.USGS += recorded.USGS;
                trecorded.Customer += recorded.Customer;
                trecorded.Other += recorded.Other;
                //Site Funding Allocated
                tsiteFundingAlloted.Customer += siteFundingAlloted.Customer;
                tsiteFundingAlloted.USGS += siteFundingAlloted.USGS;
                tsiteFundingAlloted.Other += siteFundingAlloted.Other;
                //Studies Funding Allocated
                tstudiesFundingAllocated.Customer += studiesFundingAllocated.Customer;
                tstudiesFundingAllocated.USGS += studiesFundingAllocated.USGS;
                tstudiesFundingAllocated.Other += studiesFundingAllocated.Other;
                //Difference
                tdifference.USGS += difference.USGS;
                tdifference.Customer += difference.Customer;
                tdifference.Other += difference.Other;
                #endregion
                //Format HTML
                if(summary["recorded"].Count > 1)
                {
                    formatString += String.Format("<tr><td colspan='4' style='background-color:white;'><b>{0}</b></td></tr>", GetModName(record.Key));
                    formatString += String.Format(htmlFormat, siteFundingAlloted.USGS, studiesFundingAllocated.USGS, siteFundingAlloted.USGS + studiesFundingAllocated.USGS, siteFundingAlloted.Customer, studiesFundingAllocated.Customer, siteFundingAlloted.Customer + studiesFundingAllocated.Customer, siteFundingAlloted.Other, studiesFundingAllocated.Other, siteFundingAlloted.Other + studiesFundingAllocated.Other, recorded.USGS, recorded.Customer, recorded.Other, ColorCell(difference.USGS), ColorCell(difference.Customer), ColorCell(difference.Other));
                }
            }
            return "<table style='width:100%;'><tr><td></td><td><center><b>USGS CWP</b></center></td><td><center><b>Customer</b></center></td><td><center><b>Other</b></center></td></tr>" + String.Format(htmlFormat, tsiteFundingAlloted.USGS, tstudiesFundingAllocated.USGS, tsiteFundingAlloted.USGS + tstudiesFundingAllocated.USGS, tsiteFundingAlloted.Customer, tstudiesFundingAllocated.Customer, tsiteFundingAlloted.Customer + tstudiesFundingAllocated.Customer, tsiteFundingAlloted.Other, tstudiesFundingAllocated.Other, tsiteFundingAlloted.Other + tstudiesFundingAllocated.Other, trecorded.USGS, trecorded.Customer, trecorded.Other, ColorCell(tdifference.USGS), ColorCell(tdifference.Customer), ColorCell(tdifference.Other)) + formatString + "</table>";
        }

        private string GetModName(int p)
        {
            if (p == 0) return "Original Agreement";
            else return String.Format("Mod {0}", p);
        }
        private string ColorCell(double p)
        {
            if (p != 0)
            {
                return String.Format("<td align='center' style='background-color: lightgray; color: red;'> {0:C0} </td>", p);
            }
            else return String.Format("<td align='center' style='background-color: lightgray; color: black;'> {0:C0} </td>", p);
        }
    }
}