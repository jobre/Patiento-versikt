using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using Microsoft.Web.Services3.Security.Tokens;

namespace Microsoft.WCF.Documentation
{
    public class InspectorInserter : BehaviorExtensionElement, IServiceBehavior, IEndpointBehavior, IOperationBehavior
    {
        #region IServiceBehavior Members
        public void AddBindingParameters(
          ServiceDescription serviceDescription,
          ServiceHostBase serviceHostBase,
          System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
          BindingParameterCollection bindingParameters
        )
        { return; }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chDisp in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher epDisp in chDisp.Endpoints)
                {
                    epDisp.DispatchRuntime.MessageInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
                    foreach (DispatchOperation op in epDisp.DispatchRuntime.Operations)
                        op.ParameterInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { return; }

        #endregion
        #region IEndpointBehavior Members
        public void AddBindingParameters(
          ServiceEndpoint endpoint, BindingParameterCollection bindingParameters
        ) { return; }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
            foreach (ClientOperation op in clientRuntime.Operations)
                op.ParameterInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
            foreach (DispatchOperation op in endpointDispatcher.DispatchRuntime.Operations)
                op.ParameterInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
        }

        public void Validate(ServiceEndpoint endpoint) { return; }
        #endregion
        #region IOperationBehavior Members
        public void AddBindingParameters(
          OperationDescription operationDescription, BindingParameterCollection bindingParameters
        )
        { return; }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new PasswordDigestMessageInspector("test", "test"));
        }

        public void Validate(OperationDescription operationDescription) { return; }

        #endregion

        public override Type BehaviorType
        {
            get { return typeof(InspectorInserter); }
        }

        protected override object CreateBehavior()
        { return new InspectorInserter(); }
    }

    public class PasswordDigestMessageInspector : IClientMessageInspector, IParameterInspector, IDispatchMessageInspector
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public PasswordDigestMessageInspector(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            try
            {
                MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
                reply = buffer.CreateMessage();
                System.IO.File.WriteAllText(@"c:\temp\testthord\response-" + DateTime.Now.ToString("MMddyyyyhhmmssfff") + ".xml", reply.ToString());
            }
            catch (Exception e)
            {
                Log4Net.Logger.loggError(e, "Error in receiving response from Thord", "", "AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)"); 
            }
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            // Use the WSE 3.0 security token class
            UsernameToken token = new UsernameToken(this.Username, this.Password, PasswordOption.SendPlainText);

            // Serialize the token to XML
            XmlElement securityToken = token.GetXml(new XmlDocument());

            //
            MessageHeader securityHeader = MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", securityToken, false);
            request.Headers.Add(securityHeader);

            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();
            try
            {
                System.IO.File.WriteAllText(@"c:\temp\testthord\" + DateTime.Now.ToString("MMddyyyyhhmmssfff") + ".xml", request.ToString());
            }
            catch { }

            // complete
            return Convert.DBNull;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            Console.WriteLine("Test");
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            Console.WriteLine("Test");
            return null;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            throw new NotImplementedException();
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            throw new NotImplementedException();
        }
    }
}
