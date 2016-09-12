using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NationalFundingDev
{
    public partial class GenerateRecordID : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var agreements = siftaDB.Agreements.Where(p => p.RecordID == null);
            var mods = siftaDB.AgreementMods.Where(p => p.RecordID == null);
            var siteFunding = siftaDB.FundingSites.Where(p => p.RecordID == null);
            var studiesFunding = siftaDB.FundingStudies.Where(p => p.RecordID == null);
            ltlAgreement.Text = agreements.Count().ToString();
            ltlMods.Text = mods.Count().ToString();
            ltlSiteFunding.Text = siteFunding.Count().ToString();
            ltlStudiesFunding.Text = studiesFunding.Count().ToString();
            foreach(var agreement in agreements)
            {
                agreement.RecordID = agreement.RecordIdentifier();
            }
            foreach(var mod in mods)
            {
                mod.RecordID = mod.RecordIdentifier();
            }
            foreach(var sf in siteFunding)
            {
                sf.RecordID = sf.RecordIdentifier();
            }
            foreach(var sf in studiesFunding)
            {
                sf.RecordID = sf.RecordIdentifier();
            }
            siftaDB.SubmitChanges();
        }
    }
}