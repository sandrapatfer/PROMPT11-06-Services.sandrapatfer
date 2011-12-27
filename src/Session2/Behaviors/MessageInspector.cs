using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Behaviors
{
    class ServerMessageInspector : IDispatchMessageInspector
    {
        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            if (request.Headers.FindHeader("id", "") >= 0)
            {
                Console.WriteLine("nice request");
            }
            else
            {
                Console.WriteLine("bad request");
            }
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }

        #endregion
    }
}
