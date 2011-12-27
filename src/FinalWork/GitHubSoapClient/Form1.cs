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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
            //    new EndpointAddress("http://localhost:8080/GitHubBroker"));
            //GitHubBroker.IssuesCollectionResp resp = factory.CreateChannel().GetIssues();
            //if (resp != null)
            //{
            //    MessageBox.Show(string.Format("Returned {0} issues", resp.Issues.Count()));
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get Repository Issues
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));
            GitHubBroker.IssuesCollectionResp resp = factory.CreateChannel().GetRepositoryIssues(tRepoName.Text);
            if (resp != null)
            {
                foreach (var issue in resp.Issues)
                {
                    issuesTable.Rows.Add(issue.Title, issue.Body);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateIssueForm f = new CreateIssueForm();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));
            factory.CreateChannel().EditIssue(tRepoName.Text, "1", null);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));
            GitHubBroker.RepositoriesCollectionResp resp = factory.CreateChannel().GetRepositories();

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Get All Issues

            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
    new EndpointAddress("http://localhost:8080/githubbroker"));
            GitHubBroker.IssuesCollectionResp resp = factory.CreateChannel().GetAllIssues();
            if (resp != null)
            {
                foreach (var issue in resp.Issues)
                {
                    issuesTable.Rows.Add(issue.Title, issue.Body);
                }
            }

        }
    }
}
    