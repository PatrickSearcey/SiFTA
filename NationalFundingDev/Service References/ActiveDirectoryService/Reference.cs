﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NationalFundingDev.ActiveDirectoryService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Employee", Namespace="http://schemas.datacontract.org/2004/07/SiftaWebServices.WCF.Employee")]
    [System.SerializableAttribute()]
    public partial class Employee : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DepartmentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmployeeIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MiddleNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrgCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneFaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneWorkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StreetOneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StreetTwoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ZipCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Department {
            get {
                return this.DepartmentField;
            }
            set {
                if ((object.ReferenceEquals(this.DepartmentField, value) != true)) {
                    this.DepartmentField = value;
                    this.RaisePropertyChanged("Department");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmployeeID {
            get {
                return this.EmployeeIDField;
            }
            set {
                if ((object.ReferenceEquals(this.EmployeeIDField, value) != true)) {
                    this.EmployeeIDField = value;
                    this.RaisePropertyChanged("EmployeeID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MiddleName {
            get {
                return this.MiddleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MiddleNameField, value) != true)) {
                    this.MiddleNameField = value;
                    this.RaisePropertyChanged("MiddleName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrgCode {
            get {
                return this.OrgCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.OrgCodeField, value) != true)) {
                    this.OrgCodeField = value;
                    this.RaisePropertyChanged("OrgCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneFax {
            get {
                return this.PhoneFaxField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneFaxField, value) != true)) {
                    this.PhoneFaxField = value;
                    this.RaisePropertyChanged("PhoneFax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneWork {
            get {
                return this.PhoneWorkField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneWorkField, value) != true)) {
                    this.PhoneWorkField = value;
                    this.RaisePropertyChanged("PhoneWork");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StreetOne {
            get {
                return this.StreetOneField;
            }
            set {
                if ((object.ReferenceEquals(this.StreetOneField, value) != true)) {
                    this.StreetOneField = value;
                    this.RaisePropertyChanged("StreetOne");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StreetTwo {
            get {
                return this.StreetTwoField;
            }
            set {
                if ((object.ReferenceEquals(this.StreetTwoField, value) != true)) {
                    this.StreetTwoField = value;
                    this.RaisePropertyChanged("StreetTwo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ZipCode {
            get {
                return this.ZipCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ZipCodeField, value) != true)) {
                    this.ZipCodeField = value;
                    this.RaisePropertyChanged("ZipCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ActiveDirectoryService.IActiveDirectory")]
    public interface IActiveDirectory {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IActiveDirectory/GetEmployee", ReplyAction="http://tempuri.org/IActiveDirectory/GetEmployeeResponse")]
        NationalFundingDev.ActiveDirectoryService.Employee GetEmployee(string EmployeeID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IActiveDirectory/GetEmployee", ReplyAction="http://tempuri.org/IActiveDirectory/GetEmployeeResponse")]
        System.Threading.Tasks.Task<NationalFundingDev.ActiveDirectoryService.Employee> GetEmployeeAsync(string EmployeeID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IActiveDirectoryChannel : NationalFundingDev.ActiveDirectoryService.IActiveDirectory, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ActiveDirectoryClient : System.ServiceModel.ClientBase<NationalFundingDev.ActiveDirectoryService.IActiveDirectory>, NationalFundingDev.ActiveDirectoryService.IActiveDirectory {
        
        public ActiveDirectoryClient() {
        }
        
        public ActiveDirectoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ActiveDirectoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ActiveDirectoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ActiveDirectoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public NationalFundingDev.ActiveDirectoryService.Employee GetEmployee(string EmployeeID) {
            return base.Channel.GetEmployee(EmployeeID);
        }
        
        public System.Threading.Tasks.Task<NationalFundingDev.ActiveDirectoryService.Employee> GetEmployeeAsync(string EmployeeID) {
            return base.Channel.GetEmployeeAsync(EmployeeID);
        }
    }
}
