﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _6_DistributedWinner_Client.SelectingAWinnerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.cs.rit.edu/axel/conversions/", ConfigurationName="SelectingAWinnerService.PlayerCellServiceSoap")]
    public interface PlayerCellServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/axel/conversions/Login", ReplyAction="*")]
        int Login();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/axel/conversions/Get", ReplyAction="*")]
        int Get(int playerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/axel/conversions/Set", ReplyAction="*")]
        void Set(int playerId, int selection);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cs.rit.edu/axel/conversions/IsFirst", ReplyAction="*")]
        bool IsFirst(int playerId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface PlayerCellServiceSoapChannel : _6_DistributedWinner_Client.SelectingAWinnerService.PlayerCellServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class PlayerCellServiceSoapClient : System.ServiceModel.ClientBase<_6_DistributedWinner_Client.SelectingAWinnerService.PlayerCellServiceSoap>, _6_DistributedWinner_Client.SelectingAWinnerService.PlayerCellServiceSoap {
        
        public PlayerCellServiceSoapClient() {
        }
        
        public PlayerCellServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PlayerCellServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlayerCellServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlayerCellServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Login() {
            return base.Channel.Login();
        }
        
        public int Get(int playerId) {
            return base.Channel.Get(playerId);
        }
        
        public void Set(int playerId, int selection) {
            base.Channel.Set(playerId, selection);
        }
        
        public bool IsFirst(int playerId) {
            return base.Channel.IsFirst(playerId);
        }
    }
}
