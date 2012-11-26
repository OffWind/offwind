using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BrendanGrant.Helpers.FileAssociation.Example
{
   public partial class NewProgramAssociationDialog : Form
   {
      public NewProgramAssociationDialog()
      {
         InitializeComponent();
      }

      public string ProgramID
      {
         get
         {
            return programIdTextBox.Text;
         }
         set
         {
            programIdTextBox.Text = value;
         }
      }
   }
}