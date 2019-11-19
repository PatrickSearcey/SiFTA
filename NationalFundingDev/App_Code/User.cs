using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalFundingDev
{
    /// <summary>
    /// The User Class grabs the userID from windows authentication.
    /// </summary>
    public class User
    {
        #region Local Variables
        private String user_id, _OrgCode, _Home;
        private Boolean _CanInsert, _CanUpdate, _CanDelete, _IsAdmin, _IsCenterAdmin;
        private List<string> SuperUsers = new List<string>(){
            "bdreece", "sholl", "slvasque", "rjneafie", "epease", "afox", "kesmith", "dterry", "cfanguy"
        };
        private EmployeeAccess permissions;
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        #endregion

        #region Constructors
        /// <summary>
        /// The Default Constructor
        /// Sets the org code to one that they have permissions to edit
        /// </summary>
        public User()
        {
            user_id = HttpContext.Current.User.Identity.Name.Replace("GS\\", "").Replace("-pr", "");
            var employee = siftaDB.Employees.FirstOrDefault(p => p.EmployeeID == user_id);
            if(employee != null)
            {
                _Home = employee.OrgCode;
            }
            //Set permissions to false
            _CanInsert = _CanUpdate = _CanDelete = _IsAdmin = _IsCenterAdmin = false;
        }
        /// <summary>
        /// Overloaded Constructor
        /// Allows for permissions to be established for a Center
        /// </summary>
        /// <param name="OrgCode">The Org Code of the Current Page</param>
        public User(string OrgCode)
        {
            //Set the Org Code
            _OrgCode = OrgCode;
            //Grabs the Users Identity from Windows Authentication and strips the unnecessary parts
            user_id = HttpContext.Current.User.Identity.Name.Replace("GS\\", "").Replace("-pr", "");
            //Grab Employee information
            var employee = siftaDB.Employees.FirstOrDefault(p => p.EmployeeID == user_id);
            if(employee!= null)
            {
                //Sets Home to be the org code the user is with.
                _Home = employee.OrgCode;
                //Grabs permissions from the database
                permissions = siftaDB.EmployeeAccesses.FirstOrDefault(p => p.EmployeeID == user_id && p.OrgCode == OrgCode);
                //If they haven't been given any permissions
                if (permissions == null)
                {
                    //Set permissions to false
                    _CanInsert = _CanUpdate = _CanDelete = _IsAdmin = _IsCenterAdmin = false;
                }
                else
                {
                    //They have been given permissions
                    _CanInsert = permissions.CanInsertRecords;
                    _CanUpdate = permissions.CanUpdateRecords;
                    _CanDelete = permissions.CanDeleteRecords;
                    _IsAdmin = permissions.CanViewAdminPortal;
                    _IsCenterAdmin = permissions.CenterAdmin;
                }
            }
            else
            {
                //Set permissions to false
                _CanInsert = _CanUpdate = _CanDelete = _IsAdmin = _IsCenterAdmin = false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns True if the user is a Center Admin
        /// </summary>
        public Boolean IsCenterAdmin
        {
            get
            {
                return _IsCenterAdmin;
            }
        }

        /// <summary>
        /// The ID of the user ex:jdoe
        /// </summary>
        public String ID
        {
            get
            {
                return user_id;
            }
        }
        /// <summary>
        /// Returns the OrgCode Associated with the current User
        /// </summary>
        public String OrgCode
        {
            get
            {
                return _OrgCode;
            }
        }

        /// <summary>
        /// Returns True if the user is a Super User
        /// </summary>
        public Boolean IsSuperUser
        {
            get
            {
                return SuperUsers.Contains(user_id);
            }
        }
        /// <summary>
        /// Returns True if the User is an Admin for this center
        /// </summary>
        public Boolean IsAdmin
        {
            get
            {
                if (this.IsSuperUser) return true;
                else if (_IsAdmin) return true; else return false;
            }
        }
        /// <summary>
        /// Returns True if the User can insert information for this Center
        /// </summary>
        public Boolean CanInsert
        {
            get
            {
                if (this.IsSuperUser) return true;
                return _CanInsert;
            }
        }
        /// <summary>
        /// Returns True if the User can update information for this Center
        /// </summary>
        public Boolean CanUpdate
        {
            get
            {
                if (this.IsSuperUser) return true;
                return _CanUpdate;
            }
        }
        /// <summary>
        /// Returns True if the User can delete information for this Center
        /// </summary>
        public Boolean CanDelete
        {
            get
            {
                if (this.IsSuperUser) return true;
                return _CanDelete;
            }
        }
        /// <summary>
        /// Returns True if the User can view the Admin Portal for this Center
        /// </summary>
        public Boolean AdminPortalVisible
        {
            get
            {
                if (String.IsNullOrEmpty(_OrgCode)) return false;
                if (this.IsSuperUser) return true;
                return IsAdmin;
            }
        }

        /// <summary>
        /// Returns the OrgCode Associated with the user
        /// </summary>
        public String Home
        {
            get
            {
                return _Home;
            }
        }
        #endregion
    }
}