using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatParty
{
    class ChatClient : ChatServiceContract.IClientContract
    {
        public Form1 MainForm { get; set; }

        #region IClientContract Members

        public void NewMessage(string message)
        {
            MainForm.AddMessage(message);
        }

        #endregion
    }
}
