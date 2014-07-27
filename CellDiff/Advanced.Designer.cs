namespace CellDiff
{
    partial class Advanced
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Advanced));
            this.label1 = new System.Windows.Forms.Label();
            this.sources = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.targets = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.destinations = new System.Windows.Forms.TextBox();
            this.separateDestination = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sourceBold = new System.Windows.Forms.CheckBox();
            this.sourceStrikeout = new System.Windows.Forms.CheckBox();
            this.sourceUnderline = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.targetBold = new System.Windows.Forms.CheckBox();
            this.targetStrikeout = new System.Windows.Forms.CheckBox();
            this.targetUnderline = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.sourceColor = new System.Windows.Forms.TextBox();
            this.targetColor = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // sources
            // 
            resources.ApplyResources(this.sources, "sources");
            this.sources.Name = "sources";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // targets
            // 
            resources.ApplyResources(this.targets, "targets");
            this.targets.Name = "targets";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sources);
            this.groupBox1.Controls.Add(this.targets);
            this.groupBox1.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.destinations);
            this.groupBox2.Controls.Add(this.separateDestination);
            this.groupBox2.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // destinations
            // 
            resources.ApplyResources(this.destinations, "destinations");
            this.destinations.Name = "destinations";
            // 
            // separateDestination
            // 
            resources.ApplyResources(this.separateDestination, "separateDestination");
            this.separateDestination.Name = "separateDestination";
            this.separateDestination.UseVisualStyleBackColor = true;
            this.separateDestination.CheckedChanged += new System.EventHandler(this.separateDestination_CheckedChanged);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sourceColor);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.sourceBold);
            this.groupBox3.Controls.Add(this.sourceStrikeout);
            this.groupBox3.Controls.Add(this.sourceUnderline);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // sourceBold
            // 
            resources.ApplyResources(this.sourceBold, "sourceBold");
            this.sourceBold.Name = "sourceBold";
            this.sourceBold.UseVisualStyleBackColor = true;
            // 
            // sourceStrikeout
            // 
            resources.ApplyResources(this.sourceStrikeout, "sourceStrikeout");
            this.sourceStrikeout.Name = "sourceStrikeout";
            this.sourceStrikeout.UseVisualStyleBackColor = true;
            // 
            // sourceUnderline
            // 
            resources.ApplyResources(this.sourceUnderline, "sourceUnderline");
            this.sourceUnderline.Name = "sourceUnderline";
            this.sourceUnderline.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.targetColor);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.targetBold);
            this.groupBox4.Controls.Add(this.targetStrikeout);
            this.groupBox4.Controls.Add(this.targetUnderline);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // targetBold
            // 
            resources.ApplyResources(this.targetBold, "targetBold");
            this.targetBold.Name = "targetBold";
            this.targetBold.UseVisualStyleBackColor = true;
            // 
            // targetStrikeout
            // 
            resources.ApplyResources(this.targetStrikeout, "targetStrikeout");
            this.targetStrikeout.Name = "targetStrikeout";
            this.targetStrikeout.UseVisualStyleBackColor = true;
            // 
            // targetUnderline
            // 
            resources.ApplyResources(this.targetUnderline, "targetUnderline");
            this.targetUnderline.Name = "targetUnderline";
            this.targetUnderline.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.ok, "ok");
            this.ok.Name = "ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel, "cancel");
            this.cancel.Name = "cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // sourceColor
            // 
            resources.ApplyResources(this.sourceColor, "sourceColor");
            this.sourceColor.Name = "sourceColor";
            // 
            // targetColor
            // 
            resources.ApplyResources(this.targetColor, "targetColor");
            this.targetColor.Name = "targetColor";
            // 
            // Advanced
            // 
            this.AcceptButton = this.ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Advanced";
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.Advanced_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sources;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox targets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox destinations;
        private System.Windows.Forms.RadioButton separateDestination;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox sourceBold;
        private System.Windows.Forms.CheckBox sourceStrikeout;
        private System.Windows.Forms.CheckBox sourceUnderline;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox targetBold;
        private System.Windows.Forms.CheckBox targetStrikeout;
        private System.Windows.Forms.CheckBox targetUnderline;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TextBox sourceColor;
        private System.Windows.Forms.TextBox targetColor;
    }
}