using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BrendanGrant.Helpers.FileAssociation.Example
{
    public partial class AddVerbDialog : Form
    {
        public AddVerbDialog()
        {
            InitializeComponent();
        }

        public string VerbName
        {
            get { return verbNameTextBox.Text; }
            set { verbNameTextBox.Text = value; }
        }

        public string VerbCommand
        {
            get { return verbCommandTextBox.Text; }
            set { verbCommandTextBox.Text = value; }
        }
	

    }
}