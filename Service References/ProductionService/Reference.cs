﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ortoped.ProductionService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductionAidSearchFilter", Namespace="http://schemas.datacontract.org/2004/07/Common.DTO")]
    [System.SerializableAttribute()]
    public partial class ProductionAidSearchFilter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BoxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HolderField;
        
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
        public string Box {
            get {
                return this.BoxField;
            }
            set {
                if ((object.ReferenceEquals(this.BoxField, value) != true)) {
                    this.BoxField = value;
                    this.RaisePropertyChanged("Box");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyId {
            get {
                return this.CompanyIdField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyIdField, value) != true)) {
                    this.CompanyIdField = value;
                    this.RaisePropertyChanged("CompanyId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Holder {
            get {
                return this.HolderField;
            }
            set {
                if ((object.ReferenceEquals(this.HolderField, value) != true)) {
                    this.HolderField = value;
                    this.RaisePropertyChanged("Holder");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductionAidDTO", Namespace="http://schemas.datacontract.org/2004/07/Common.DTO")]
    [System.SerializableAttribute()]
    public partial class ProductionAidDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AidIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArtNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArtNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BoxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreateDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HolderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductionStateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime PromisedDeliverDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool UrgentField;
        
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
        public int AidId {
            get {
                return this.AidIdField;
            }
            set {
                if ((this.AidIdField.Equals(value) != true)) {
                    this.AidIdField = value;
                    this.RaisePropertyChanged("AidId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArtName {
            get {
                return this.ArtNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ArtNameField, value) != true)) {
                    this.ArtNameField = value;
                    this.RaisePropertyChanged("ArtName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArtNo {
            get {
                return this.ArtNoField;
            }
            set {
                if ((object.ReferenceEquals(this.ArtNoField, value) != true)) {
                    this.ArtNoField = value;
                    this.RaisePropertyChanged("ArtNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Box {
            get {
                return this.BoxField;
            }
            set {
                if ((object.ReferenceEquals(this.BoxField, value) != true)) {
                    this.BoxField = value;
                    this.RaisePropertyChanged("Box");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyId {
            get {
                return this.CompanyIdField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyIdField, value) != true)) {
                    this.CompanyIdField = value;
                    this.RaisePropertyChanged("CompanyId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CreateDate {
            get {
                return this.CreateDateField;
            }
            set {
                if ((object.ReferenceEquals(this.CreateDateField, value) != true)) {
                    this.CreateDateField = value;
                    this.RaisePropertyChanged("CreateDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Holder {
            get {
                return this.HolderField;
            }
            set {
                if ((object.ReferenceEquals(this.HolderField, value) != true)) {
                    this.HolderField = value;
                    this.RaisePropertyChanged("Holder");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderNo {
            get {
                return this.OrderNoField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderNoField, value) != true)) {
                    this.OrderNoField = value;
                    this.RaisePropertyChanged("OrderNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Price {
            get {
                return this.PriceField;
            }
            set {
                if ((object.ReferenceEquals(this.PriceField, value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductionState {
            get {
                return this.ProductionStateField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductionStateField, value) != true)) {
                    this.ProductionStateField = value;
                    this.RaisePropertyChanged("ProductionState");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime PromisedDeliverDate {
            get {
                return this.PromisedDeliverDateField;
            }
            set {
                if ((this.PromisedDeliverDateField.Equals(value) != true)) {
                    this.PromisedDeliverDateField = value;
                    this.RaisePropertyChanged("PromisedDeliverDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Row {
            get {
                return this.RowField;
            }
            set {
                if ((this.RowField.Equals(value) != true)) {
                    this.RowField = value;
                    this.RaisePropertyChanged("Row");
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
        public bool Urgent {
            get {
                return this.UrgentField;
            }
            set {
                if ((this.UrgentField.Equals(value) != true)) {
                    this.UrgentField = value;
                    this.RaisePropertyChanged("Urgent");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductionAidUpdateFilter", Namespace="http://schemas.datacontract.org/2004/07/Common.DTO")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Ortoped.ProductionService.ProductionAidSetHolder))]
    public partial class ProductionAidUpdateFilter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AidIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderNoField;
        
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
        public int AidId {
            get {
                return this.AidIdField;
            }
            set {
                if ((this.AidIdField.Equals(value) != true)) {
                    this.AidIdField = value;
                    this.RaisePropertyChanged("AidId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyId {
            get {
                return this.CompanyIdField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyIdField, value) != true)) {
                    this.CompanyIdField = value;
                    this.RaisePropertyChanged("CompanyId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderNo {
            get {
                return this.OrderNoField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderNoField, value) != true)) {
                    this.OrderNoField = value;
                    this.RaisePropertyChanged("OrderNo");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductionAidSetHolder", Namespace="http://schemas.datacontract.org/2004/07/Common.DTO")]
    [System.SerializableAttribute()]
    public partial class ProductionAidSetHolder : Ortoped.ProductionService.ProductionAidUpdateFilter {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SetToThisHolderField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SetToThisHolder {
            get {
                return this.SetToThisHolderField;
            }
            set {
                if ((object.ReferenceEquals(this.SetToThisHolderField, value) != true)) {
                    this.SetToThisHolderField = value;
                    this.RaisePropertyChanged("SetToThisHolder");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductionService.IOrderService", CallbackContract=typeof(Ortoped.ProductionService.IOrderServiceCallback))]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetProductionAidList", ReplyAction="http://tempuri.org/IOrderService/GetProductionAidListResponse")]
        Ortoped.ProductionService.ProductionAidDTO[] GetProductionAidList(Ortoped.ProductionService.ProductionAidSearchFilter filter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/UpdateAid", ReplyAction="http://tempuri.org/IOrderService/UpdateAidResponse")]
        bool UpdateAid(Ortoped.ProductionService.ProductionAidDTO aid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/ReturnAidToBox", ReplyAction="http://tempuri.org/IOrderService/ReturnAidToBoxResponse")]
        bool ReturnAidToBox(Ortoped.ProductionService.ProductionAidUpdateFilter filter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/SetProductionAidHolder", ReplyAction="http://tempuri.org/IOrderService/SetProductionAidHolderResponse")]
        bool SetProductionAidHolder(Ortoped.ProductionService.ProductionAidSetHolder filter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/SendMessage", ReplyAction="http://tempuri.org/IOrderService/SendMessageResponse")]
        void SendMessage(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/Subscribe", ReplyAction="http://tempuri.org/IOrderService/SubscribeResponse")]
        bool Subscribe(string company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/UnSubscribe", ReplyAction="http://tempuri.org/IOrderService/UnSubscribeResponse")]
        bool UnSubscribe(string company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/TestService", ReplyAction="http://tempuri.org/IOrderService/TestServiceResponse")]
        void TestService(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOrderService/OnMessageAdded")]
        void OnMessageAdded(Ortoped.ProductionService.ProductionAidDTO order, System.DateTime timestamp);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : Ortoped.ProductionService.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.DuplexClientBase<Ortoped.ProductionService.IOrderService>, Ortoped.ProductionService.IOrderService {
        
        public OrderServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public OrderServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public OrderServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public Ortoped.ProductionService.ProductionAidDTO[] GetProductionAidList(Ortoped.ProductionService.ProductionAidSearchFilter filter) {
            return base.Channel.GetProductionAidList(filter);
        }
        
        public bool UpdateAid(Ortoped.ProductionService.ProductionAidDTO aid) {
            return base.Channel.UpdateAid(aid);
        }
        
        public bool ReturnAidToBox(Ortoped.ProductionService.ProductionAidUpdateFilter filter) {
            return base.Channel.ReturnAidToBox(filter);
        }
        
        public bool SetProductionAidHolder(Ortoped.ProductionService.ProductionAidSetHolder filter) {
            return base.Channel.SetProductionAidHolder(filter);
        }
        
        public void SendMessage(string message) {
            base.Channel.SendMessage(message);
        }
        
        public bool Subscribe(string company) {
            return base.Channel.Subscribe(company);
        }
        
        public bool UnSubscribe(string company) {
            return base.Channel.UnSubscribe(company);
        }
        
        public void TestService(string message) {
            base.Channel.TestService(message);
        }
    }
}