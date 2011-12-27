using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace GitHubSoapClient
{
    public partial class CreateIssueForm : Form
    {
        public CreateIssueForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/githubbroker"));
            factory.CreateChannel().CreateIssue(tRepository.Text, null);
            Close();
        }
    }
}
