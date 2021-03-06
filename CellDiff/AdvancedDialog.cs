﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CellDiff
{
    /// <summary>
    /// Dialog box to receive advanced options.
    /// </summary>
    [ComVisible(false)]
    public partial class AdvancedDialog : Form
    {
        [ComVisible(false)]
        public struct OptionValues
        {
            public string Sources;
            public string Targets;
            public bool SeparateDestinateions;
            public string Destinations;
            public Decoration SourceDecoration;
            public Decoration TargetDecoration;
        }

        public OptionValues Options;

        [ComVisible(false)]
        public class ValidateOptionsEventArgs : EventArgs
        {
            public OptionValues Options;
            public bool Invalid;
        }

        public delegate void ValidateOptionsEvent(object sender, ValidateOptionsEventArgs e);

        public event ValidateOptionsEvent ValidateOptions;

        public AdvancedDialog()
        {
            InitializeComponent();
        }

        private void separateDestination_CheckedChanged(object sender, EventArgs e)
        {
            destinations.Enabled = separateDestination.Checked;
        }

        private void Advanced_Shown(object sender, EventArgs e)
        {
            sources.Text = Options.Sources;
            targets.Text = Options.Targets;
            separateDestination.Checked = Options.SeparateDestinateions;
            destinations.Text = Options.Destinations;
            sourceUnderline.Checked = Options.SourceDecoration.Underline;
            sourceStrikeout.Checked = Options.SourceDecoration.Strikeout;
            sourceBold.Checked = Options.SourceDecoration.Bold;
            sourceColorBox.BackColor = Options.SourceDecoration.Color;
            targetUnderline.Checked = Options.TargetDecoration.Underline;
            targetStrikeout.Checked = Options.TargetDecoration.Strikeout;
            targetBold.Checked = Options.TargetDecoration.Bold;
            targetColorBox.BackColor = Options.TargetDecoration.Color;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            var args = new ValidateOptionsEventArgs();
            args.Options.Sources = sources.Text.Trim();
            args.Options.Targets = targets.Text.Trim();
            args.Options.SeparateDestinateions = separateDestination.Checked;
            args.Options.Destinations = destinations.Text.Trim();
            args.Options.SourceDecoration.Underline = sourceUnderline.Checked;
            args.Options.SourceDecoration.Strikeout = sourceStrikeout.Checked;
            args.Options.SourceDecoration.Bold = sourceBold.Checked;
            args.Options.SourceDecoration.Color = sourceColorBox.BackColor;
            args.Options.TargetDecoration.Underline = targetUnderline.Checked;
            args.Options.TargetDecoration.Strikeout = targetStrikeout.Checked;
            args.Options.TargetDecoration.Bold = targetBold.Checked;
            args.Options.TargetDecoration.Color = targetColorBox.BackColor;

            var handler = ValidateOptions;
            if (handler != null)
            {
                handler(this, args);
            }

            if (!args.Invalid)
            {
                Options = args.Options;
                Close();
            }
        }

        private void colorPictureBox_Click(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            colorDialog.Color = pictureBox.BackColor;
            if (DialogResult.OK == colorDialog.ShowDialog(this))
            {
                pictureBox.BackColor = colorDialog.Color;
            }
        }
    }

}
