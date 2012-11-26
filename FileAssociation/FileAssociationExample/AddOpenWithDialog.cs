using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BrendanGrant.Helpers.FileAssociation.Example
{
    public partial class AddOpenWithDialog : Form
    {
        public AddOpenWithDialog()
        {
            InitializeComponent();
        }

        public string ProgramName
        {
            get { return newProgramTextBox.Text; }
            set { newProgramTextBox.Text = value; }
        }
	

    }
}