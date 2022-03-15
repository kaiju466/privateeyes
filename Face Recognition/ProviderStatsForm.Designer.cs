namespace Face_Recognition
{
    partial class ProviderStatsForm
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
            this.scheduleTextBox = new System.Windows.Forms.TextBox();
            this.scheduleLabel = new System.Windows.Forms.Label();
            this.securityLevelTextBox = new System.Windows.Forms.TextBox();
            this.securityLevelLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.scheduleListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // scheduleTextBox
            // 
            this.scheduleTextBox.Location = new System.Drawing.Point(110, 221);
            this.scheduleTextBox.Name = "scheduleTextBox";
            this.scheduleTextBox.ReadOnly = true;
            this.scheduleTextBox.Size = new System.Drawing.Size(245, 22);
            this.scheduleTextBox.TabIndex = 19;
            this.scheduleTextBox.Visible = false;
            // 
            // scheduleLabel
            // 
            this.scheduleLabel.AutoSize = true;
            this.scheduleLabel.Location = new System.Drawing.Point(12, 121);
            this.scheduleLabel.Name = "scheduleLabel";
            this.scheduleLabel.Size = new System.Drawing.Size(71, 17);
            this.scheduleLabel.TabIndex = 18;
            this.scheduleLabel.Text = "Schedule:";
            // 
            // securityLevelTextBox
            // 
            this.securityLevelTextBox.Location = new System.Drawing.Point(137, 90);
            this.securityLevelTextBox.Name = "securityLevelTextBox";
            this.securityLevelTextBox.ReadOnly = true;
            this.securityLevelTextBox.Size = new System.Drawing.Size(245, 22);
            this.securityLevelTextBox.TabIndex = 17;
            // 
            // securityLevelLabel
            // 
            this.securityLevelLabel.AutoSize = true;
            this.securityLevelLabel.Location = new System.Drawing.Point(12, 93);
            this.securityLevelLabel.Name = "securityLevelLabel";
            this.securityLevelLabel.Size = new System.Drawing.Size(101, 17);
            this.securityLevelLabel.TabIndex = 16;
            this.securityLevelLabel.Text = "Security Level:";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(12, 65);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(39, 17);
            this.titleLabel.TabIndex = 15;
            this.titleLabel.Text = "Title:";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(137, 62);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.ReadOnly = true;
            this.titleTextBox.Size = new System.Drawing.Size(245, 22);
            this.titleTextBox.TabIndex = 14;
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(137, 34);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.ReadOnly = true;
            this.lastNameTextBox.Size = new System.Drawing.Size(245, 22);
            this.lastNameTextBox.TabIndex = 13;
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(137, 6);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.ReadOnly = true;
            this.firstNameTextBox.Size = new System.Drawing.Size(245, 22);
            this.firstNameTextBox.TabIndex = 12;
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(12, 37);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(80, 17);
            this.lastNameLabel.TabIndex = 11;
            this.lastNameLabel.Text = "Last Name:";
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(12, 9);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(80, 17);
            this.firstNameLabel.TabIndex = 10;
            this.firstNameLabel.Text = "First Name:";
            // 
            // scheduleListBox
            // 
            this.scheduleListBox.FormattingEnabled = true;
            this.scheduleListBox.ItemHeight = 16;
            this.scheduleListBox.Location = new System.Drawing.Point(137, 121);
            this.scheduleListBox.Name = "scheduleListBox";
            this.scheduleListBox.Size = new System.Drawing.Size(245, 116);
            this.scheduleListBox.TabIndex = 20;
            this.scheduleListBox.SelectedIndexChanged += new System.EventHandler(this.scheduleListBox_SelectedIndexChanged);
            // 
            // ProviderStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 255);
            this.ControlBox = false;
            this.Controls.Add(this.scheduleListBox);
            this.Controls.Add(this.scheduleTextBox);
            this.Controls.Add(this.scheduleLabel);
            this.Controls.Add(this.securityLevelTextBox);
            this.Controls.Add(this.securityLevelLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameLabel);
            this.Name = "ProviderStatsForm";
            this.ShowInTaskbar = false;
            this.Text = "Provider Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox scheduleTextBox;
        private System.Windows.Forms.Label scheduleLabel;
        private System.Windows.Forms.TextBox securityLevelTextBox;
        private System.Windows.Forms.Label securityLevelLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.ListBox scheduleListBox;
    }
}