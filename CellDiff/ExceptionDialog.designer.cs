namespace Alissa.GUI
{
    partial class ExceptionDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.detailButton = new System.Windows.Forms.Button();
            this.detailTextBox = new System.Windows.Forms.TextBox();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(377, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // detailButton
            // 
            this.detailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detailButton.Location = new System.Drawing.Point(377, 41);
            this.detailButton.Name = "detailButton";
            this.detailButton.Size = new System.Drawing.Size(75, 23);
            this.detailButton.TabIndex = 1;
            this.detailButton.Text = "Details >>";
            this.detailButton.UseVisualStyleBackColor = true;
            this.detailButton.Click += new System.EventHandler(this.detailButton_Click);
            // 
            // detailTextBox
            // 
            this.detailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.detailTextBox.Location = new System.Drawing.Point(12, 85);
            this.detailTextBox.Multiline = true;
            this.detailTextBox.Name = "detailTextBox";
            this.detailTextBox.ReadOnly = true;
            this.detailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.detailTextBox.Size = new System.Drawing.Size(440, 65);
            this.detailTextBox.TabIndex = 2;
            this.detailTextBox.Text = "Exception and stack trace.";
            this.detailTextBox.WordWrap = false;
            // 
            // messageTextBox
            // 
            this.messageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageTextBox.Location = new System.Drawing.Point(12, 12);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(359, 52);
            this.messageTextBox.TabIndex = 3;
            this.messageTextBox.Text = "Human readable message.";
            // 
            // ExceptionDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(464, 162);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.detailTextBox);
            this.Controls.Add(this.detailButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 114);
            this.Name = "ExceptionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exception";
            this.Load += new System.EventHandler(this.ExceptionDialog_Load);
            this.SizeChanged += new System.EventHandler(this.ExceptionDialog_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button detailButton;
        private System.Windows.Forms.TextBox detailTextBox;
        private System.Windows.Forms.TextBox messageTextBox;
    }
}