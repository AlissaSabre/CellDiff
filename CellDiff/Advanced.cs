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
            public bool SourceUnderline;
            public bool SourceStrikeout;
            public bool SourceBold;
            public int SourceColor;
            public bool TargetUnderline;
            public bool TargetStrikeout;
            public bool TargetBold;
            public int TargetColor;
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
            sourceUnderline.Checked = Options.SourceUnderline;
            sourceStrikeout.Checked = Options.SourceStrikeout;
            sourceBold.Checked = Options.SourceBold;
            //sourceColor.Text = Options.SourceColor;
            targetUnderline.Checked = Options.TargetUnderline;
            targetStrikeout.Checked = Options.TargetStrikeout;
            targetBold.Checked = Options.TargetBold;
            //targetColor.Text = Options.TargetColor;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            var args = new ValidateOptionsEventArgs();
            args.Options.Sources = sources.Text.Trim();
            args.Options.Targets = targets.Text.Trim();
            args.Options.SeparateDestinateions = separateDestination.Checked;
            args.Options.Destinations = destinations.Text.Trim();
            args.Options.SourceUnderline = sourceUnderline.Checked;
            args.Options.SourceStrikeout = sourceStrikeout.Checked;
            args.Options.SourceBold = sourceBold.Checked;
            args.Options.TargetUnderline = targetUnderline.Checked;
            args.Options.TargetStrikeout = targetStrikeout.Checked;
            args.Options.TargetBold = targetBold.Checked;

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
