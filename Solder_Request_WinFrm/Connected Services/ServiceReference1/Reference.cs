﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Solder_Request_WinFrm.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.WebServiceToOEESoap")]
    public interface WebServiceToOEESoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<string> HelloWorldAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetListD_ToOEE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Entity))]
        Solder_Request_WinFrm.ServiceReference1.D_ToOEE[] GetListD_ToOEE(string _tempLineId, System.DateTime _tempBeginTime, System.DateTime _tempEndTime, string _tempExceptionState);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetListD_ToOEE", ReplyAction="*")]
        System.Threading.Tasks.Task<Solder_Request_WinFrm.ServiceReference1.D_ToOEE[]> GetListD_ToOEEAsync(string _tempLineId, System.DateTime _tempBeginTime, System.DateTime _tempEndTime, string _tempExceptionState);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class D_ToOEE : Entity {
        
        private string toOEEIdField;
        
        private string andonRecordHistoryIdField;
        
        private string lineIdField;
        
        private string lineNameField;
        
        private System.DateTime beginTimeField;
        
        private System.DateTime endTimeField;
        
        private decimal downtimeField;
        
        private string measureField;
        
        private string exceptionStateField;
        
        private string failureContentField;
        
        private string failureTitleField;
        
        private string failureTitleTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ToOEEId {
            get {
                return this.toOEEIdField;
            }
            set {
                this.toOEEIdField = value;
                this.RaisePropertyChanged("ToOEEId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string AndonRecordHistoryId {
            get {
                return this.andonRecordHistoryIdField;
            }
            set {
                this.andonRecordHistoryIdField = value;
                this.RaisePropertyChanged("AndonRecordHistoryId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string LineId {
            get {
                return this.lineIdField;
            }
            set {
                this.lineIdField = value;
                this.RaisePropertyChanged("LineId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string LineName {
            get {
                return this.lineNameField;
            }
            set {
                this.lineNameField = value;
                this.RaisePropertyChanged("LineName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.DateTime BeginTime {
            get {
                return this.beginTimeField;
            }
            set {
                this.beginTimeField = value;
                this.RaisePropertyChanged("BeginTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public System.DateTime EndTime {
            get {
                return this.endTimeField;
            }
            set {
                this.endTimeField = value;
                this.RaisePropertyChanged("EndTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public decimal Downtime {
            get {
                return this.downtimeField;
            }
            set {
                this.downtimeField = value;
                this.RaisePropertyChanged("Downtime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string Measure {
            get {
                return this.measureField;
            }
            set {
                this.measureField = value;
                this.RaisePropertyChanged("Measure");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string ExceptionState {
            get {
                return this.exceptionStateField;
            }
            set {
                this.exceptionStateField = value;
                this.RaisePropertyChanged("ExceptionState");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string FailureContent {
            get {
                return this.failureContentField;
            }
            set {
                this.failureContentField = value;
                this.RaisePropertyChanged("FailureContent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string FailureTitle {
            get {
                return this.failureTitleField;
            }
            set {
                this.failureTitleField = value;
                this.RaisePropertyChanged("FailureTitle");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string FailureTitleType {
            get {
                return this.failureTitleTypeField;
            }
            set {
                this.failureTitleTypeField = value;
                this.RaisePropertyChanged("FailureTitleType");
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(D_ToOEE))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public abstract partial class Entity : object, System.ComponentModel.INotifyPropertyChanged {
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceToOEESoapChannel : Solder_Request_WinFrm.ServiceReference1.WebServiceToOEESoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceToOEESoapClient : System.ServiceModel.ClientBase<Solder_Request_WinFrm.ServiceReference1.WebServiceToOEESoap>, Solder_Request_WinFrm.ServiceReference1.WebServiceToOEESoap {
        
        public WebServiceToOEESoapClient() {
        }
        
        public WebServiceToOEESoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceToOEESoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceToOEESoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceToOEESoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public System.Threading.Tasks.Task<string> HelloWorldAsync() {
            return base.Channel.HelloWorldAsync();
        }
        
        public Solder_Request_WinFrm.ServiceReference1.D_ToOEE[] GetListD_ToOEE(string _tempLineId, System.DateTime _tempBeginTime, System.DateTime _tempEndTime, string _tempExceptionState) {
            return base.Channel.GetListD_ToOEE(_tempLineId, _tempBeginTime, _tempEndTime, _tempExceptionState);
        }
        
        public System.Threading.Tasks.Task<Solder_Request_WinFrm.ServiceReference1.D_ToOEE[]> GetListD_ToOEEAsync(string _tempLineId, System.DateTime _tempBeginTime, System.DateTime _tempEndTime, string _tempExceptionState) {
            return base.Channel.GetListD_ToOEEAsync(_tempLineId, _tempBeginTime, _tempEndTime, _tempExceptionState);
        }
    }
}
