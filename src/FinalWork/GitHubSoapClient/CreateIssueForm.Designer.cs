namespace GitHubSoapClient
{
    partial class CreateIssueForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tRepository = new System.Windows.Forms.TextBox();
            this.tIssueTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Repository";
            // 
            // tRepository
            // 
            this.tRepository.Location = new System.Drawing.Point(78, 21);
            this.tRepository.Name = "tRepository";
            this.tRepository.Size = new System.Drawing.Size(325, 20);
            this.tRepository.TabIndex = 1;
            this.tRepository.Text = "PROMPT11-06-Services.sandrapatfer";
            // 
            // tIssueTitle
            // 
            this.tIssueTitle.Location = new System.Drawing.Point(78, 56);
            this.tIssueTitle.Name = "tIssueTitle";
            this.tIssueTitle.Size = new System.Drawing.Size(325, 20);
            this.tIssueTitle.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Title";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(328, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CreateIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 163);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tIssueTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tRepository);
            this.Controls.Add(this.label1);
            this.Name = "CreateIssueForm";
            this.Text = "CreateIssueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tRepository;
        private System.Windows.Forms.TextBox tIssueTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}