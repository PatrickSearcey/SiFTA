using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

namespace NationalFundingDev
{
    public class ActiveDirectoryService
    {
        private SearchResult _result;
        public Employee GetEmployee(String EmployeeID)
        {
            if (UserInActiveDirectory(EmployeeID))
            {
                return CreateEmployeeFromAD(EmployeeID);
            }
            else
            {
                return null;
            }
        }
        private Employee CreateEmployeeFromAD(String EmployeeID)
        {

            var employee = new Employee();
            employee.EmployeeID = EmployeeID;
            employee.Title = GetProperty("title");
            employee.Department = GetProperty("department");
            employee.PhoneFax = GetProperty("facsimileTelephoneNumber");
            employee.PhoneWork = GetProperty("telephoneNumber");
            employee.Email = GetProperty("mail");

            #region Address
            //Grab the address comes as 1505 Ferguson Lane, , Austin, TX, 78754-4501, US and split them
            var address = GetProperty("street").Split(',');
            switch (address.Length)
            {
                case 5:
                    //Street One
                    employee.StreetOne = address[0];
                    //City
                    employee.City = address[1];
                    //State
                    employee.State = address[2];
                    //Zip Code
                    employee.ZipCode = address[3];
                    break;
                case 6:
                    //Street 1
                    employee.StreetOne = address[0];
                    //Street 2
                    employee.StreetTwo = address[1];
                    //City
                    employee.City = address[2];
                    //State
                    employee.State = address[3];
                    //Zip Code
                    employee.ZipCode = address[4];
                    break;
                default:
                    break;
            }
            #endregion

            #region Name
            //Split the name based on the spaces Justin K Robertson
            var name = GetProperty("cn").Split(' ');
            switch (name.Length)
            {
                case 2:
                    employee.FirstName = name[0];
                    employee.LastName = name[1];
                    break;
                case 3:
                    employee.FirstName = name[0];
                    employee.MiddleName = name[1];
                    employee.LastName = name[2];
                    break;
                default:
                    break;
            }
            #endregion  

            #region OrgCode
            var tempOrg = GetProperty("extensionattribute8").ToCharArray();
            if (tempOrg.Count() > 6)
            {
                var orgCodeArray = new Char[4] { tempOrg[1], tempOrg[2], tempOrg[4], tempOrg[5] };
                employee.OrgCode = new String(orgCodeArray);
            }
            else
            {
                employee.OrgCode = "GCSJ";
            }
            #endregion

            if (employee.StreetTwo == "YYYY") employee.StreetTwo = null;

            return employee;
        }
        private bool UserInActiveDirectory(String EmployeeID)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://gs.doi.net");
            DirectorySearcher dSearch = new DirectorySearcher(entry);
            dSearch.Filter = String.Format("(&((&(objectCategory=Person)(objectClass=User)))(samaccountname={0}))", EmployeeID);
            try
            {
                _result = dSearch.FindOne();
                entry.Close();
                if (_result != null) return true; else return false;
            }
            catch (Exception ex)
            {
                entry.Close();
                return false;
            }
        }

        /// <summary>
        /// Grabs a property from an active directory search result
        /// </summary>
        /// <param name="searchResult">The Search Result that we are querying (a person in AD)</param>
        /// <param name="PropertyName">The name of the property we wish to grab for this person</param>
        private string GetProperty(string PropertyName)
        {
            if (_result.Properties.Contains(PropertyName))
            {
                return _result.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}