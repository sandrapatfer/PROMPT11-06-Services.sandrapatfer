using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace GitHubSoapBroker.Behaviors
{
    class ConnectionCheckOperationBehavior : IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            // checking if the connection is ok
            if (!GitHubConnection.IsConnected)
            {
                throw new FaultException<ServiceUnavailableFault>(
                    new ServiceUnavailableFault
                    {
                        Reason = "Connection to Git Hub is down",
                        RetryPeriod = DateTime.Now.AddMinutes(5)
                    }
                );
            }
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}
