using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace ChatParty
{
    public partial class Form1 : Form
    {
        private ChatServiceContract.IServiceContract _channel = null;
        private ChatClient _client = null;

        public Form1()
        {
            InitializeComponent();
            _client = new ChatClient() { MainForm = this };
            bSend.Enabled = false;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            if (_channel == null)
            {
                var fact = new DuplexChannelFactory<ChatServiceContract.IServiceContract>(
                    _client, new NetTcpBinding(SecurityMode.None), @"net.tcp://localhost:8080");
                _channel = fact.CreateChannel();
                _channel.RegisterParty(new ChatServiceContract.RegisterRequest() { PartyName = tPartyName.Text });
                bConnect.Text = "Disconnect";
                tPartyName.Enabled = false;
                bSend.Enabled = true;
            }
            else
            {
                _channel.ClosePartySession(tPartyName.Text);
                bConnect.Text = "Connect";
                tPartyName.Enabled = true;
                bSend.Enabled = false;
            }
        }

        internal void AddMessage(string message)
        {
            messages.Items.Add(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bSend_Click(object sender, EventArgs e)
        {
            _channel.Message(tMessage.Text);
        }
    }
}
