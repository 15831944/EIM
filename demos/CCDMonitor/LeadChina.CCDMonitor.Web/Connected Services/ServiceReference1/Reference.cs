﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeadChina.CCDMonitor.Web.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SourceData", Namespace="http://schemas.datacontract.org/2004/07/LeadChina.CCDMonitor.Web.Models")]
    [System.SerializableAttribute()]
    public partial class SourceData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.Stream ImageStreamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LineNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProcessNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceCameraNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkshopNoField;
        
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
        public string CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedDateField, value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeviceNo {
            get {
                return this.DeviceNoField;
            }
            set {
                if ((object.ReferenceEquals(this.DeviceNoField, value) != true)) {
                    this.DeviceNoField = value;
                    this.RaisePropertyChanged("DeviceNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.IO.Stream ImageStream {
            get {
                return this.ImageStreamField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageStreamField, value) != true)) {
                    this.ImageStreamField = value;
                    this.RaisePropertyChanged("ImageStream");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LineNo {
            get {
                return this.LineNoField;
            }
            set {
                if ((object.ReferenceEquals(this.LineNoField, value) != true)) {
                    this.LineNoField = value;
                    this.RaisePropertyChanged("LineNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LocNo {
            get {
                return this.LocNoField;
            }
            set {
                if ((object.ReferenceEquals(this.LocNoField, value) != true)) {
                    this.LocNoField = value;
                    this.RaisePropertyChanged("LocNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProcessNo {
            get {
                return this.ProcessNoField;
            }
            set {
                if ((object.ReferenceEquals(this.ProcessNoField, value) != true)) {
                    this.ProcessNoField = value;
                    this.RaisePropertyChanged("ProcessNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SourceCameraNo {
            get {
                return this.SourceCameraNoField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceCameraNoField, value) != true)) {
                    this.SourceCameraNoField = value;
                    this.RaisePropertyChanged("SourceCameraNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkshopNo {
            get {
                return this.WorkshopNoField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkshopNoField, value) != true)) {
                    this.WorkshopNoField = value;
                    this.RaisePropertyChanged("WorkshopNo");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IMonitorWcfService")]
    public interface IMonitorWcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitorWcfService/SavePhoto", ReplyAction="http://tempuri.org/IMonitorWcfService/SavePhotoResponse")]
        void SavePhoto(LeadChina.CCDMonitor.Web.ServiceReference1.SourceData photo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitorWcfService/SavePhoto", ReplyAction="http://tempuri.org/IMonitorWcfService/SavePhotoResponse")]
        System.Threading.Tasks.Task SavePhotoAsync(LeadChina.CCDMonitor.Web.ServiceReference1.SourceData photo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMonitorWcfServiceChannel : LeadChina.CCDMonitor.Web.ServiceReference1.IMonitorWcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MonitorWcfServiceClient : System.ServiceModel.ClientBase<LeadChina.CCDMonitor.Web.ServiceReference1.IMonitorWcfService>, LeadChina.CCDMonitor.Web.ServiceReference1.IMonitorWcfService {
        
        public MonitorWcfServiceClient() {
        }
        
        public MonitorWcfServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MonitorWcfServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitorWcfServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitorWcfServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SavePhoto(LeadChina.CCDMonitor.Web.ServiceReference1.SourceData photo) {
            base.Channel.SavePhoto(photo);
        }
        
        public System.Threading.Tasks.Task SavePhotoAsync(LeadChina.CCDMonitor.Web.ServiceReference1.SourceData photo) {
            return base.Channel.SavePhotoAsync(photo);
        }
    }
}
