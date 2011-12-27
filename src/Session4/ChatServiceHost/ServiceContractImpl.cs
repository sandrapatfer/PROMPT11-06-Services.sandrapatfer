using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ChatServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    class ServiceContractImpl : ChatServiceContract.IServiceContract
    {
        private Dictionary<string, ChatServiceContract.IClientContract> _parties = new Dictionary<string, ChatServiceContract.IClientContract>();

        #region IServiceContract Members

        public void RegisterParty(ChatServiceContract.RegisterRequest req)
        {
            Console.WriteLine("New Party: {0}", req.PartyName);
            NotifyAllParties(string.Format("New party: {0} is in house", req.PartyName));
            var cli = OperationContext.Current.GetCallbackChannel<ChatServiceContract.IClientContract>();
            cli.NewMessage(string.Format("Hello {0}, welcome to the party!", req.PartyName));
            _parties.Add(req.PartyName, cli);
        }

        public void Message(string message)
        {
            NotifyAllParties(message);
        }

        public void ClosePartySession(string partyName)
        {
        }

        #endregion

        void NotifyAllParties(string message)
        {
            foreach (var cli in _parties.Values)
            {
                cli.NewMessage(message);
            }
        }
    }
}
