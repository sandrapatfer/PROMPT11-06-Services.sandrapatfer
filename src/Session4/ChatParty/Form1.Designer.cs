namespace ChatParty
{
    partial class Form1
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
            this.messages = new System.Windows.Forms.ListBox();
            this.tMessage = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.tPartyName = new System.Windows.Forms.TextBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.Location = new System.Drawing.Point(14, 47);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(502, 303);
            this.messages.TabIndex = 0;
            // 
            // tMessage
            // 
            this.tMessage.Location = new System.Drawing.Point(14, 369);
            this.tMessage.Name = "tMessage";
            this.tMessage.Size = new System.Drawing.Size(432, 20);
            this.tMessage.TabIndex = 1;
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(458, 367);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(58, 23);
            this.bSend.TabIndex = 2;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tPartyName
            // 
            this.tPartyName.Location = new System.Drawing.Point(14, 12);
            this.tPartyName.Name = "tPartyName";
            this.tPartyName.Size = new System.Drawing.Size(432, 20);
            this.tPartyName.TabIndex = 3;
            this.tPartyName.Text = "nick name";
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(458, 12);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(58, 23);
            this.bConnect.TabIndex = 4;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 404);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.tPartyName);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.tMessage);
            this.Controls.Add(this.messages);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.TextBox tMessage;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.TextBox tPartyName;
        private System.Windows.Forms.Button bConnect;
    }
}

