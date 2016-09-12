using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NationalFundingDev
{
    public partial class AgreementLogPage : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private int AgreementModID;
        private AgreementMod mod;
        private Agreement agreement;
        private User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            AgreementModID = Convert.ToInt32(Request.QueryString["AgreementModID"]);
            mod = siftaDB.AgreementMods.FirstOrDefault(p => p.AgreementModID == AgreementModID);
            if (mod == null) Response.Redirect("closePage.html");
            agreement = mod.Agreement;
            BindAgreementLogInformation();
        }
        private void BindAgreementLogInformation()
        {
            if (!IsPostBack)
            {
                rcbActionAgreementLog.Items.Add(new RadComboBoxItem() { Text = "Select an Action", Value = "" });
                foreach (var action in siftaDB.lutAgreementLogTypes)
                {
                    rcbActionAgreementLog.Items.Add(new RadComboBoxItem() { Text = action.Type, Value = action.AgreementLogTypeID.ToString() });
                }
                rdtpAgreementLogTime.SelectedDate = DateTime.Now;
            }
            
        }
        protected void rbSubmitAgreementLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdtpAgreementLogTime.SelectedDate == null) throw new ArgumentException("A Date Was Not Selected.");
                var log = new AgreementModLog()
                {
                    CreatedBy = user.ID,
                    ModifiedBy = user.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                log.LoggedDate = Convert.ToDateTime(rdtpAgreementLogTime.SelectedDate);
                log.Remarks = rtbRemarksAgreementLog.Text;
                if(rcbActionAgreementLog.SelectedIndex != 0)
                {
                    log.AgreementLogTypeID = Convert.ToInt32(rcbActionAgreementLog.SelectedValue);
                }
                else
                {
                    throw new ArgumentException("An Action Was Not Selected.");
                }
                mod.AgreementModLogs.Add(log);
                siftaDB.SubmitChanges();
                pnlAgreementLog.Visible = false;
                pnlComplete.Visible = true;
            }
            catch(Exception ex)
            {
                ltlError.Text = ex.Message;
                pnlAgreementLog.Visible = false;
                pnlError.Visible = true;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("closePage.html");
        }

        protected void rbBack_Click(object sender, EventArgs e)
        {
            pnlAgreementLog.Visible = true;
            pnlError.Visible = false;
        }
    }
}