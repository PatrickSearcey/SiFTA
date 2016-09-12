using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{
    public partial class MapControlClean : System.Web.UI.UserControl
    {
        public int Width = 500, Height = 500;
        public List<Site> Sites;
        protected void Page_Load(object sender, EventArgs e)
        {
            hfHeight.Value = Height.ToString();
            hfWidth.Value = Width.ToString();
            hfSites.Value = Sites.ToJson();
            map.Height = Unit.Pixel(Height);
            map.Width = Unit.Pixel(Width);
        }
        public String AppendBaseURL(string path)
        {
            return path.AppendBaseURL();
        }
    }
}