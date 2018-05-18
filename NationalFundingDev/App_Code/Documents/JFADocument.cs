using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Novacode;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;

namespace NationalFundingDev
{
    public class JFADocument
    {
        #region Local Variables
        private String Path = "";
        private HttpContext context = HttpContext.Current;
        private String document;
        private DocumentFormat.OpenXml.Packaging.WordprocessingDocument doc;
        private spAgreementDocumentResult agreement;
        private Center center;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        #endregion

        #region Constructors
        public JFADocument(int AgreementID, bool dumb = false)
        {
            if(!dumb) Path = context.Server.MapPath("Forms/JFATemplate.docx");
            else Path = context.Server.MapPath("Forms/JFATemplateDumb.docx");
            doc = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(Path, false);
            //get document text
            using (StreamReader sr = new StreamReader(doc.MainDocumentPart.GetStream()))
            {
                document = sr.ReadToEnd();
            }
            agreement = siftaDB.spAgreementDocument(AgreementID).FirstOrDefault();
            var a = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID);
            center = a.Customer.Center;
            ReplaceValues();
        }
        #endregion

        private void ReplaceValues()
        {
            var terms = new Dictionary<string, string>();
            terms.Add("[NowInformal]", DateTime.Now.ToString("MM/dd/yyyy"));
            terms.Add("[Now]", DateTime.Now.ToString("MMM d, yyyy"));
            terms.Add("[NowFormal]", DateTime.Now.ToString("MMMM d, yyyy"));
            terms.Add("[CenterName]", center.Name);
            terms.Add("[CenterAddress]", center.Address);
            terms.Add("[CenterCity]", center.City);
            terms.Add("[CenterState]", center.State);
            terms.Add("[CenterZipCode]", center.ZipCode);
            terms.Add("[OrgAbbrev]", center.Name);
            terms.Add("[CustomerName]", agreement.CustomerName);
            terms.Add("[CustomerCode]", agreement.Code);
            terms.Add("[FBMSNumber]", agreement.Number.ToString());
            terms.Add("[TIN]", agreement.CustomerTIN);
            terms.Add("[PurchaseOrderNum]", agreement.PurchaseOrderNumber);
            terms.Add("[FundsType]", FixedorActual);
            terms.Add("[FundingUSGS]", String.Format("{0:C0}", agreement.FundingUSGSCMF));
            terms.Add("[FundingCust]", String.Format("{0:C0}", agreement.FundingCustomer));
            terms.Add("[usgsFundingSentence]", FundingSentence);
            if(agreement.BillingCycleFrequency != null)terms.Add("[BillingFrequency]", agreement.BillingCycleFrequency.ToLower());
            terms.Add("[FixedCostCheckBox]", FixedCostCheckBox);
            terms.Add("[SONumber]", agreement.SalesDocument);
            //Customer Billing Contact
            terms.Add("[cbFirstName]", agreement.cbFirstName);
            terms.Add("[cbLastName]", agreement.cbLastName);
            terms.Add("[cbName]", String.Format("{0} {1}", agreement.cbFirstName, agreement.cbLastName));
            terms.Add("[cbTitle]", agreement.cbTitle);
            terms.Add("[cbFullAddress]", string.Format("{0}, {1} {2} {3}", agreement.cbStreetOne + " " + agreement.cbStreetTwo, agreement.cbCity, agreement.cbState, agreement.cbZipCode));
            terms.Add("[cbAddressOne]", agreement.cbStreetOne + " " + agreement.cbStreetTwo);
            terms.Add("[cbAddressTwo]", String.Format("{0}, {1} {2}", agreement.cbCity, agreement.cbState, agreement.cbZipCode));
            terms.Add("[cbPhoneWork]", PhoneFormat(agreement.cbPhoneWork));
            terms.Add("[cbPhoneFax]", PhoneFormat(agreement.cbPhoneFax));
            terms.Add("[cbEmail]", agreement.cbEmail);
            //Customer Technical Contact
            terms.Add("[ctSalutation]", agreement.ctSalutation);
            terms.Add("[ctFirstName]", agreement.ctFirstName);
            terms.Add("[ctLastName]", agreement.ctLastName);
            terms.Add("[ctName]", String.Format("{0} {1}", agreement.ctFirstName, agreement.ctLastName));
            terms.Add("[ctTitle]", agreement.ctTitle);
            terms.Add("[ctFullAddress]", string.Format("{0}, {1} {2} {3}", agreement.ctStreetOne + " " + agreement.ctStreetTwo, agreement.ctCity, agreement.ctState, agreement.ctZipCode));
            terms.Add("[ctAddressOne]", agreement.ctStreetOne + " " + agreement.ctStreetTwo);
            terms.Add("[ctAddressTwo]", String.Format("{0}, {1} {2}", agreement.ctCity, agreement.ctState, agreement.ctZipCode));
            terms.Add("[ctPhoneWork]", PhoneFormat(agreement.ctPhoneWork));
            terms.Add("[ctPhoneFax]", PhoneFormat(agreement.ctPhoneFax));
            terms.Add("[ctEmail]", agreement.ctEmail);
            //USGS Billing Contact
            terms.Add("[ubFirstName]", agreement.ubFirstName);
            terms.Add("[ubLastName]", agreement.ubLastName);
            terms.Add("[ubName]", String.Format("{0} {1}", agreement.ubFirstName, agreement.ubLastName));
            terms.Add("[ubTitle]", FundingExtensions.XMLSafe(agreement.ubTitle));
            terms.Add("[ubFullAddress]", string.Format("{0}, {1} {2} {3}", agreement.ubStreetOne + " " + agreement.ubStreetTwo, agreement.ubCity, agreement.ubState, agreement.ubZipCode));
            terms.Add("[ubAddressOne]", agreement.ubStreetOne + " " + agreement.ubStreetTwo);
            terms.Add("[ubAddressTwo]", String.Format("{0}, {1} {2}", agreement.ubCity, agreement.ubState, agreement.ubZipCode));
            terms.Add("[ubPhoneWork]", PhoneFormat(agreement.ubPhoneWork));
            terms.Add("[ubPhoneFax]", PhoneFormat(agreement.ubPhoneFax));
            terms.Add("[ubEmail]", agreement.ubEmail);
            //USGS Technical Contact
            terms.Add("[utFirstName]", agreement.utFirstName);
            terms.Add("[utLastName]", agreement.utLastName);
            terms.Add("[utName]", String.Format("{0} {1}", agreement.utFirstName, agreement.utLastName));
            terms.Add("[utTitle]", agreement.utTitle);
            terms.Add("[utFullAddress]", string.Format("{0}, {1} {2} {3}", agreement.utStreetOne + " " + agreement.utStreetTwo, agreement.utCity, agreement.utState, agreement.utZipCode));
            terms.Add("[utAddressOne]", agreement.utStreetOne + " " + agreement.utStreetTwo);
            terms.Add("[utAddressTwo]", String.Format("{0}, {1} {2}", agreement.utCity, agreement.utState, agreement.utZipCode));
            terms.Add("[utPhoneWork]", PhoneFormat(agreement.utPhoneWork));
            terms.Add("[utPhoneFax]", PhoneFormat(agreement.utPhoneFax));
            terms.Add("[utEmail]", agreement.utEmail);
            //Added to change checklist project chief to Tech contact
            terms.Add("[utNamePD]", String.Format("{0} {1} {2}", agreement.utFirstName, agreement.utLastName, PhoneFormat(agreement.utPhoneWork)));
            //Handle Dates
            var start = Convert.ToDateTime(agreement.StartDate);
            var end = Convert.ToDateTime(agreement.EndDate);
            terms.Add("[StartDate]", start.ToString("M/d/yyyy"));
            terms.Add("[EndDate]", end.ToString("M/d/yyyy"));
            terms.Add("[StartDateFormal]", start.ToString("MMMM d, yyyy"));
            terms.Add("[EndDateFormal]", end.ToString("MMMM d, yyyy"));
            foreach(var term in terms)
            {
                document = document.Replace(term.Key, FundingExtensions.XMLSafe(term.Value));
            }
        }
        public void DownloadDocument()
        {
            byte[] byteArray = File.ReadAllBytes(Path);
            using (MemoryStream mem = new MemoryStream())
            {
                //Grab the word document
                mem.Write(byteArray, 0, (int)byteArray.Length);
                using (DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordDoc = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(mem, true))
                {
                    //Write it back to word
                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(document);
                    }
                }
                //Write the file out
                using (Stream fileStream = context.Response.OutputStream)
                {
                    context.Response.AddHeader("content-disposition", "attachment; filename=\"" + agreement.PurchaseOrderNumber + "_JFA.docx\"");
                    context.Response.ContentType = "application/msword";
                    mem.WriteTo(fileStream);
                }
                context.Response.End();
            }
        }
        public void SaveDocument(String DestinationPath)
        {
            if(!Directory.Exists(DestinationPath)) Directory.CreateDirectory(DestinationPath);
            //document.SaveAs(String.Format("{0}{1}.docx", DestinationPath, agreement.PurchaseOrderNumber));
        }

        #region Properties
        public String FinancialReviewerID
        {
            set
            {
                try
                {
                    var id = Convert.ToInt32(value);
                    var fr = siftaDB.CenterFinancialReviewers.FirstOrDefault(p => p.CenterFinancialReviewerID == id);
                    document = document.Replace("[FinancialReviewerWithPhone]" , String.Format("{0} {1}", fr.Name, PhoneFormat(fr.Phone)));
                    document = document.Replace("[FinancialReviewer]", fr.Name);
                }
                catch
                {
                    document = document.Replace("[FinancialReviewerWithPhone]", "");
                    document = document.Replace("[FinancialReviewer]", "");
                }
            }
        }
        public String CenterDirectorID
        {
            set
            {
                try
                {
                    var id = Convert.ToInt32(value);
                    var cd = siftaDB.CenterDirectors.FirstOrDefault(p => p.CenterDirectorID == id);
                    document = document.Replace("[Director]", cd.Name);
                    document = document.Replace("[DirectorTitle]", cd.Title);
                    document = document.Replace("[DirectorWithPhone]", String.Format("{0} {1}", cd.Name, PhoneFormat(cd.Phone)));
                }
                catch
                {
                    document = document.Replace("[Director]", "");
                    document = document.Replace("[DirectorTitle]", "");
                    document = document.Replace("[DirectorWithPhone]", "");
                }
            }
        }
        public String DUNS
        {
            set
            {
                document = document.Replace("[DUNS]", value);
            }
        }
        public String ProjectNumber
        {
            set
            {
                document = document.Replace("[ProjectNumber]", value);
            }
        }
        public String ProjectName
        {
            set
            {
                document = document.Replace("[ProjectName]", value);
            }
        }
        private String FixedorActual
        {
            get
            {
                if (String.IsNullOrEmpty(agreement.FundsType)) return "";
                if (agreement.FundsType == "F") return "fixed"; else return "actual";
            }
        }
        private String FundingSentence
        {
            get
            {
                if (agreement.FundingUSGSCMF > 0)
                {
                    return string.Format("U.S. Geological Survey contributions for this agreement are {0:C0} for a combined total of {1:C0}. ", agreement.FundingUSGSCMF, agreement.FundingCustomer + agreement.FundingUSGSCMF);
                }
                else
                {
                    return "";
                }
            }
        }
        private String FixedCostCheckBox
        {
            get
            {
                if (agreement.FundsType == "F") return "YES[ X ] NO[   ]"; else return "YES[   ] NO[ X ]";
            }
        }
        private String PhoneFormat(String number)
        {
            //Make sure a number exists
            if (!string.IsNullOrEmpty(number))
            {
                //Strips the string down to be only numbers
                number = System.Text.RegularExpressions.Regex.Replace(number, @"[^\d]", "");
                if (number.Length > 10)
                {
                    //Format the first 10 digits then push the remainder into the extension section
                    return String.Format("{0:(###) ###-####} Ext {1}", Convert.ToInt64(number.Substring(0, 10)), number.Substring(10, number.Length - 10));
                }
                else
                {
                    //Format the phone number
                    return String.Format("{0:(###) ###-####}", Convert.ToInt64(number));
                }
            }
            else return "";
        }
        #endregion
    }
}