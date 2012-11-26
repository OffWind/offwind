namespace BrendanGrant.Helpers.FileAssociation.Example
{
    partial class AddVerbDialog
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
           this.verbCommandTextBox = new System.Windows.Forms.TextBox();
           this.okButton = new System.Windows.Forms.Button();
           this.cancelButton = new System.Windows.Forms.Button();
           this.verbNameTextBox = new System.Windows.Forms.TextBox();
           this.label1 = new System.Windows.Forms.Label();
           this.label2 = new System.Windows.Forms.Label();
           this.SuspendLayout();
           // 
           // verbCommandTextBox
           // 
           this.verbCommandTextBox.Location = new System.Drawing.Point(74, 43);
           this.verbCommandTextBox.Name = "verbCommandTextBox";
           this.verbCommandTextBox.Size = new System.Drawing.Size(393, 20);
           this.verbCommandTextBox.TabIndex = 2;
           // 
           // okButton
           // 
           this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
           this.okButton.Location = new System.Drawing.Point(311, 9);
           this.okButton.Name = "okButton";
           this.okButton.Size = new System.Drawing.Size(75, 23);
           this.okButton.TabIndex = 3;
           this.okButton.Text = "OK";
           this.okButton.UseVisualStyleBackColor = true;
           // 
           // cancelButton
           // 
           this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.cancelButton.Location = new System.Drawing.Point(392, 9);
           this.cancelButton.Name = "cancelButton";
           this.cancelButton.Size = new System.Drawing.Size(75, 23);
           this.cancelButton.TabIndex = 4;
           this.cancelButton.Text = "Cancel";
           this.cancelButton.UseVisualStyleBackColor = true;
           // 
           // verbNameTextBox
           // 
           this.verbNameTextBox.Location = new System.Drawing.Point(74, 12);
           this.verbNameTextBox.Name = "verbNameTextBox";
           this.verbNameTextBox.Size = new System.Drawing.Size(231, 20);
           this.verbNameTextBox.TabIndex = 1;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(30, 12);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(38, 13);
           this.label1.TabIndex = 4;
           this.label1.Text = "Name:";
           this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(12, 43);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(57, 13);
           this.label2.TabIndex = 5;
           this.label2.Text = "Command:";
           this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
           // 
           // AddVerbDialog
           // 
           this.AcceptButton = this.okButton;
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.CancelButton = this.cancelButton;
           this.ClientSize = new System.Drawing.Size(479, 78);
           this.Controls.Add(this.label2);
           this.Controls.Add(this.label1);
           this.Controls.Add(this.verbNameTextBox);
           this.Controls.Add(this.cancelButton);
           this.Controls.Add(this.okButton);
           this.Controls.Add(this.verbCommandTextBox);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "AddVerbDialog";
           this.Text = "Add Verb";
           this.ResumeLayout(false);
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox verbCommandTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox verbNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}