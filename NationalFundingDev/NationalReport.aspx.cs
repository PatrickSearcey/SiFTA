using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{
    public partial class NationalReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rtsOptions_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            rmpOptions.SelectedIndex = rtsOptions.SelectedTab.TabIndex;
        }
    }
}