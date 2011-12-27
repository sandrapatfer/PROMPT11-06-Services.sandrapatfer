using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ChatServiceContract
{
    [ServiceContract(CallbackContract = typeof(IClientContract), SessionMode = SessionMode.Required)]
    public interface IServiceContract
    {
        [OperationContract(IsOneWay = true)]
        void RegisterParty(RegisterRequest req);

        [OperationContract(IsOneWay = true)]
        void Message(string message);

        [OperationContract(IsOneWay = true, IsTerminating = true)]
        void ClosePartySession(string partyName);
    }
}
