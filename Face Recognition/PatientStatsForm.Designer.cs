namespace Face_Recognition
{
    partial class PatientStatsForm
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
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.aptDateTextBox = new System.Windows.Forms.TextBox();
            this.providerNameTextBox = new System.Windows.Forms.TextBox();
            this.providerNameLabel = new System.Windows.Forms.Label();
            this.aptDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.aptDescriptionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(12, 9);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(80, 17);
            this.firstNameLabel.TabIndex = 0;
            this.firstNameLabel.Text = "First Name:";
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(12, 37);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(80, 17);
            this.lastNameLabel.TabIndex = 1;
            this.lastNameLabel.Text = "Last Name:";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(137, 6);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.ReadOnly = true;
            this.firstNameTextBox.Size = new System.Drawing.Size(197, 22);
            this.firstNameTextBox.TabIndex = 2;
            this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(137, 34);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.ReadOnly = true;
            this.lastNameTextBox.Size = new System.Drawing.Size(197, 22);
            this.lastNameTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Apt. Date/Time:";
            // 
            // aptDateTextBox
            // 
            this.aptDateTextBox.Location = new System.Drawing.Point(137, 62);
            this.aptDateTextBox.Name = "aptDateTextBox";
            this.aptDateTextBox.ReadOnly = true;
            this.aptDateTextBox.Size = new System.Drawing.Size(197, 22);
            this.aptDateTextBox.TabIndex = 9;
            // 
            // providerNameTextBox
            // 
            this.providerNameTextBox.Location = new System.Drawing.Point(137, 90);
            this.providerNameTextBox.Name = "providerNameTextBox";
            this.providerNameTextBox.ReadOnly = true;
            this.providerNameTextBox.Size = new System.Drawing.Size(197, 22);
            this.providerNameTextBox.TabIndex = 10;
            // 
            // providerNameLabel
            // 
            this.providerNameLabel.AutoSize = true;
            this.providerNameLabel.Location = new System.Drawing.Point(12, 95);
            this.providerNameLabel.Name = "providerNameLabel";
            this.providerNameLabel.Size = new System.Drawing.Size(72, 17);
            this.providerNameLabel.TabIndex = 11;
            this.providerNameLabel.Text = "Dr. Name:";
            // 
            // aptDescriptionTextBox
            // 
            this.aptDescriptionTextBox.Location = new System.Drawing.Point(137, 118);
            this.aptDescriptionTextBox.Name = "aptDescriptionTextBox";
            this.aptDescriptionTextBox.ReadOnly = true;
            this.aptDescriptionTextBox.Size = new System.Drawing.Size(197, 22);
            this.aptDescriptionTextBox.TabIndex = 12;
            // 
            // aptDescriptionLabel
            // 
            this.aptDescriptionLabel.AutoSize = true;
            this.aptDescriptionLabel.Location = new System.Drawing.Point(12, 121);
            this.aptDescriptionLabel.Name = "aptDescriptionLabel";
            this.aptDescriptionLabel.Size = new System.Drawing.Size(112, 17);
            this.aptDescriptionLabel.TabIndex = 13;
            this.aptDescriptionLabel.Text = "Apt. Description:";
            // 
            // PatientStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 255);
            this.ControlBox = false;
            this.Controls.Add(this.aptDescriptionLabel);
            this.Controls.Add(this.aptDescriptionTextBox);
            this.Controls.Add(this.providerNameLabel);
            this.Controls.Add(this.providerNameTextBox);
            this.Controls.Add(this.aptDateTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameLabel);
            this.Name = "PatientStatsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Patient Info";
            this.Load += new System.EventHandler(this.PatientStatsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox aptDateTextBox;
        private System.Windows.Forms.TextBox providerNameTextBox;
        private System.Windows.Forms.Label providerNameLabel;
        private System.Windows.Forms.TextBox aptDescriptionTextBox;
        private System.Windows.Forms.Label aptDescriptionLabel;

    }
}