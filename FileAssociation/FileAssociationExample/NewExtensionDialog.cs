using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BrendanGrant.Helpers.FileAssociation.Example
{
   public partial class NewExtensionDialog : Form
   {
      public NewExtensionDialog()
      {
         InitializeComponent();
      }

      public string Extension
      {
         get
         {
            if (!extensionTextBox.Text.StartsWith("."))
               extensionTextBox.Text = "." + extensionTextBox.Text;

            return extensionTextBox.Text;
         }
         set
         {
            if (!value.StartsWith("."))
               value = "." + value;

            extensionTextBox.Text = value;
         }
      }
	
   }
}