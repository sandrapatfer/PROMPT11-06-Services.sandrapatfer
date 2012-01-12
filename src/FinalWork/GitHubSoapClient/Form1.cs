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
        private string _selectedRepoId;
        private string _selectedIssueId;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get Repository Issues
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));
            GitHubBroker.IssuesCollectionResp resp = factory.CreateChannel().GetRepositoryIssues(tRepositoryName.Text);
            issuesTable.Rows.Clear();
            if (resp != null)
            {
                foreach (var issue in resp.Issues)
                {
                    var idx = issuesTable.Rows.Add(issue.Title, issue.Body);
                    issuesTable.Rows[idx].Tag = Convert.ToString(issue.Number);
                }
                if (issuesTable.SelectedRows.Count == 1)
                {
                    _selectedIssueId = issuesTable.SelectedRows[0].Tag as string;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create Issue
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));
            var resp = factory.CreateChannel().CreateIssue(tRepositoryName.Text,
                new GitHubBroker.IssueData() { Title = tIssueTitle.Text, Body = tIssueDescr.Text });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Edit Issue
            if (string.IsNullOrEmpty(_selectedIssueId))
            {
                MessageBox.Show("Select an issue first!");
                return;
            }
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));

            var newIssue = new GitHubBroker.IssueData()
            {
                Title = tIssueTitle.Text,
                Body = tIssueDescr.Text
            };
            var resp = factory.CreateChannel().EditIssue(tRepositoryName.Text, _selectedIssueId, newIssue);
            CheckResponse(resp);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Get All Repositories
            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));
            GitHubBroker.RepositoriesCollectionResp resp = factory.CreateChannel().GetRepositories();
            dataGridView1.Rows.Clear();
            if (resp != null)
            {
                foreach (var repo in resp.Repositories)
                {
                    var idx = dataGridView1.Rows.Add(repo.Name, repo.Description);
                    dataGridView1.Rows[idx].Tag = repo.Name;
                }
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    _selectedRepoId = dataGridView1.SelectedRows[0].Tag as string;
                }
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            // Create Repository

            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
    new EndpointAddress("http://localhost:8080/githubbroker"));

            var newRepo = new GitHubBroker.RepositoryData()
            {
                Name = tRepositoryName.Text,
                Description = tRepositoryDescr.Text
            };
            var resp = factory.CreateChannel().CreateRepository(newRepo);
            CheckResponse(resp);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Edit Repository
            if (string.IsNullOrEmpty(_selectedRepoId))
            {
                MessageBox.Show("Select a repository first!");
                return;
            }

            var factory = new ChannelFactory<GitHubBroker.IGitHubSoapBrokerChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/githubbroker"));

            var newRepo = new GitHubBroker.RepositoryData()
            {
                Name = tRepositoryName.Text,
                Description = tRepositoryDescr.Text
            };
            var resp = factory.CreateChannel().EditRepository(_selectedRepoId, newRepo);
            CheckResponse(resp);
        }

        private void tRepoName_TextChanged(object sender, EventArgs e)
        {

        }



        private void CheckResponse(GitHubBroker.StatusResp resp)
        {
            if (resp.Code != GitHubBroker.StatusCode.Ok)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Clear repo fields
            _selectedRepoId = "";
            tRepositoryName.Text = "";
            tRepositoryDescr.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // repository table selection changed
            if (dataGridView1.SelectedRows.Count == 1)
            {
                _selectedRepoId = dataGridView1.SelectedRows[0].Tag as string;
                tRepositoryName.Text = dataGridView1.SelectedRows[0].Cells[0].Value as string;
                tRepositoryDescr.Text = dataGridView1.SelectedRows[0].Cells[1].Value as string;
            }
        }

        private void issuesTable_SelectionChanged(object sender, EventArgs e)
        {
            // issues table selection changed
            if (issuesTable.SelectedRows.Count == 1)
            {
                _selectedIssueId = issuesTable.SelectedRows[0].Tag as string;
                tIssueTitle.Text = issuesTable.SelectedRows[0].Cells[0].Value as string;
                tIssueDescr.Text = issuesTable.SelectedRows[0].Cells[1].Value as string;
            }
        }
    }
}
    