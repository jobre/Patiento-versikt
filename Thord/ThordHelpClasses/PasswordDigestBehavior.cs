using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace ThordCustomUserNameToken
{
    //public class PasswordDigestBehavior : BehaviorExtensionElement, IServiceBehavior, IEndpointBehavior, IOperationBehavior
    //{
    //    public string Username { get; set; }
    //    public string Password { get; set; }

    //    public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
    //    {
    //        clientRuntime.MessageInspectors.Add(new PasswordDigestMessageInspector(this.Username, this.Password));
    //    }

    //    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Validate(ServiceEndpoint endpoint)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Validate(OperationDescription operationDescription)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    //    {
    //        return;
    //    }

    //    void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
    //    {
    //        clientRuntime.MessageInspectors.Add(new PasswordDigestMessageInspector(this.Username, this.Password));
    //    }

    //    void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
    //    {
    //        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(null);
    //    }

    //     void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
    //    {
            
    //    }

    //    public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Type BehaviorType
    //    {
    //        get { return null; }
    //    }


    //    protected override object CreateBehavior()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
