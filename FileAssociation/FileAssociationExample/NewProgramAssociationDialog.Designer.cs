namespace BrendanGrant.Helpers.FileAssociation.Example
{
   partial class NewProgramAssociationDialog
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
         this.label1 = new System.Windows.Forms.Label();
         this.programIdTextBox = new System.Windows.Forms.TextBox();
         this.cancelButton = new System.Windows.Forms.Button();
         this.okButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(12, 9);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(68, 23);
         this.label1.TabIndex = 7;
         this.label1.Text = "Program ID:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // programIdTextBox
         // 
         this.programIdTextBox.Location = new System.Drawing.Point(86, 12);
         this.programIdTextBox.Name = "programIdTextBox";
         this.programIdTextBox.Size = new System.Drawing.Size(196, 20);
         this.programIdTextBox.TabIndex = 0;
         // 
         // cancelButton
         // 
         this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cancelButton.Location = new System.Drawing.Point(288, 38);
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size(75, 23);
         this.cancelButton.TabIndex = 2;
         this.cancelButton.Text = "Cancel";
         this.cancelButton.UseVisualStyleBackColor = true;
         // 
         // okButton
         // 
         this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.okButton.Location = new System.Drawing.Point(288, 9);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(75, 23);
         this.okButton.TabIndex = 1;
         this.okButton.Text = "OK";
         this.okButton.UseVisualStyleBackColor = true;
         // 
         // NewProgramAssociationDialog
         // 
         this.AcceptButton = this.okButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.cancelButton;
         this.ClientSize = new System.Drawing.Size(375, 75);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.programIdTextBox);
         this.Controls.Add(this.cancelButton);
         this.Controls.Add(this.okButton);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "NewProgramAssociationDialog";
         this.Text = "New Program ID";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox programIdTextBox;
      private System.Windows.Forms.Button cancelButton;
      private System.Windows.Forms.Button okButton;
   }
}