using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CellDiff
{
    public partial class Advanced : Form
    {
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

        public class ValidateOptionsEventArgs : EventArgs
        {
            public OptionValues Options;
            public bool Invalid;
        }

        public delegate void ValidateOptionsEvent(object sender, ValidateOptionsEventArgs e);

        public event ValidateOptionsEvent ValidateOptions;

        public Advanced()
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
            sourceColor.Text = Options.SourceDecoration.Color.ToString("X6");
            targetUnderline.Checked = Options.TargetDecoration.Underline;
            targetStrikeout.Checked = Options.TargetDecoration.Strikeout;
            targetBold.Checked = Options.TargetDecoration.Bold;
            targetColor.Text = Options.TargetDecoration.Color.ToString("X6");
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
            args.Options.TargetDecoration.Underline = targetUnderline.Checked;
            args.Options.TargetDecoration.Strikeout = targetStrikeout.Checked;
            args.Options.TargetDecoration.Bold = targetBold.Checked;

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
    }

}
