using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace NationalFundingDev
{
    public static class FundingExtensions
    {

        public static int GridTextLength = 15;
        #region Employee
        public static bool ValidEmployeeID(this string employeeID)
        {
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            //Check to see if they aren't being tracked yet.
            if (siftaDB.Employees.FirstOrDefault(p => p.EmployeeID == employeeID) == null)
            {
                //Call the service for Active Directory
                var service = new ActiveDirectoryService.ActiveDirectoryClient();
                //Try to get the employee with the same id
                var employee = service.GetEmployee(employeeID);
                //Check to see that the employee is not null
                if (employee != null)
                {
                    var e = new Employee()
                    {
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        OrgCode = employee.OrgCode,
                        EmployeeID = employee.EmployeeID,
                        StreetOne = employee.StreetOne,
                        StreetTwo = employee.StreetTwo,
                        City = employee.City,
                        State = employee.State,
                        ZipCode = employee.ZipCode,
                        Department = employee.Department,
                        Email = employee.Email,
                        Title = employee.Title,
                        PhoneFax = employee.PhoneFax,
                        PhoneWork = employee.PhoneWork
                    };
                    siftaDB.Employees.InsertOnSubmit(e);
                    siftaDB.SubmitChanges();
                    return true;
                }
                //Employee is not real return false
                else return false;
            }
            else return true;
        }
        #endregion
        #region Menu
        public static void AddLinks(this Telerik.Web.UI.RadMenu menu)
        {
            menu.DataBind();
            SiftaDBDataContext siftaDB = new SiftaDBDataContext();
            User user;
            String OrgCode;
            var reportsItem = menu.Items.FirstOrDefault(p => p.Text == "Reports");
            if(!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["OrgCode"]))
            {
                OrgCode = HttpContext.Current.Request.QueryString["OrgCode"];
                user = new User(OrgCode);
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center", NavigateUrl = String.Format("~/Reports/Center/CenterReport.aspx?OrgCode={0}", OrgCode) });
                if (user.AdminPortalVisible)
                {
                    menu.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center Admin", NavigateUrl = String.Format("~/Admin.aspx?OrgCode={0}", OrgCode), OuterCssClass = "adminMenuItem" });
                }
                if (user.IsSuperUser)
                {
                    AddSiftaAdminLinks(menu);
                }
            }
            else if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["CustomerID"]))
            {
                var CustomerID = Convert.ToInt32(HttpContext.Current.Request.QueryString["CustomerID"]);
                OrgCode = siftaDB.Customers.FirstOrDefault(p => p.CustomerID == CustomerID).OrgCode;
                user = new User(OrgCode);
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center", NavigateUrl = String.Format("~/Reports/Center/CenterReport.aspx?OrgCode={0}", OrgCode) });
                if (user.AdminPortalVisible)
                {
                    menu.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center Admin", NavigateUrl = String.Format("~/Admin.aspx?OrgCode={0}", OrgCode), OuterCssClass = "adminMenuItem" });
                }
                if (user.IsSuperUser)
                {
                    AddSiftaAdminLinks(menu);
                }
            }
            else if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["AgreementID"]))
            {
                var AgreementID = Convert.ToInt32(HttpContext.Current.Request.QueryString["AgreementID"]);
                OrgCode = siftaDB.Agreements.FirstOrDefault(p => p.AgreementID == AgreementID).Customer.OrgCode;
                user = new User(OrgCode);
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center", NavigateUrl = String.Format("~/Reports/Center/CenterReport.aspx?OrgCode={0}", OrgCode) });
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Agreement", NavigateUrl = AppendBaseURL(String.Format("Reports/Agreement/AgreementReport.aspx?AgreementID={0}", AgreementID)) });
                if (user.AdminPortalVisible)
                {
                    menu.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center Admin", NavigateUrl = String.Format("~/Admin.aspx?OrgCode={0}", OrgCode), OuterCssClass = "adminMenuItem" });
                }
                if (user.IsSuperUser)
                {
                    AddSiftaAdminLinks(menu);
                }
            }
            else if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SalesOrderNumber"]))
            {
                var SalesOrderNumber = HttpContext.Current.Request.QueryString["SalesorderNumber"];
                var agreement = siftaDB.Agreements.FirstOrDefault(p => p.SalesDocument == SalesOrderNumber);
                OrgCode = agreement.Customer.OrgCode;
                user = new User(OrgCode);
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center", NavigateUrl = String.Format("~/Reports/Center/CenterReport.aspx?OrgCode={0}", OrgCode) });
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Agreement", NavigateUrl = AppendBaseURL(String.Format("Reports/Agreement/AgreementReport.aspx?AgreementID={0}", agreement.AgreementID)) });
                if (user.AdminPortalVisible)
                {
                    menu.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center Admin", NavigateUrl = String.Format("~/Admin.aspx?OrgCode={0}", OrgCode), OuterCssClass = "adminMenuItem" });
                }
                if (user.IsSuperUser)
                {
                    AddSiftaAdminLinks(menu);
                }
            }
            else if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["PurchaseOrderNumber"]))
            {
                var PurchaseOrderNumber = HttpContext.Current.Request.QueryString["PurchaseOrderNumber"];
                var agreement = siftaDB.Agreements.FirstOrDefault(p => p.PurchaseOrderNumber == PurchaseOrderNumber);
                OrgCode = agreement.Customer.OrgCode;
                user = new User(OrgCode);
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center", NavigateUrl = String.Format("~/Reports/Center/CenterReport.aspx?OrgCode={0}", OrgCode) });
                reportsItem.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Agreement", NavigateUrl = AppendBaseURL(String.Format("Reports/Agreement/AgreementReport.aspx?AgreementID={0}", agreement.AgreementID)) });
                if (user.AdminPortalVisible)
                {
                    menu.Items.Add(new Telerik.Web.UI.RadMenuItem() { Text = "Center Admin", NavigateUrl = String.Format("~/Admin.aspx?OrgCode={0}", OrgCode), OuterCssClass = "adminMenuItem" });
                }
                if (user.IsSuperUser)
                {
                    AddSiftaAdminLinks(menu);
                }
            }
            else
            {
                user = new User();
                if (user.IsSuperUser)
                {
                    AddSiftaAdminLinks(menu);
                }
            }
        }
        private static void AddSiftaAdminLinks(Telerik.Web.UI.RadMenu menu)
        {
            var mainItem = new Telerik.Web.UI.RadMenuItem() { Text = "SIFTA Admin", OuterCssClass = "adminMenuItem" };
            mainItem.Items.Add(new Telerik.Web.UI.RadMenuItem("Metrics", "~/Reports/Metrics/national.aspx"));
            mainItem.Items.Add(new Telerik.Web.UI.RadMenuItem("Image Search", "~/Reports/Metrics/ImageSearch.aspx"));
            menu.Items.Add(mainItem);
        }
        #endregion

        #region Agreement
        public static DateTime? TrueStartDate(this Agreement agreement)
        {
            return agreement.AgreementMods.FirstOrDefault(p => p.Number == 0).StartDate;
        }
        public static DateTime? TrueEndDate(this Agreement agreement)
        {
            var mods = agreement.AgreementMods.Where(p=>p.EndDate != null).OrderByDescending(p => p.Number);
            if (mods.Count() > 0)
            {
                return mods.First().EndDate;
            }
            else return null;
        }
        public static Boolean HasDifference(this Agreement agreement)
        {
            var difference = agreement.FundingSummary()["difference"];
            var check = difference.Where(p => p.Value.USGS != 0 || p.Value.Customer != 0 || p.Value.Other != 0).ToList();
            if (check.Count == 0) return false; else return true;
        }
        /// <summary>
        /// Holds dictionary of records.
        /// Keys = "recorded", "allocatedSite", "allocatedStudies", "difference"
        /// </summary>
        /// <param name="agreement"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<int, FundingAmounts>> FundingSummary(this Agreement agreement)
        {
            #region Define Dictionaries
            //Holds all the records Dictionaries Key= "recorded", "allocated", "difference"
            var recordsDictionary = new Dictionary<string, Dictionary<int, FundingAmounts>>();
            //Holds the recorded funds from each agreement mod
            var recordedFunds = new Dictionary<int, FundingAmounts>();
            //Holds the allocated funds for each agreement mod
            var allocatedSiteFunds = new Dictionary<int, FundingAmounts>();
            //Holds the allocated Studies funding
            var allocatedStudiesFunds = new Dictionary<int, FundingAmounts>();
            //Holds the Differences for each mod
            var difference = new Dictionary<int, FundingAmounts>();
            #endregion

            #region Add Recorded and Allocated Funding
            //Goes through and adds the mod number and its associated funding amounts
            foreach (var mod in agreement.AgreementMods)
            {
                //Add the recorded funding to the mod
                recordedFunds.Add(mod.Number, new FundingAmounts() { USGS = mod.FundingUSGSCMF == null ? 0 : Convert.ToDouble(mod.FundingUSGSCMF), Customer = mod.FundingCustomer == null ? 0 : Convert.ToDouble(mod.FundingCustomer), Other = mod.FundingOther == null ? 0 : Convert.ToDouble(mod.FundingOther) });
                //Add a Site Funding for the mod
                allocatedSiteFunds.Add(mod.Number, new FundingAmounts() { Customer = 0, Other = 0, USGS = 0 });
                //Add a Studies Funding for the mod
                allocatedStudiesFunds.Add(mod.Number, new FundingAmounts() { Customer = 0, Other = 0, USGS = 0 });
                //Add a difference for the mod
                difference.Add(mod.Number, new FundingAmounts() { Customer = 0, Other = 0, USGS = 0 });
                //Add all of the site Funding to the allocated site funding list
                foreach (var sf in mod.FundingSites)
                {
                    //Grab a copy of the dictionary FundingAmounts Object
                    var funding = allocatedSiteFunds[mod.Number];
                    //Set values
                    funding.Customer += Convert.ToDouble(sf.FundingCustomer);
                    funding.USGS += Convert.ToDouble(sf.FundingUSGSCMF);
                    funding.Other += Convert.ToDouble(sf.FundingOther);
                    //Set the FundingAmounts in the dictionary equal to the one you altered.
                    allocatedSiteFunds[mod.Number] = funding;
                }
                foreach (var sf in mod.FundingStudies)
                {
                    //Grab a copy of the dictionary FundingAmounts Object
                    var funding = allocatedStudiesFunds[mod.Number];
                    //Set values
                    funding.Customer += Convert.ToDouble(sf.FundingCustomer);
                    funding.USGS += Convert.ToDouble(sf.FundingUSGSCMF);
                    funding.Other += Convert.ToDouble(sf.FundingOther);
                    //Set the FundingAmounts in the dictionary equal to the one you altered.
                    allocatedStudiesFunds[mod.Number] = funding;
                }
            }
            #endregion

            #region Calculate Difference
            foreach (var recordedFund in recordedFunds)
            {
                var recorded = recordedFund.Value;
                var siteAllocated = allocatedSiteFunds[recordedFund.Key];
                var studiesAllocated = allocatedStudiesFunds[recordedFund.Key];
                var diff = difference[recordedFund.Key];

                diff.USGS = recorded.USGS - siteAllocated.USGS - studiesAllocated.USGS;
                diff.Customer = recorded.Customer - siteAllocated.Customer - studiesAllocated.Customer;
                diff.Other = recorded.Other - siteAllocated.Other - studiesAllocated.Other;

                difference[recordedFund.Key] = diff;
            }
            #endregion

            #region Add Dictionaries to the recordsDictionary
            recordsDictionary.Add("recorded", recordedFunds);
            recordsDictionary.Add("allocatedSite", allocatedSiteFunds);
            recordsDictionary.Add("allocatedStudies", allocatedStudiesFunds);
            recordsDictionary.Add("difference", difference);
            #endregion
            return recordsDictionary;
        }
        public struct FundingAmounts
        {
            public double USGS, Customer, Other;
        }
        #endregion

        #region Double Formats
        public static Double ToDouble(this object o)
        {
            var str = o.ToString();
            if (String.IsNullOrEmpty(str)) return 0;
            str = str.Replace("$", "");
            return Convert.ToDouble(str);
        }
        #endregion

        #region DateFormats
        public static DateTime? ToDateTime(this object o)
        {
            if (String.IsNullOrEmpty(o.ToString())) return null;
            return Convert.ToDateTime(o);
        }
        #endregion

        #region String Formatting
        public static String ToDollars(this Double? d)
        {
            return String.Format("{0:c0}", Convert.ToDouble(d));
        }
        public static String ToDollars(this Double d)
        {
            return String.Format("{0:c0}", d);
        }
        /// <summary>
        /// Takes a string and returns a string of just the numbers found in that string.
        /// "123A45" -> "12345"
        /// </summary>
        /// <param name="number">The string this method is being invoked on</param>
        /// <returns>A string of the numbers in that string</returns>
        public static String GrabNumbers(this String number)
        {
            if (!String.IsNullOrEmpty(number)) return System.Text.RegularExpressions.Regex.Replace(number, @"[^\d]", ""); else return "";
        }

        public static String AppendTimeStamp(this string fileName)
        {
            //If it is empty return an empty string
            if (String.IsNullOrEmpty(fileName)) return "";
            //If it isn't a file .txt .something just return the string plus a timestamp
            if (!fileName.Contains('.')) return String.Format("{0}_{1}", fileName, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            //If not concatinate [fileName]_[Timestamp][.fileExtension]
            return String.Concat(fileName.Substring(0, fileName.LastIndexOf('.')), "_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.')));
        }

        /// <summary>
        /// Appends a base url to the path.
        /// Turns "/Images/Temp/stuff.png" into "urlpath/Images/Temp/stuff.png".
        /// </summary>
        /// <param name="path">The virtual path</param>
        /// <returns>A formatted direct path</returns>
        public static String AppendBaseURL(this string path)
        {
            String temp;
            String baseURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
            if (baseURL.Contains("sifta.water.usgs.gov")) baseURL += "/";
            if (String.IsNullOrEmpty(path)) return baseURL;
            temp = path.Replace("~/", "");
            if (path.StartsWith("/")) temp = temp.Substring(1, path.Length - 1);
            return String.Format("{0}/{1}", baseURL, temp);
        }

        public static String XMLSafe(this string str)
        {
            if (str == null) return "";
            return WebUtility.HtmlEncode(str);
        }
        #endregion

        #region Object Formatting
        /// <summary>
        /// Converts Object to string without throwing an error if they object is null
        /// </summary>
        /// <param name="obj">The either null or string object</param>
        /// <returns>The object as a string, or "" if the object is null</returns>
        public static String ObjToString(this object obj)
        {
            if (obj == null) return ""; else return obj.ToString();
        }
        #endregion

        #region Phone Formatting
        /// <summary>
        /// Takes a phone number strips it of all characters and then formats it for consistency. 
        /// (xxx) xxx - xxxx Ext xxxx
        /// </summary>
        /// <param name="number">The number before it is formatted</param>
        /// <returns>A formatted Phone number</returns>
        public static String ToPhoneFormat(this String number)
        {
            //Make sure a number exists
            if (!string.IsNullOrEmpty(number))
            {
                //Strips the string down to be only numbers
                number = System.Text.RegularExpressions.Regex.Replace(number, @"[^\d]", "");
                if (String.IsNullOrEmpty(number)) return "";
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

        /// <summary>
        /// Takes a phone number strips it of all characters and then formats it for consistency. 
        /// (xxx) xxx - xxxx Ext xxxx
        /// </summary>
        /// <param name="o">The number before it is formatted</param>
        /// <returns>A formatted Phone number</returns>
        public static String ToPhoneFormat(this Object o)
        {
            String number;
            if (o != null) number = o.ToString(); else return "";
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

        #region Site Formatting
        public static String ToJson(this List<Site> sites)
        {
            if (sites == null) return "";
            if (sites.Count == 0) return "";
            var json = "";
            String GeoJSONMainFormat = "{{\"type\":\"FeatureCollection\",\"features\":[{0}]}}";
            foreach(var site in sites)
            {
                json += site.ToJson() + ",";
            }
            //Get rid of the final , 
            json = json.Substring(0, json.Length - 1);
            return String.Format(GeoJSONMainFormat, json);
        }
        /// <summary>
        /// Formats a site object to JSON
        /// </summary>
        /// <param name="site">The Site that needs to be converted to JSON</param>
        /// <returns>A formatted String {info}</returns>
        public static String ToJson(this Site site)
        {
            string iconURL = "", siteType = "", realTime = "";
            if (site.RealTime == true) realTime = "Real-Time Site";
            switch (site.TypeCode)
            {
                //SW
                case "ES":
                case "GL":
                case "LK":
                case "OC":
                case "OC-CO":
                case "ST":
                case "ST-CA":
                case "ST-DCH":
                case "ST-TS":
                case "WE":
                    iconURL = "sw";
                    siteType = "Surface Water";
                    break;
                //GW
                case "GW":
                case "GW-CR":
                case "GW-EX":
                case "GW-HZ":
                case "GW-IW":
                case "GW-MW":
                case "GW-TH":
                case "SB":
                case "SB-CV":
                case "SB-GWD":
                case "SB-TSM":
                case "SB-UZ":
                    iconURL = "gw";
                    siteType = "Ground Water";
                    break;
                //AT
                case "AT":
                    iconURL = "at";
                    siteType = "Atmospheric";
                    break;
                //DEFAULT
                default:
                    iconURL = "ot";
                    siteType = "Other";
                    break;
            }
            var nsipURL = String.Format(AppendBaseURL("Images/FPSScores/FPS{0}.gif"), site.FPSScore);
            String GeoJSONSiteFormat = "{\"type\":\"Feature\",\"properties\":{\"SiteNumber\":\"" + site.SiteNumber + "\",\"SiteName\":\"" + site.Name + "\",\"iconURL\":\"" + iconURL + "\",\"NSIPImg\": \"" + nsipURL + "\", \"SiteType\": \"" + siteType + "\",\"RealTimeSite\": \"" + realTime + "\"}, \"geometry\":{\"type\":\"Point\",\"coordinates\":[" + site.Longitude.ToString() + "," + site.Latitude.ToString() + "]}}";
            return GeoJSONSiteFormat;
        }
        #endregion

        #region Contacts
        /// <summary>
        /// Formats the Full name of an Employee Object
        /// </summary>
        /// <param name="contact">The Employee object this method is being invoked on</param>
        /// <returns>The Formatted Full Name of the contact</returns>
        public static String FullName(this Employee contact)
        {
            if (contact == null) return "";
            if (!String.IsNullOrEmpty(contact.MiddleName))
            {
                return String.Format("{0} {1} {2}", contact.FirstName, contact.MiddleName, contact.LastName);
            }
            else
            {
                return String.Format("{0} {1}", contact.FirstName, contact.LastName);
            }
        }

        /// <summary>
        ///  Formats the Full name of a CustomerContact Object
        /// </summary>
        /// <param name="contact">The CustomerContact object this method is being invoked on</param>
        /// <returns>The Formatted Full Name of the contact</returns>
        public static String FullName(this CustomerContact contact)
        {
            if (contact == null) return "";
            if (!String.IsNullOrEmpty(contact.MiddleName))
            {
                return String.Format("{0} {1} {2}", contact.FirstName, contact.MiddleName, contact.LastName);
            }
            else
            {
                return String.Format("{0} {1}", contact.FirstName, contact.LastName);
            }
        }
        #endregion
    }
}