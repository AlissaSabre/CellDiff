using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alissa.GUI
{
    public partial class ExceptionDialog : Form
    {
        public ExceptionDialog()
        {
            InitializeComponent();
        }

        private int OriginalFormHeight;

        private void ExceptionDialog_Load(object sender, EventArgs e)
        {
            OriginalFormHeight = Height;
            Height = MinimumSize.Height;
            detailButton.Enabled = true;
        }

        private Exception _Exception;

        public Exception Exception
        {
            get { return _Exception; }
            set
            {
                _Exception = value;
                messageTextBox.Text = value.Message;
                messageTextBox.Select(0, 0);
                detailTextBox.Text = value.ToString();
                detailTextBox.Select(0, 0);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void detailButton_Click(object sender, EventArgs e)
        {
            Height = OriginalFormHeight;
        }

        private void ExceptionDialog_SizeChanged(object sender, EventArgs e)
        {
            detailButton.Enabled = !ClientRectangle.Contains(detailTextBox.Location);
        }
    }
}
