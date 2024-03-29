﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace page.remote {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://www.cs.rit.edu/~ats/db", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db", ConfigurationName="remote.ServiceSoap")]
    public interface ServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/~ats/db/get_Count", ReplyAction="*")]
        page.remote.get_CountResponse get_Count(page.remote.get_CountRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.cs.rit.edu/~ats/db/get_Count", ReplyAction="*")]
        System.IAsyncResult Beginget_Count(page.remote.get_CountRequest request, System.AsyncCallback callback, object asyncState);
        
        page.remote.get_CountResponse Endget_Count(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/~ats/db/Search", ReplyAction="*")]
        page.remote.SearchResponse Search(page.remote.SearchRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.cs.rit.edu/~ats/db/Search", ReplyAction="*")]
        System.IAsyncResult BeginSearch(page.remote.SearchRequest request, System.AsyncCallback callback, object asyncState);
        
        page.remote.SearchResponse EndSearch(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/~ats/db/Enter", ReplyAction="*")]
        page.remote.EnterResponse Enter(page.remote.EnterRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.cs.rit.edu/~ats/db/Enter", ReplyAction="*")]
        System.IAsyncResult BeginEnter(page.remote.EnterRequest request, System.AsyncCallback callback, object asyncState);
        
        page.remote.EnterResponse EndEnter(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/~ats/db/Remove", ReplyAction="*")]
        page.remote.RemoveResponse Remove(page.remote.RemoveRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.cs.rit.edu/~ats/db/Remove", ReplyAction="*")]
        System.IAsyncResult BeginRemove(page.remote.RemoveRequest request, System.AsyncCallback callback, object asyncState);
        
        page.remote.RemoveResponse EndRemove(System.IAsyncResult result);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="get_Count", WrapperNamespace="http://www.cs.rit.edu/~ats/db", IsWrapped=true)]
    public partial class get_CountRequest {
        
        public get_CountRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="get_CountResponse", WrapperNamespace="http://www.cs.rit.edu/~ats/db", IsWrapped=true)]
    public partial class get_CountResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public int get_CountResult;
        
        public get_CountResponse() {
        }
        
        public get_CountResponse(int get_CountResult) {
            this.get_CountResult = get_CountResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SearchRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Search", Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public page.remote.SearchRequestBody Body;
        
        public SearchRequest() {
        }
        
        public SearchRequest(page.remote.SearchRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db")]
    public partial class SearchRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public page.remote.ArrayOfString keys;
        
        public SearchRequestBody() {
        }
        
        public SearchRequestBody(page.remote.ArrayOfString keys) {
            this.keys = keys;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SearchResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SearchResponse", Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public page.remote.SearchResponseBody Body;
        
        public SearchResponse() {
        }
        
        public SearchResponse(page.remote.SearchResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db")]
    public partial class SearchResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public page.remote.ArrayOfString[] SearchResult;
        
        public SearchResponseBody() {
        }
        
        public SearchResponseBody(page.remote.ArrayOfString[] SearchResult) {
            this.SearchResult = SearchResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EnterRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Enter", Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public page.remote.EnterRequestBody Body;
        
        public EnterRequest() {
        }
        
        public EnterRequest(page.remote.EnterRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db")]
    public partial class EnterRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public page.remote.ArrayOfString tuple;
        
        public EnterRequestBody() {
        }
        
        public EnterRequestBody(page.remote.ArrayOfString tuple) {
            this.tuple = tuple;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EnterResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EnterResponse", Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public page.remote.EnterResponseBody Body;
        
        public EnterResponse() {
        }
        
        public EnterResponse(page.remote.EnterResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db")]
    public partial class EnterResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool EnterResult;
        
        public EnterResponseBody() {
        }
        
        public EnterResponseBody(bool EnterResult) {
            this.EnterResult = EnterResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RemoveRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Remove", Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public page.remote.RemoveRequestBody Body;
        
        public RemoveRequest() {
        }
        
        public RemoveRequest(page.remote.RemoveRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db")]
    public partial class RemoveRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public page.remote.ArrayOfString keys;
        
        public RemoveRequestBody() {
        }
        
        public RemoveRequestBody(page.remote.ArrayOfString keys) {
            this.keys = keys;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RemoveResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RemoveResponse", Namespace="http://www.cs.rit.edu/~ats/db", Order=0)]
        public page.remote.RemoveResponseBody Body;
        
        public RemoveResponse() {
        }
        
        public RemoveResponse(page.remote.RemoveResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.cs.rit.edu/~ats/db")]
    public partial class RemoveResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool RemoveResult;
        
        public RemoveResponseBody() {
        }
        
        public RemoveResponseBody(bool RemoveResult) {
            this.RemoveResult = RemoveResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ServiceSoapChannel : page.remote.ServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class get_CountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public get_CountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public page.remote.get_CountResponse Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((page.remote.get_CountResponse)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class SearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public SearchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public page.remote.SearchResponse Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((page.remote.SearchResponse)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class EnterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public EnterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public page.remote.EnterResponse Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((page.remote.EnterResponse)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class RemoveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public RemoveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public page.remote.RemoveResponse Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((page.remote.RemoveResponse)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<page.remote.ServiceSoap>, page.remote.ServiceSoap {
        
        private BeginOperationDelegate onBeginget_CountDelegate;
        
        private EndOperationDelegate onEndget_CountDelegate;
        
        private System.Threading.SendOrPostCallback onget_CountCompletedDelegate;
        
        private BeginOperationDelegate onBeginSearchDelegate;
        
        private EndOperationDelegate onEndSearchDelegate;
        
        private System.Threading.SendOrPostCallback onSearchCompletedDelegate;
        
        private BeginOperationDelegate onBeginEnterDelegate;
        
        private EndOperationDelegate onEndEnterDelegate;
        
        private System.Threading.SendOrPostCallback onEnterCompletedDelegate;
        
        private BeginOperationDelegate onBeginRemoveDelegate;
        
        private EndOperationDelegate onEndRemoveDelegate;
        
        private System.Threading.SendOrPostCallback onRemoveCompletedDelegate;
        
        public ServiceSoapClient() {
        }
        
        public ServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<get_CountCompletedEventArgs> get_CountCompleted;
        
        public event System.EventHandler<SearchCompletedEventArgs> SearchCompleted;
        
        public event System.EventHandler<EnterCompletedEventArgs> EnterCompleted;
        
        public event System.EventHandler<RemoveCompletedEventArgs> RemoveCompleted;
        
        public page.remote.get_CountResponse get_Count(page.remote.get_CountRequest request) {
            return base.Channel.get_Count(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginget_Count(page.remote.get_CountRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginget_Count(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public page.remote.get_CountResponse Endget_Count(System.IAsyncResult result) {
            return base.Channel.Endget_Count(result);
        }
        
        private System.IAsyncResult OnBeginget_Count(object[] inValues, System.AsyncCallback callback, object asyncState) {
            page.remote.get_CountRequest request = ((page.remote.get_CountRequest)(inValues[0]));
            return this.Beginget_Count(request, callback, asyncState);
        }
        
        private object[] OnEndget_Count(System.IAsyncResult result) {
            page.remote.get_CountResponse retVal = this.Endget_Count(result);
            return new object[] {
                    retVal};
        }
        
        private void Onget_CountCompleted(object state) {
            if ((this.get_CountCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.get_CountCompleted(this, new get_CountCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void get_CountAsync(page.remote.get_CountRequest request) {
            this.get_CountAsync(request, null);
        }
        
        public void get_CountAsync(page.remote.get_CountRequest request, object userState) {
            if ((this.onBeginget_CountDelegate == null)) {
                this.onBeginget_CountDelegate = new BeginOperationDelegate(this.OnBeginget_Count);
            }
            if ((this.onEndget_CountDelegate == null)) {
                this.onEndget_CountDelegate = new EndOperationDelegate(this.OnEndget_Count);
            }
            if ((this.onget_CountCompletedDelegate == null)) {
                this.onget_CountCompletedDelegate = new System.Threading.SendOrPostCallback(this.Onget_CountCompleted);
            }
            base.InvokeAsync(this.onBeginget_CountDelegate, new object[] {
                        request}, this.onEndget_CountDelegate, this.onget_CountCompletedDelegate, userState);
        }
        
        public page.remote.SearchResponse Search(page.remote.SearchRequest request) {
            return base.Channel.Search(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSearch(page.remote.SearchRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSearch(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public page.remote.SearchResponse EndSearch(System.IAsyncResult result) {
            return base.Channel.EndSearch(result);
        }
        
        private System.IAsyncResult OnBeginSearch(object[] inValues, System.AsyncCallback callback, object asyncState) {
            page.remote.SearchRequest request = ((page.remote.SearchRequest)(inValues[0]));
            return this.BeginSearch(request, callback, asyncState);
        }
        
        private object[] OnEndSearch(System.IAsyncResult result) {
            page.remote.SearchResponse retVal = this.EndSearch(result);
            return new object[] {
                    retVal};
        }
        
        private void OnSearchCompleted(object state) {
            if ((this.SearchCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SearchCompleted(this, new SearchCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SearchAsync(page.remote.SearchRequest request) {
            this.SearchAsync(request, null);
        }
        
        public void SearchAsync(page.remote.SearchRequest request, object userState) {
            if ((this.onBeginSearchDelegate == null)) {
                this.onBeginSearchDelegate = new BeginOperationDelegate(this.OnBeginSearch);
            }
            if ((this.onEndSearchDelegate == null)) {
                this.onEndSearchDelegate = new EndOperationDelegate(this.OnEndSearch);
            }
            if ((this.onSearchCompletedDelegate == null)) {
                this.onSearchCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSearchCompleted);
            }
            base.InvokeAsync(this.onBeginSearchDelegate, new object[] {
                        request}, this.onEndSearchDelegate, this.onSearchCompletedDelegate, userState);
        }
        
        public page.remote.EnterResponse Enter(page.remote.EnterRequest request) {
            return base.Channel.Enter(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginEnter(page.remote.EnterRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginEnter(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public page.remote.EnterResponse EndEnter(System.IAsyncResult result) {
            return base.Channel.EndEnter(result);
        }
        
        private System.IAsyncResult OnBeginEnter(object[] inValues, System.AsyncCallback callback, object asyncState) {
            page.remote.EnterRequest request = ((page.remote.EnterRequest)(inValues[0]));
            return this.BeginEnter(request, callback, asyncState);
        }
        
        private object[] OnEndEnter(System.IAsyncResult result) {
            page.remote.EnterResponse retVal = this.EndEnter(result);
            return new object[] {
                    retVal};
        }
        
        private void OnEnterCompleted(object state) {
            if ((this.EnterCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.EnterCompleted(this, new EnterCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void EnterAsync(page.remote.EnterRequest request) {
            this.EnterAsync(request, null);
        }
        
        public void EnterAsync(page.remote.EnterRequest request, object userState) {
            if ((this.onBeginEnterDelegate == null)) {
                this.onBeginEnterDelegate = new BeginOperationDelegate(this.OnBeginEnter);
            }
            if ((this.onEndEnterDelegate == null)) {
                this.onEndEnterDelegate = new EndOperationDelegate(this.OnEndEnter);
            }
            if ((this.onEnterCompletedDelegate == null)) {
                this.onEnterCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnEnterCompleted);
            }
            base.InvokeAsync(this.onBeginEnterDelegate, new object[] {
                        request}, this.onEndEnterDelegate, this.onEnterCompletedDelegate, userState);
        }
        
        public page.remote.RemoveResponse Remove(page.remote.RemoveRequest request) {
            return base.Channel.Remove(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginRemove(page.remote.RemoveRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRemove(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public page.remote.RemoveResponse EndRemove(System.IAsyncResult result) {
            return base.Channel.EndRemove(result);
        }
        
        private System.IAsyncResult OnBeginRemove(object[] inValues, System.AsyncCallback callback, object asyncState) {
            page.remote.RemoveRequest request = ((page.remote.RemoveRequest)(inValues[0]));
            return this.BeginRemove(request, callback, asyncState);
        }
        
        private object[] OnEndRemove(System.IAsyncResult result) {
            page.remote.RemoveResponse retVal = this.EndRemove(result);
            return new object[] {
                    retVal};
        }
        
        private void OnRemoveCompleted(object state) {
            if ((this.RemoveCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.RemoveCompleted(this, new RemoveCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void RemoveAsync(page.remote.RemoveRequest request) {
            this.RemoveAsync(request, null);
        }
        
        public void RemoveAsync(page.remote.RemoveRequest request, object userState) {
            if ((this.onBeginRemoveDelegate == null)) {
                this.onBeginRemoveDelegate = new BeginOperationDelegate(this.OnBeginRemove);
            }
            if ((this.onEndRemoveDelegate == null)) {
                this.onEndRemoveDelegate = new EndOperationDelegate(this.OnEndRemove);
            }
            if ((this.onRemoveCompletedDelegate == null)) {
                this.onRemoveCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnRemoveCompleted);
            }
            base.InvokeAsync(this.onBeginRemoveDelegate, new object[] {
                        request}, this.onEndRemoveDelegate, this.onRemoveCompletedDelegate, userState);
        }
    }
}
