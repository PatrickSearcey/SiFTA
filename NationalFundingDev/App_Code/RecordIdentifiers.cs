using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NationalFundingDev
{
    public static class RecordIdentifiers
    {
        private static int RecordIDSize = 25;
        /// <summary>
        /// Returns a random 15 digit number
        /// </summary>
        private static string RandomNumbers
        {
            get
            {
                var rnd = new Random();
                var s = new StringBuilder();
                while (s.Length < RecordIDSize)
                {
                    s.Append(rnd.Next(10).ToString());
                }
                return s.ToString();
            }
        }
        public static string RecordIdentifier(this Agreement a)
        {
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            string id;
            do
            {
                id = String.Format("SIFTA-{0}A{1}", a.AgreementID, RandomNumbers).Substring(0, RecordIDSize);
            } while (siftaDB.Agreements.FirstOrDefault(p => p.RecordID == id) != null);
            return id;
        }
        public static string RecordIdentifier(this AgreementMod am)
        {
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            string id;
            do
            {
                id = String.Format("SIFTA-{0}M{1}", am.AgreementModID, RandomNumbers).Substring(0, RecordIDSize);
            } while (siftaDB.AgreementMods.FirstOrDefault(p => p.RecordID == id) != null);
            return id;
        }
        public static string RecordIdentifier(this FundingSite fs)
        {
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            string id;
            do
            {
                id = String.Format("SIFTA-{0}SF{1}", fs.FundingSiteID, RandomNumbers).Substring(0, RecordIDSize);
            } while (siftaDB.FundingSites.FirstOrDefault(p => p.RecordID == id) != null);
            return id;
        }
        public static string RecordIdentifier(this FundingStudy fs)
        {
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            string id;
            do
            {
                id = String.Format("SIFTA-{0}RF{1}", fs.FundingStudyID, RandomNumbers).Substring(0, RecordIDSize);
            } while (siftaDB.FundingStudies.FirstOrDefault(p => p.RecordID == id) != null);
            return id;
        }

        public static void UpdateRecords()
        {
            var siftaDB = new SiftaDBDataContext();
            foreach (var agreement in siftaDB.Agreements.Where(p => p.RecordID == null))
            {
                agreement.RecordID = RecordIdentifier(agreement);
                siftaDB.SubmitChanges();
            }
            foreach (var mod in siftaDB.AgreementMods.Where(p => p.RecordID == null))
            {
                mod.RecordID = RecordIdentifier(mod);
                siftaDB.SubmitChanges();
            }
            foreach (var s in siftaDB.FundingSites.Where(p => p.RecordID == null))
            {
                s.RecordID = RecordIdentifier(s);
                siftaDB.SubmitChanges();
            }
            foreach (var s in siftaDB.FundingStudies.Where(p => p.RecordID == null))
            {
                s.RecordID = RecordIdentifier(s);
                siftaDB.SubmitChanges();
            }
        }
    }
}