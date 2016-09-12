
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Net;

namespace NationalFundingDev
{
    public partial class Default : System.Web.UI.Page
    {
        private User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(user.Home))
            {
                Response.Redirect(String.Format("Center.aspx?OrgCode={0}", user.Home));
            }
            else
            {
                Response.Redirect("CentersMap.aspx");
            }
        }
    }
}