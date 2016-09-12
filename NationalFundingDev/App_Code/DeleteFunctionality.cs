using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalFundingDev
{
    public static class DeleteFunctionality
    {
        public static void Delete(this Customer _customer, bool StoreValues = false)
        {
            var siftaDB = new SiftaDBDataContext();
            if(StoreValues)
            {
                var address = String.Format("http://sifta.water.usgs.gov/Services/REST/Customer/CustomerInformation.ashx?CustomerID={0}&AllRecords=true", _customer.CustomerID);
                System.Net.WebClient wc = new System.Net.WebClient();
                var xml = wc.DownloadString(address);
                siftaDB.Archives.InsertOnSubmit(new Archive() { Type = "Customer", CreatedBy = new User().ID, CreatedOn = DateTime.Now, Data = xml });
            }
            var customer = siftaDB.Customers.FirstOrDefault(p => p.CustomerID == _customer.CustomerID);
            //Delete all Agreements
            foreach(var agreement in customer.Agreements)
            {
                //Delete the agreement
                Delete(agreement, false);
            }
            //Delete Contacts
            foreach(var contact in customer.CustomerContacts)
            {
                foreach(var address in contact.CustomerContactAddresses)
                {
                    siftaDB.CustomerContactAddresses.DeleteOnSubmit(address);
                }
                siftaDB.CustomerContacts.DeleteOnSubmit(contact);
            }
            siftaDB.Customers.DeleteOnSubmit(customer);
            siftaDB.SubmitChanges();
        }
        
        public static void Delete(this Agreement agreement, bool StoreValues = false)
        {
            var siftaDB = new SiftaDBDataContext();
            var _agreement = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == agreement.AgreementID);
            if(StoreValues)
            {
                var address = String.Format("http://sifta.water.usgs.gov/Services/REST/Agreement/AgreementInformation.ashx?AgreementID={0}", _agreement.AgreementID);
                System.Net.WebClient wc = new System.Net.WebClient();
                var xml = wc.DownloadString(address);
                siftaDB.Archives.InsertOnSubmit(new Archive() { Type = "Agreement", CreatedBy = new User().ID, CreatedOn = DateTime.Now, Data = xml });
            }
            //Go through each mod in the agreement
            foreach(var mod in _agreement.AgreementMods)
            {
                //Delete all teh site funding
                foreach(var site in mod.FundingSites)
                {
                    siftaDB.FundingSites.DeleteOnSubmit(site);
                }
                //Delete all studies funding
                foreach(var study in mod.FundingStudies)
                {
                    siftaDB.FundingStudies.DeleteOnSubmit(study);
                }
                //Delete all logs
                foreach(var log in mod.AgreementModLogs)
                {
                    siftaDB.AgreementModLogs.DeleteOnSubmit(log);
                }
                //Delete all coop funding
                foreach(var coop in mod.CooperativeFundings)
                {
                    siftaDB.CooperativeFundings.DeleteOnSubmit(coop);
                }
                siftaDB.AgreementMods.DeleteOnSubmit(mod);
            }
            siftaDB.Agreements.DeleteOnSubmit(_agreement);
            siftaDB.SubmitChanges();
        }
    }
}