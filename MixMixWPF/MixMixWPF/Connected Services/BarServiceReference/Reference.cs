﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MixMixWPF.BarServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Address", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Address : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MixMixWPF.BarServiceReference.Zipcode ZipcodeField;
        
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
        public string AddressName {
            get {
                return this.AddressNameField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressNameField, value) != true)) {
                    this.AddressNameField = value;
                    this.RaisePropertyChanged("AddressName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MixMixWPF.BarServiceReference.Zipcode Zipcode {
            get {
                return this.ZipcodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ZipcodeField, value) != true)) {
                    this.ZipcodeField = value;
                    this.RaisePropertyChanged("Zipcode");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Zipcode", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Zipcode : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MixMixWPF.BarServiceReference.Country CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ZipField;
        
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
        public string CityName {
            get {
                return this.CityNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CityNameField, value) != true)) {
                    this.CityNameField = value;
                    this.RaisePropertyChanged("CityName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MixMixWPF.BarServiceReference.Country Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Zip {
            get {
                return this.ZipField;
            }
            set {
                if ((object.ReferenceEquals(this.ZipField, value) != true)) {
                    this.ZipField = value;
                    this.RaisePropertyChanged("Zip");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Country", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Country : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
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
        public string CountryName {
            get {
                return this.CountryNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryNameField, value) != true)) {
                    this.CountryNameField = value;
                    this.RaisePropertyChanged("CountryName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Contact", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MixMixWPF.BarServiceReference.Bar))]
    public partial class Contact : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
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
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNumber {
            get {
                return this.PhoneNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNumberField, value) != true)) {
                    this.PhoneNumberField = value;
                    this.RaisePropertyChanged("PhoneNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Bar", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Bar : MixMixWPF.BarServiceReference.Contact {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MixMixWPF.BarServiceReference.Address AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MixMixWPF.BarServiceReference.Address Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Ingredient", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Ingredient : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double AlchField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool MeasurableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
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
        public double Alch {
            get {
                return this.AlchField;
            }
            set {
                if ((this.AlchField.Equals(value) != true)) {
                    this.AlchField = value;
                    this.RaisePropertyChanged("Alch");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Measurable {
            get {
                return this.MeasurableField;
            }
            set {
                if ((this.MeasurableField.Equals(value) != true)) {
                    this.MeasurableField = value;
                    this.RaisePropertyChanged("Measurable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Measurement", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Measurement : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ProportionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
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
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Proportion {
            get {
                return this.ProportionField;
            }
            set {
                if ((this.ProportionField.Equals(value) != true)) {
                    this.ProportionField = value;
                    this.RaisePropertyChanged("Proportion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Stock", Namespace="http://schemas.datacontract.org/2004/07/Entities")]
    [System.SerializableAttribute()]
    public partial class Stock : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MixMixWPF.BarServiceReference.Ingredient IngredientField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double QuantityField;
        
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
        public MixMixWPF.BarServiceReference.Ingredient Ingredient {
            get {
                return this.IngredientField;
            }
            set {
                if ((object.ReferenceEquals(this.IngredientField, value) != true)) {
                    this.IngredientField = value;
                    this.RaisePropertyChanged("Ingredient");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Quantity {
            get {
                return this.QuantityField;
            }
            set {
                if ((this.QuantityField.Equals(value) != true)) {
                    this.QuantityField = value;
                    this.RaisePropertyChanged("Quantity");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BarServiceReference.IBarService")]
    public interface IBarService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Create", ReplyAction="http://tempuri.org/IBarService/CreateResponse")]
        MixMixWPF.BarServiceReference.Bar Create(int managerId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Create", ReplyAction="http://tempuri.org/IBarService/CreateResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar> CreateAsync(int managerId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Find", ReplyAction="http://tempuri.org/IBarService/FindResponse")]
        MixMixWPF.BarServiceReference.Bar Find(int barId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Find", ReplyAction="http://tempuri.org/IBarService/FindResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar> FindAsync(int barId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Update", ReplyAction="http://tempuri.org/IBarService/UpdateResponse")]
        bool Update(int barId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Update", ReplyAction="http://tempuri.org/IBarService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(int barId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetBarsByManagerId", ReplyAction="http://tempuri.org/IBarService/GetBarsByManagerIdResponse")]
        MixMixWPF.BarServiceReference.Bar[] GetBarsByManagerId(int managerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetBarsByManagerId", ReplyAction="http://tempuri.org/IBarService/GetBarsByManagerIdResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar[]> GetBarsByManagerIdAsync(int managerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/FindAll", ReplyAction="http://tempuri.org/IBarService/FindAllResponse")]
        MixMixWPF.BarServiceReference.Bar[] FindAll(string zipcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/FindAll", ReplyAction="http://tempuri.org/IBarService/FindAllResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar[]> FindAllAsync(string zipcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Delete", ReplyAction="http://tempuri.org/IBarService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/Delete", ReplyAction="http://tempuri.org/IBarService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/CreateStock", ReplyAction="http://tempuri.org/IBarService/CreateStockResponse")]
        void CreateStock(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient, int measurementId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/CreateStock", ReplyAction="http://tempuri.org/IBarService/CreateStockResponse")]
        System.Threading.Tasks.Task CreateStockAsync(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient, int measurementId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/CreateNonMeasurableStock", ReplyAction="http://tempuri.org/IBarService/CreateNonMeasurableStockResponse")]
        void CreateNonMeasurableStock(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/CreateNonMeasurableStock", ReplyAction="http://tempuri.org/IBarService/CreateNonMeasurableStockResponse")]
        System.Threading.Tasks.Task CreateNonMeasurableStockAsync(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetAllIngredients", ReplyAction="http://tempuri.org/IBarService/GetAllIngredientsResponse")]
        MixMixWPF.BarServiceReference.Ingredient[] GetAllIngredients();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetAllIngredients", ReplyAction="http://tempuri.org/IBarService/GetAllIngredientsResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Ingredient[]> GetAllIngredientsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetAllMeasurements", ReplyAction="http://tempuri.org/IBarService/GetAllMeasurementsResponse")]
        MixMixWPF.BarServiceReference.Measurement[] GetAllMeasurements();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetAllMeasurements", ReplyAction="http://tempuri.org/IBarService/GetAllMeasurementsResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Measurement[]> GetAllMeasurementsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetAllStocks", ReplyAction="http://tempuri.org/IBarService/GetAllStocksResponse")]
        MixMixWPF.BarServiceReference.Stock[] GetAllStocks(int barId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/GetAllStocks", ReplyAction="http://tempuri.org/IBarService/GetAllStocksResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Stock[]> GetAllStocksAsync(int barId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/UpdateStock", ReplyAction="http://tempuri.org/IBarService/UpdateStockResponse")]
        bool UpdateStock(MixMixWPF.BarServiceReference.Stock[] stock, int barid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/UpdateStock", ReplyAction="http://tempuri.org/IBarService/UpdateStockResponse")]
        System.Threading.Tasks.Task<bool> UpdateStockAsync(MixMixWPF.BarServiceReference.Stock[] stock, int barid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/FindIngredient", ReplyAction="http://tempuri.org/IBarService/FindIngredientResponse")]
        MixMixWPF.BarServiceReference.Ingredient FindIngredient(int ingredientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/FindIngredient", ReplyAction="http://tempuri.org/IBarService/FindIngredientResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Ingredient> FindIngredientAsync(int ingredientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/FindMeasurement", ReplyAction="http://tempuri.org/IBarService/FindMeasurementResponse")]
        MixMixWPF.BarServiceReference.Measurement FindMeasurement(int measurementId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBarService/FindMeasurement", ReplyAction="http://tempuri.org/IBarService/FindMeasurementResponse")]
        System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Measurement> FindMeasurementAsync(int measurementId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBarServiceChannel : MixMixWPF.BarServiceReference.IBarService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BarServiceClient : System.ServiceModel.ClientBase<MixMixWPF.BarServiceReference.IBarService>, MixMixWPF.BarServiceReference.IBarService {
        
        public BarServiceClient() {
        }
        
        public BarServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BarServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BarServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BarServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MixMixWPF.BarServiceReference.Bar Create(int managerId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username, string password) {
            return base.Channel.Create(managerId, name, address, phonenumber, email, username, password);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar> CreateAsync(int managerId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username, string password) {
            return base.Channel.CreateAsync(managerId, name, address, phonenumber, email, username, password);
        }
        
        public MixMixWPF.BarServiceReference.Bar Find(int barId) {
            return base.Channel.Find(barId);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar> FindAsync(int barId) {
            return base.Channel.FindAsync(barId);
        }
        
        public bool Update(int barId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username) {
            return base.Channel.Update(barId, name, address, phonenumber, email, username);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(int barId, string name, MixMixWPF.BarServiceReference.Address address, string phonenumber, string email, string username) {
            return base.Channel.UpdateAsync(barId, name, address, phonenumber, email, username);
        }
        
        public MixMixWPF.BarServiceReference.Bar[] GetBarsByManagerId(int managerId) {
            return base.Channel.GetBarsByManagerId(managerId);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar[]> GetBarsByManagerIdAsync(int managerId) {
            return base.Channel.GetBarsByManagerIdAsync(managerId);
        }
        
        public MixMixWPF.BarServiceReference.Bar[] FindAll(string zipcode) {
            return base.Channel.FindAll(zipcode);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Bar[]> FindAllAsync(string zipcode) {
            return base.Channel.FindAllAsync(zipcode);
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public void CreateStock(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient, int measurementId) {
            base.Channel.CreateStock(barId, quantity, ingredient, measurementId);
        }
        
        public System.Threading.Tasks.Task CreateStockAsync(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient, int measurementId) {
            return base.Channel.CreateStockAsync(barId, quantity, ingredient, measurementId);
        }
        
        public void CreateNonMeasurableStock(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient) {
            base.Channel.CreateNonMeasurableStock(barId, quantity, ingredient);
        }
        
        public System.Threading.Tasks.Task CreateNonMeasurableStockAsync(int barId, double quantity, MixMixWPF.BarServiceReference.Ingredient ingredient) {
            return base.Channel.CreateNonMeasurableStockAsync(barId, quantity, ingredient);
        }
        
        public MixMixWPF.BarServiceReference.Ingredient[] GetAllIngredients() {
            return base.Channel.GetAllIngredients();
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Ingredient[]> GetAllIngredientsAsync() {
            return base.Channel.GetAllIngredientsAsync();
        }
        
        public MixMixWPF.BarServiceReference.Measurement[] GetAllMeasurements() {
            return base.Channel.GetAllMeasurements();
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Measurement[]> GetAllMeasurementsAsync() {
            return base.Channel.GetAllMeasurementsAsync();
        }
        
        public MixMixWPF.BarServiceReference.Stock[] GetAllStocks(int barId) {
            return base.Channel.GetAllStocks(barId);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Stock[]> GetAllStocksAsync(int barId) {
            return base.Channel.GetAllStocksAsync(barId);
        }
        
        public bool UpdateStock(MixMixWPF.BarServiceReference.Stock[] stock, int barid) {
            return base.Channel.UpdateStock(stock, barid);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateStockAsync(MixMixWPF.BarServiceReference.Stock[] stock, int barid) {
            return base.Channel.UpdateStockAsync(stock, barid);
        }
        
        public MixMixWPF.BarServiceReference.Ingredient FindIngredient(int ingredientId) {
            return base.Channel.FindIngredient(ingredientId);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Ingredient> FindIngredientAsync(int ingredientId) {
            return base.Channel.FindIngredientAsync(ingredientId);
        }
        
        public MixMixWPF.BarServiceReference.Measurement FindMeasurement(int measurementId) {
            return base.Channel.FindMeasurement(measurementId);
        }
        
        public System.Threading.Tasks.Task<MixMixWPF.BarServiceReference.Measurement> FindMeasurementAsync(int measurementId) {
            return base.Channel.FindMeasurementAsync(measurementId);
        }
    }
}
