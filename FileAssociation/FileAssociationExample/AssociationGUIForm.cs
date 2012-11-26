using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

using BrendanGrant.Helpers.FileAssociation;

namespace BrendanGrant.Helpers.FileAssociation.Example
{
   public partial class AssociationGUIForm : Form
   {
      DateTime lastUpdate = DateTime.Now;
      string existingString = "";
      
      public AssociationGUIForm()
      {
         InitializeComponent();
      }

      private void Tester_Load(object sender, EventArgs e)
      {
         refreshExtensionsButton_Click(null, null);
      }

      private void extensionsListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         string extension = (string)extensionsListBox.SelectedItem;

         FileAssociationInfo fa = new FileAssociationInfo(extension);

         contentTypeTextBox.Text = fa.ContentType;
         contentTypeTextBox.Tag = contentTypeTextBox.Text;

         programIdTextBox.Text = fa.ProgID;
         programIdTextBox.Tag = programIdTextBox.Text;

         openWithListBox.DataSource = fa.OpenWithList;

         PerceivedTypeComboBox.DataSource = Enum.GetNames(typeof(PerceivedTypes));
         PerceivedTypeComboBox.SelectedIndex = (int)fa.PerceivedType;
         PerceivedTypeComboBox.Tag = PerceivedTypeComboBox.SelectedIndex;

         extensionLabel.Text = fa.Extension;

         if (fa.PersistentHandler != Guid.Empty)
         {
            persistentHandlerTextBox.Text = fa.PersistentHandler.ToString();
            persistentHandlerTextBox.Tag = fa.PersistentHandler;
         }
         else
         {
            persistentHandlerTextBox.Text = "";
         }

         if (fa.PersistentHandler != Guid.Empty)
         {
            persistentHandlerTextBox.Tag = persistentHandlerTextBox.Text = fa.PersistentHandler.ToString();
         }
         else
         {
            persistentHandlerTextBox.Tag = persistentHandlerTextBox.Text = "";
         }

         ProgramAssociationInfo pa = new ProgramAssociationInfo(fa.ProgID);
         programIdLabel.Text = fa.ProgID;

         if (pa.Exists)
         {
            programIdGroupBox.Enabled = true;

            EditFlags[] editFlags = (EditFlags[])Enum.GetValues(typeof(EditFlags));

            editFlagsListBox.Items.Clear();

            foreach (EditFlags flag in editFlags)
            {
               editFlagsListBox.Items.Add(flag);
            }

            EditFlags[] flags = (EditFlags[])Enum.GetValues(typeof(EditFlags));

            EditFlags setFlags = pa.EditFlags;
            editFlagsListBox.Tag = setFlags;

            for (int x = 1; x < flags.Length; x++)
            {
               EditFlags flag = flags[x];

               if ((setFlags & flag) == flag)
               {
                  editFlagsListBox.SetItemChecked(x, true);
               }
               else
               {
                  editFlagsListBox.SetItemChecked(x, false);
               }
            }

            if (setFlags == EditFlags.None)
            {
               editFlagsListBox.SetItemChecked((int)EditFlags.None, true);
            }
            else
            {
               editFlagsListBox.SetItemChecked((int)EditFlags.None, false);
            }


            editFlagsListBox.Tag = GetIntArray(editFlagsListBox);

            ProgramVerb[] verbs = pa.Verbs;
            verbsTreeView.Nodes.Clear();
            foreach (ProgramVerb verb in verbs)
            {
               TreeNode node = new TreeNode(verb.Name);
               node.Nodes.Add(new TreeNode(verb.Command));
               node.Nodes[0].Tag = "command";
               verbsTreeView.Nodes.Add(node);
            }
            verbsTreeView.ExpandAll();

            descriptionTextBox.Text = pa.Description;
            descriptionTextBox.Tag = descriptionTextBox.Text;


            alwaysShowExtCheckBox.Checked = pa.AlwaysShowExtension;
            alwaysShowExtCheckBox.Tag = alwaysShowExtCheckBox.Checked;

            if (pa.DefaultIcon != ProgramIcon.None)
            {
               iconPathTextBox.Text = pa.DefaultIcon.Path;
               iconIndexTextBox.Text = pa.DefaultIcon.Index.ToString();
            }
            else
            {
               iconPathTextBox.Text = "";
               iconIndexTextBox.Text = "";
            }

         }
         else
         {
            programIdGroupBox.Enabled = false;

            editFlagsListBox.Items.Clear();
            verbsTreeView.Nodes.Clear();

            descriptionTextBox.Text = "";
            descriptionTextBox.Tag = "";
         }

      }

      //Refresh extension list
      private void refreshExtensionsButton_Click(object sender, EventArgs e)
      {
         RegistryKey root = Registry.ClassesRoot;

         string[] subKeys = root.GetSubKeyNames();

         extensionsListBox.Items.Clear();
         foreach (string subKey in subKeys)
         {
            if (subKey.StartsWith("."))
            {
               extensionsListBox.Items.Add(subKey);
            }
         }
      }

      //Update values related to extension that have changed
      private void extensionUpdateButton_Click(object sender, EventArgs e)
      {
         string extension = (string)extensionsListBox.SelectedItem;

         FileAssociationInfo fa = new FileAssociationInfo(extension);

         if (programIdTextBox.Text != (string)programIdTextBox.Tag)
         {
            fa.ProgID = programIdTextBox.Text;
         }

         if (contentTypeTextBox.Text != (string)contentTypeTextBox.Tag)
         {
            fa.ContentType = contentTypeTextBox.Text;
         }

         if (PerceivedTypeComboBox.SelectedIndex != (int)PerceivedTypeComboBox.Tag)
         {
            fa.PerceivedType = (PerceivedTypes)PerceivedTypeComboBox.SelectedIndex;
         }

         if (persistentHandlerTextBox.Text != (string)persistentHandlerTextBox.Tag)
         {
            fa.PersistentHandler = new Guid(persistentHandlerTextBox.Text);
         }

         refreshExtensionsButton_Click(null, null);
         extensionsListBox.SelectedItem = extension;
      }

      //Update values related to program id that have changed
      private void updateProgIdButton_Click(object sender, EventArgs e)
      {

         string extension = (string)extensionsListBox.SelectedItem;
         ProgramAssociationInfo pa = new ProgramAssociationInfo(programIdTextBox.Text);
         int[] tmpArray;

         if (pa.Exists)
         {
            if ((string)descriptionTextBox.Tag != descriptionTextBox.Text)
            {
               pa.Description = descriptionTextBox.Text;
            }

            EditFlags editFlags = EditFlags.None;

            tmpArray = GetIntArray(editFlagsListBox);
            if (!IntArraysEqual(tmpArray, (int[])editFlagsListBox.Tag))
            {

               for (int x = 0; x < tmpArray.Length; x++)
               {
                  //If value == 0 then None was selected and will override all others
                  //Do not handle first index, is None
                  if (tmpArray[x] == 0)
                     break;

                  editFlags |= (EditFlags)(1 << (tmpArray[x]-1));
               }

               pa.EditFlags = editFlags;
            }

            pa.DefaultIcon = new FileAssociation.ProgramIcon(iconPathTextBox.Text, 0);

            if (alwaysShowExtCheckBox.Checked != (bool)alwaysShowExtCheckBox.Tag)
            {
               pa.AlwaysShowExtension = alwaysShowExtCheckBox.Checked;
            }

         }

         refreshExtensionsButton_Click(null, null);
         extensionsListBox.SelectedItem = extension;
      }

      //Creates an array of checked indices of list box
      private int[] GetIntArray(CheckedListBox listBox)
      {
         List<int> intArray = new List<int>();
         foreach (int o in listBox.CheckedIndices)
         {
            intArray.Add(o);
         }
         return intArray.ToArray();
      }
      
      //Checks if the two arrays contain the same values
      private bool IntArraysEqual(int[] array1, int[] array2)
      {
         if (array1 == null || array2 == null)
            return false;

         if (array1.Length != array2.Length)
            return false;

         for (int x = 0; x < array1.Length; x++)
         {
            if (array1[x] != array2[x])
               return false;
         }

         return true;

      }

      //Wipes out existing open with list and replaces them with provided list
      private void addOpenWith_Click(object sender, EventArgs e)
      {
         string extension = (string)extensionsListBox.SelectedItem;

         FileAssociationInfo fa = new FileAssociationInfo(extension);

         if (!fa.Exists)
         {
            return;
         }

         AddOpenWithDialog f = new AddOpenWithDialog();
         if (f.ShowDialog() == DialogResult.OK)
         {
            List<string> l = new List<string>();

            l.AddRange(fa.OpenWithList);
            if (!l.Contains(f.ProgramName))
            {
               l.Add(f.ProgramName);
               fa.OpenWithList = l.ToArray();

               refreshExtensionsButton_Click(null, null);
               extensionsListBox.SelectedItem = extension;
            }
         }
      
      }
      //Wipes out existing open with list and replaces them with provided list
      private void removeOpenWith_Click(object sender, EventArgs e)
      {
         string extension = (string)extensionsListBox.SelectedItem;

         FileAssociationInfo fa = new FileAssociationInfo(extension);

         if (!fa.Exists)
         {
            return;
         }

         List<string> l = new List<string>();

         l.AddRange(fa.OpenWithList);


         if (l.Contains(openWithListBox.SelectedItem.ToString()))
         {
            l.Remove(openWithListBox.SelectedItem.ToString());
            fa.OpenWithList = l.ToArray();

            refreshExtensionsButton_Click(null, null);
            extensionsListBox.SelectedItem = extension;
         }

      }

      //Wipes out existing verbs and replaces them with provided list
      private void addVerbButton_Click(object sender, EventArgs e)
      {
         string extension = (string)extensionsListBox.SelectedItem;
         ProgramAssociationInfo pa = new ProgramAssociationInfo(programIdTextBox.Text);

         if (!pa.Exists)
         {
            return;
         }

         AddVerbDialog d = new AddVerbDialog();

         if (d.ShowDialog() == DialogResult.OK)
         {
            ProgramVerb[] verbs = pa.Verbs;
            List<ProgramVerb> l = new List<ProgramVerb>();
            l.AddRange(verbs);

            if (!l.Contains(new ProgramVerb(d.VerbName, d.VerbCommand)))
            {
               l.Add(new ProgramVerb(d.VerbName, d.VerbCommand));
               pa.Verbs = l.ToArray();

               refreshExtensionsButton_Click(null, null);
               extensionsListBox.SelectedItem = extension;
            }

         }

      }

      //Wipes out existing verbs and replaces them with provided list
      private void removeVerbButton_Click(object sender, EventArgs e)
      {
         TreeNode node = verbsTreeView.SelectedNode;
         if (node == null)
            return;

         if (node.Tag != null && node.Tag.ToString() == "command")
         {
            node = node.Parent;
         }

         string extension = (string)extensionsListBox.SelectedItem;
         ProgramAssociationInfo pa = new ProgramAssociationInfo(programIdTextBox.Text);

         if (!pa.Exists)
         {
            return;
         }

         ProgramVerb[] verbs = pa.Verbs;
         List<ProgramVerb> l = new List<ProgramVerb>();
         l.AddRange(verbs);

         ProgramVerb verbToRemove = null;

         foreach (ProgramVerb verb in l)
         {
            if (verb.Name == node.Text)
            {
               verbToRemove = verb;
               break;
            }
         }

         if (verbToRemove != null)
         {
            l.Remove(verbToRemove);
            pa.Verbs = l.ToArray();

            refreshExtensionsButton_Click(null, null);
            extensionsListBox.SelectedItem = extension;
         }


      }

      //Add single verb without affecting existing verbs
      private void addSingleVerbButton_Click(object sender, EventArgs e)
      {
         string extension = (string)extensionsListBox.SelectedItem;
         ProgramAssociationInfo pa = new ProgramAssociationInfo(programIdTextBox.Text);

         if (!pa.Exists)
         {
            return;
         }

         AddVerbDialog d = new AddVerbDialog();

         if (d.ShowDialog() == DialogResult.OK)
         {
            ProgramVerb[] verbs = pa.Verbs;
            ProgramVerb newVerb = new ProgramVerb(d.VerbName, d.VerbCommand);
            List<ProgramVerb> l = new List<ProgramVerb>();

            if (!l.Contains(newVerb))
            {
               pa.AddVerb(newVerb);

               refreshExtensionsButton_Click(null, null);
               extensionsListBox.SelectedItem = extension;
            }
         }
      }

      //Removes single verb from program id without affecting existing verbs
      private void removeSingleVerbButton_Click(object sender, EventArgs e)
      {
         TreeNode node = verbsTreeView.SelectedNode;
         if (node == null)
            return;

         if (node.Tag != null && node.Tag.ToString() == "command")
         {
            node = node.Parent;
         }

         string extension = (string)extensionsListBox.SelectedItem;
         ProgramAssociationInfo pa = new ProgramAssociationInfo(programIdTextBox.Text);

         if (!pa.Exists)
         {
            return;
         }

         pa.RemoveVerb(node.Text);

         refreshExtensionsButton_Click(null, null);
         extensionsListBox.SelectedItem = extension;

      }

      private void extensionsListBox_KeyPress(object sender, KeyPressEventArgs e)
      {
         if (DateTime.Now.Subtract(lastUpdate).TotalMilliseconds > 250)
         {
            existingString = "";
         }

         existingString += e.KeyChar;
         Console.WriteLine("Checking: {0}", existingString);
         for (int x = 0; x < extensionsListBox.Items.Count; x++)
         {
            if (extensionsListBox.Items[x].ToString().ToLower().StartsWith(existingString.ToLower()))
            {
               extensionsListBox.SelectedIndex = x;
               break;
            }
         }

         lastUpdate = DateTime.Now;
      }

      //Prompt for new extension and create it if it does not exist
      private void addExtension_Click(object sender, EventArgs e)
      {
         NewExtensionDialog dialog = new NewExtensionDialog();

         if (dialog.ShowDialog() == DialogResult.OK)
         {
            FileAssociationInfo fai = new FileAssociationInfo(dialog.Extension);

            if (fai.Exists)
            {
               MessageBox.Show("Specified extension already exists and will not be added");
            }
            else
            {
               fai.Create();
            }
            
            refreshExtensionsButton_Click(null, null);
         }

      }

      //Prompt for new program id and create it if it does not exist.
      private void newProgramAssociationButton_Click(object sender, EventArgs e)
      {
         NewProgramAssociationDialog dialog = new NewProgramAssociationDialog();

         if (dialog.ShowDialog() == DialogResult.OK)
         {
            ProgramAssociationInfo pai = new ProgramAssociationInfo(dialog.ProgramID);

            if (pai.Exists)
            {
               MessageBox.Show("Specified program already exists and will not be added");
            }
            else
            {
               pai.Create();
            }

            refreshExtensionsButton_Click(null, null);
         }

      }


   }
}