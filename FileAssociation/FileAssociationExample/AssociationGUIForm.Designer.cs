namespace BrendanGrant.Helpers.FileAssociation.Example
{
    partial class AssociationGUIForm
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
           this.extensionsListBox = new System.Windows.Forms.ListBox();
           this.contentTypeTextBox = new System.Windows.Forms.TextBox();
           this.openWithListBox = new System.Windows.Forms.ListBox();
           this.PerceivedTypeComboBox = new System.Windows.Forms.ComboBox();
           this.programIdTextBox = new System.Windows.Forms.TextBox();
           this.addOpenWith = new System.Windows.Forms.Button();
           this.removeOpenWith = new System.Windows.Forms.Button();
           this.editFlagsListBox = new System.Windows.Forms.CheckedListBox();
           this.descriptionTextBox = new System.Windows.Forms.TextBox();
           this.verbsTreeView = new System.Windows.Forms.TreeView();
           this.extensionGroupBox = new System.Windows.Forms.GroupBox();
           this.label8 = new System.Windows.Forms.Label();
           this.label7 = new System.Windows.Forms.Label();
           this.label6 = new System.Windows.Forms.Label();
           this.label5 = new System.Windows.Forms.Label();
           this.label4 = new System.Windows.Forms.Label();
           this.persistentHandlerTextBox = new System.Windows.Forms.TextBox();
           this.extensionUpdateButton = new System.Windows.Forms.Button();
           this.extensionLabel = new System.Windows.Forms.Label();
           this.label3 = new System.Windows.Forms.Label();
           this.refreshExtensionsButton = new System.Windows.Forms.Button();
           this.programIdGroupBox = new System.Windows.Forms.GroupBox();
           this.label10 = new System.Windows.Forms.Label();
           this.label9 = new System.Windows.Forms.Label();
           this.alwaysShowExtCheckBox = new System.Windows.Forms.CheckBox();
           this.label2 = new System.Windows.Forms.Label();
           this.iconIndexTextBox = new System.Windows.Forms.TextBox();
           this.iconPathTextBox = new System.Windows.Forms.TextBox();
           this.removeSingleVerbButton = new System.Windows.Forms.Button();
           this.addSingleVerbButton = new System.Windows.Forms.Button();
           this.removeVerbButton = new System.Windows.Forms.Button();
           this.addVerbButton = new System.Windows.Forms.Button();
           this.updateProgIdButton = new System.Windows.Forms.Button();
           this.programIdLabel = new System.Windows.Forms.Label();
           this.label1 = new System.Windows.Forms.Label();
           this.groupBox1 = new System.Windows.Forms.GroupBox();
           this.addExtension = new System.Windows.Forms.Button();
           this.newProgramAssociationButton = new System.Windows.Forms.Button();
           this.extensionGroupBox.SuspendLayout();
           this.programIdGroupBox.SuspendLayout();
           this.groupBox1.SuspendLayout();
           this.SuspendLayout();
           // 
           // extensionsListBox
           // 
           this.extensionsListBox.FormattingEnabled = true;
           this.extensionsListBox.Location = new System.Drawing.Point(18, 19);
           this.extensionsListBox.Name = "extensionsListBox";
           this.extensionsListBox.Size = new System.Drawing.Size(120, 173);
           this.extensionsListBox.TabIndex = 0;
           this.extensionsListBox.SelectedIndexChanged += new System.EventHandler(this.extensionsListBox_SelectedIndexChanged);
           this.extensionsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.extensionsListBox_KeyPress);
           // 
           // contentTypeTextBox
           // 
           this.contentTypeTextBox.Location = new System.Drawing.Point(105, 67);
           this.contentTypeTextBox.Name = "contentTypeTextBox";
           this.contentTypeTextBox.Size = new System.Drawing.Size(121, 20);
           this.contentTypeTextBox.TabIndex = 1;
           // 
           // openWithListBox
           // 
           this.openWithListBox.FormattingEnabled = true;
           this.openWithListBox.Location = new System.Drawing.Point(106, 120);
           this.openWithListBox.Name = "openWithListBox";
           this.openWithListBox.Size = new System.Drawing.Size(120, 95);
           this.openWithListBox.TabIndex = 3;
           // 
           // PerceivedTypeComboBox
           // 
           this.PerceivedTypeComboBox.FormattingEnabled = true;
           this.PerceivedTypeComboBox.Location = new System.Drawing.Point(105, 93);
           this.PerceivedTypeComboBox.Name = "PerceivedTypeComboBox";
           this.PerceivedTypeComboBox.Size = new System.Drawing.Size(121, 21);
           this.PerceivedTypeComboBox.TabIndex = 2;
           // 
           // programIdTextBox
           // 
           this.programIdTextBox.Location = new System.Drawing.Point(105, 41);
           this.programIdTextBox.Name = "programIdTextBox";
           this.programIdTextBox.Size = new System.Drawing.Size(121, 20);
           this.programIdTextBox.TabIndex = 0;
           // 
           // addOpenWith
           // 
           this.addOpenWith.Location = new System.Drawing.Point(232, 120);
           this.addOpenWith.Name = "addOpenWith";
           this.addOpenWith.Size = new System.Drawing.Size(54, 23);
           this.addOpenWith.TabIndex = 5;
           this.addOpenWith.Text = "Add";
           this.addOpenWith.UseVisualStyleBackColor = true;
           this.addOpenWith.Click += new System.EventHandler(this.addOpenWith_Click);
           // 
           // removeOpenWith
           // 
           this.removeOpenWith.Location = new System.Drawing.Point(232, 149);
           this.removeOpenWith.Name = "removeOpenWith";
           this.removeOpenWith.Size = new System.Drawing.Size(55, 23);
           this.removeOpenWith.TabIndex = 6;
           this.removeOpenWith.Text = "Remove";
           this.removeOpenWith.UseVisualStyleBackColor = true;
           this.removeOpenWith.Click += new System.EventHandler(this.removeOpenWith_Click);
           // 
           // editFlagsListBox
           // 
           this.editFlagsListBox.FormattingEnabled = true;
           this.editFlagsListBox.Location = new System.Drawing.Point(84, 67);
           this.editFlagsListBox.Name = "editFlagsListBox";
           this.editFlagsListBox.Size = new System.Drawing.Size(171, 184);
           this.editFlagsListBox.TabIndex = 1;
           // 
           // descriptionTextBox
           // 
           this.descriptionTextBox.Location = new System.Drawing.Point(84, 43);
           this.descriptionTextBox.Name = "descriptionTextBox";
           this.descriptionTextBox.Size = new System.Drawing.Size(172, 20);
           this.descriptionTextBox.TabIndex = 0;
           // 
           // verbsTreeView
           // 
           this.verbsTreeView.HideSelection = false;
           this.verbsTreeView.Location = new System.Drawing.Point(261, 70);
           this.verbsTreeView.Name = "verbsTreeView";
           this.verbsTreeView.Size = new System.Drawing.Size(334, 152);
           this.verbsTreeView.TabIndex = 2;
           // 
           // extensionGroupBox
           // 
           this.extensionGroupBox.Controls.Add(this.label8);
           this.extensionGroupBox.Controls.Add(this.label7);
           this.extensionGroupBox.Controls.Add(this.label6);
           this.extensionGroupBox.Controls.Add(this.label5);
           this.extensionGroupBox.Controls.Add(this.label4);
           this.extensionGroupBox.Controls.Add(this.persistentHandlerTextBox);
           this.extensionGroupBox.Controls.Add(this.extensionUpdateButton);
           this.extensionGroupBox.Controls.Add(this.extensionLabel);
           this.extensionGroupBox.Controls.Add(this.programIdTextBox);
           this.extensionGroupBox.Controls.Add(this.label3);
           this.extensionGroupBox.Controls.Add(this.contentTypeTextBox);
           this.extensionGroupBox.Controls.Add(this.openWithListBox);
           this.extensionGroupBox.Controls.Add(this.PerceivedTypeComboBox);
           this.extensionGroupBox.Controls.Add(this.removeOpenWith);
           this.extensionGroupBox.Controls.Add(this.addOpenWith);
           this.extensionGroupBox.Location = new System.Drawing.Point(180, 12);
           this.extensionGroupBox.Name = "extensionGroupBox";
           this.extensionGroupBox.Size = new System.Drawing.Size(367, 261);
           this.extensionGroupBox.TabIndex = 1;
           this.extensionGroupBox.TabStop = false;
           this.extensionGroupBox.Text = "File Association Info";
           // 
           // label8
           // 
           this.label8.AutoSize = true;
           this.label8.Location = new System.Drawing.Point(19, 120);
           this.label8.Name = "label8";
           this.label8.Size = new System.Drawing.Size(80, 13);
           this.label8.TabIndex = 32;
           this.label8.Text = "Open With List:";
           // 
           // label7
           // 
           this.label7.AutoSize = true;
           this.label7.Location = new System.Drawing.Point(14, 96);
           this.label7.Name = "label7";
           this.label7.Size = new System.Drawing.Size(85, 13);
           this.label7.TabIndex = 31;
           this.label7.Text = "Perceived Type:";
           // 
           // label6
           // 
           this.label6.AutoSize = true;
           this.label6.Location = new System.Drawing.Point(25, 70);
           this.label6.Name = "label6";
           this.label6.Size = new System.Drawing.Size(74, 13);
           this.label6.TabIndex = 30;
           this.label6.Text = "Content Type:";
           // 
           // label5
           // 
           this.label5.AutoSize = true;
           this.label5.Location = new System.Drawing.Point(56, 44);
           this.label5.Name = "label5";
           this.label5.Size = new System.Drawing.Size(43, 13);
           this.label5.TabIndex = 29;
           this.label5.Text = "ProgID:";
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(6, 228);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(93, 13);
           this.label4.TabIndex = 28;
           this.label4.Text = "PersistentHandler:";
           // 
           // persistentHandlerTextBox
           // 
           this.persistentHandlerTextBox.Location = new System.Drawing.Point(106, 227);
           this.persistentHandlerTextBox.Name = "persistentHandlerTextBox";
           this.persistentHandlerTextBox.Size = new System.Drawing.Size(241, 20);
           this.persistentHandlerTextBox.TabIndex = 4;
           this.persistentHandlerTextBox.Text = "ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ";
           // 
           // extensionUpdateButton
           // 
           this.extensionUpdateButton.Location = new System.Drawing.Point(232, 198);
           this.extensionUpdateButton.Name = "extensionUpdateButton";
           this.extensionUpdateButton.Size = new System.Drawing.Size(115, 23);
           this.extensionUpdateButton.TabIndex = 7;
           this.extensionUpdateButton.Text = "Update";
           this.extensionUpdateButton.UseVisualStyleBackColor = true;
           this.extensionUpdateButton.Click += new System.EventHandler(this.extensionUpdateButton_Click);
           // 
           // extensionLabel
           // 
           this.extensionLabel.Location = new System.Drawing.Point(106, 22);
           this.extensionLabel.Name = "extensionLabel";
           this.extensionLabel.Size = new System.Drawing.Size(120, 13);
           this.extensionLabel.TabIndex = 17;
           // 
           // label3
           // 
           this.label3.AutoSize = true;
           this.label3.Location = new System.Drawing.Point(56, 22);
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size(44, 13);
           this.label3.TabIndex = 16;
           this.label3.Text = "Current:";
           // 
           // refreshExtensionsButton
           // 
           this.refreshExtensionsButton.Location = new System.Drawing.Point(18, 203);
           this.refreshExtensionsButton.Name = "refreshExtensionsButton";
           this.refreshExtensionsButton.Size = new System.Drawing.Size(120, 23);
           this.refreshExtensionsButton.TabIndex = 1;
           this.refreshExtensionsButton.Text = "Refresh Extensions";
           this.refreshExtensionsButton.UseVisualStyleBackColor = true;
           this.refreshExtensionsButton.Click += new System.EventHandler(this.refreshExtensionsButton_Click);
           // 
           // programIdGroupBox
           // 
           this.programIdGroupBox.Controls.Add(this.label10);
           this.programIdGroupBox.Controls.Add(this.label9);
           this.programIdGroupBox.Controls.Add(this.alwaysShowExtCheckBox);
           this.programIdGroupBox.Controls.Add(this.label2);
           this.programIdGroupBox.Controls.Add(this.iconIndexTextBox);
           this.programIdGroupBox.Controls.Add(this.iconPathTextBox);
           this.programIdGroupBox.Controls.Add(this.removeSingleVerbButton);
           this.programIdGroupBox.Controls.Add(this.addSingleVerbButton);
           this.programIdGroupBox.Controls.Add(this.removeVerbButton);
           this.programIdGroupBox.Controls.Add(this.addVerbButton);
           this.programIdGroupBox.Controls.Add(this.updateProgIdButton);
           this.programIdGroupBox.Controls.Add(this.programIdLabel);
           this.programIdGroupBox.Controls.Add(this.label1);
           this.programIdGroupBox.Controls.Add(this.descriptionTextBox);
           this.programIdGroupBox.Controls.Add(this.editFlagsListBox);
           this.programIdGroupBox.Controls.Add(this.verbsTreeView);
           this.programIdGroupBox.Location = new System.Drawing.Point(12, 279);
           this.programIdGroupBox.Name = "programIdGroupBox";
           this.programIdGroupBox.Size = new System.Drawing.Size(613, 290);
           this.programIdGroupBox.TabIndex = 2;
           this.programIdGroupBox.TabStop = false;
           this.programIdGroupBox.Text = "Program Association Info";
           // 
           // label10
           // 
           this.label10.AutoSize = true;
           this.label10.Location = new System.Drawing.Point(22, 70);
           this.label10.Name = "label10";
           this.label10.Size = new System.Drawing.Size(56, 13);
           this.label10.TabIndex = 29;
           this.label10.Text = "Edit Flags:";
           // 
           // label9
           // 
           this.label9.AutoSize = true;
           this.label9.Location = new System.Drawing.Point(15, 46);
           this.label9.Name = "label9";
           this.label9.Size = new System.Drawing.Size(63, 13);
           this.label9.TabIndex = 28;
           this.label9.Text = "Description:";
           // 
           // alwaysShowExtCheckBox
           // 
           this.alwaysShowExtCheckBox.AutoSize = true;
           this.alwaysShowExtCheckBox.Location = new System.Drawing.Point(261, 45);
           this.alwaysShowExtCheckBox.Name = "alwaysShowExtCheckBox";
           this.alwaysShowExtCheckBox.Size = new System.Drawing.Size(138, 17);
           this.alwaysShowExtCheckBox.TabIndex = 9;
           this.alwaysShowExtCheckBox.Text = "Always Show Extension";
           this.alwaysShowExtCheckBox.UseVisualStyleBackColor = true;
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(10, 260);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(68, 13);
           this.label2.TabIndex = 26;
           this.label2.Text = "Default Icon:";
           // 
           // iconIndexTextBox
           // 
           this.iconIndexTextBox.Location = new System.Drawing.Point(556, 257);
           this.iconIndexTextBox.Name = "iconIndexTextBox";
           this.iconIndexTextBox.Size = new System.Drawing.Size(39, 20);
           this.iconIndexTextBox.TabIndex = 4;
           // 
           // iconPathTextBox
           // 
           this.iconPathTextBox.Location = new System.Drawing.Point(84, 257);
           this.iconPathTextBox.Name = "iconPathTextBox";
           this.iconPathTextBox.Size = new System.Drawing.Size(466, 20);
           this.iconPathTextBox.TabIndex = 3;
           // 
           // removeSingleVerbButton
           // 
           this.removeSingleVerbButton.Location = new System.Drawing.Point(356, 228);
           this.removeSingleVerbButton.Name = "removeSingleVerbButton";
           this.removeSingleVerbButton.Size = new System.Drawing.Size(89, 23);
           this.removeSingleVerbButton.TabIndex = 6;
           this.removeSingleVerbButton.Text = "Single Remove";
           this.removeSingleVerbButton.UseVisualStyleBackColor = true;
           this.removeSingleVerbButton.Click += new System.EventHandler(this.removeSingleVerbButton_Click);
           // 
           // addSingleVerbButton
           // 
           this.addSingleVerbButton.Location = new System.Drawing.Point(261, 228);
           this.addSingleVerbButton.Name = "addSingleVerbButton";
           this.addSingleVerbButton.Size = new System.Drawing.Size(89, 23);
           this.addSingleVerbButton.TabIndex = 5;
           this.addSingleVerbButton.Text = "Single Add";
           this.addSingleVerbButton.UseVisualStyleBackColor = true;
           this.addSingleVerbButton.Click += new System.EventHandler(this.addSingleVerbButton_Click);
           // 
           // removeVerbButton
           // 
           this.removeVerbButton.Location = new System.Drawing.Point(528, 228);
           this.removeVerbButton.Name = "removeVerbButton";
           this.removeVerbButton.Size = new System.Drawing.Size(67, 23);
           this.removeVerbButton.TabIndex = 8;
           this.removeVerbButton.Text = "Remove";
           this.removeVerbButton.UseVisualStyleBackColor = true;
           this.removeVerbButton.Click += new System.EventHandler(this.removeVerbButton_Click);
           // 
           // addVerbButton
           // 
           this.addVerbButton.Location = new System.Drawing.Point(455, 228);
           this.addVerbButton.Name = "addVerbButton";
           this.addVerbButton.Size = new System.Drawing.Size(67, 23);
           this.addVerbButton.TabIndex = 7;
           this.addVerbButton.Text = "Add";
           this.addVerbButton.UseVisualStyleBackColor = true;
           this.addVerbButton.Click += new System.EventHandler(this.addVerbButton_Click);
           // 
           // updateProgIdButton
           // 
           this.updateProgIdButton.Location = new System.Drawing.Point(475, 41);
           this.updateProgIdButton.Name = "updateProgIdButton";
           this.updateProgIdButton.Size = new System.Drawing.Size(120, 23);
           this.updateProgIdButton.TabIndex = 10;
           this.updateProgIdButton.Text = "Update";
           this.updateProgIdButton.UseVisualStyleBackColor = true;
           this.updateProgIdButton.Click += new System.EventHandler(this.updateProgIdButton_Click);
           // 
           // programIdLabel
           // 
           this.programIdLabel.Location = new System.Drawing.Point(84, 26);
           this.programIdLabel.Name = "programIdLabel";
           this.programIdLabel.Size = new System.Drawing.Size(171, 14);
           this.programIdLabel.TabIndex = 15;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(34, 26);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(44, 13);
           this.label1.TabIndex = 4;
           this.label1.Text = "Current:";
           // 
           // groupBox1
           // 
           this.groupBox1.Controls.Add(this.addExtension);
           this.groupBox1.Controls.Add(this.extensionsListBox);
           this.groupBox1.Controls.Add(this.refreshExtensionsButton);
           this.groupBox1.Location = new System.Drawing.Point(12, 12);
           this.groupBox1.Name = "groupBox1";
           this.groupBox1.Size = new System.Drawing.Size(162, 261);
           this.groupBox1.TabIndex = 0;
           this.groupBox1.TabStop = false;
           this.groupBox1.Text = "Available Extensions";
           // 
           // addExtension
           // 
           this.addExtension.Location = new System.Drawing.Point(18, 232);
           this.addExtension.Name = "addExtension";
           this.addExtension.Size = new System.Drawing.Size(120, 23);
           this.addExtension.TabIndex = 2;
           this.addExtension.Text = "New Extension";
           this.addExtension.UseVisualStyleBackColor = true;
           this.addExtension.Click += new System.EventHandler(this.addExtension_Click);
           // 
           // newProgramAssociationButton
           // 
           this.newProgramAssociationButton.Location = new System.Drawing.Point(487, 293);
           this.newProgramAssociationButton.Name = "newProgramAssociationButton";
           this.newProgramAssociationButton.Size = new System.Drawing.Size(120, 23);
           this.newProgramAssociationButton.TabIndex = 3;
           this.newProgramAssociationButton.Text = "New Program";
           this.newProgramAssociationButton.UseVisualStyleBackColor = true;
           this.newProgramAssociationButton.Click += new System.EventHandler(this.newProgramAssociationButton_Click);
           // 
           // AssociationGUIForm
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(641, 584);
           this.Controls.Add(this.newProgramAssociationButton);
           this.Controls.Add(this.groupBox1);
           this.Controls.Add(this.programIdGroupBox);
           this.Controls.Add(this.extensionGroupBox);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           this.Name = "AssociationGUIForm";
           this.Text = "Association GUI";
           this.Load += new System.EventHandler(this.Tester_Load);
           this.extensionGroupBox.ResumeLayout(false);
           this.extensionGroupBox.PerformLayout();
           this.programIdGroupBox.ResumeLayout(false);
           this.programIdGroupBox.PerformLayout();
           this.groupBox1.ResumeLayout(false);
           this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox extensionsListBox;
        private System.Windows.Forms.TextBox contentTypeTextBox;
        private System.Windows.Forms.ListBox openWithListBox;
        private System.Windows.Forms.ComboBox PerceivedTypeComboBox;
        private System.Windows.Forms.TextBox programIdTextBox;
        private System.Windows.Forms.Button addOpenWith;
        private System.Windows.Forms.Button removeOpenWith;
        private System.Windows.Forms.CheckedListBox editFlagsListBox;
       private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TreeView verbsTreeView;
        private System.Windows.Forms.GroupBox extensionGroupBox;
        private System.Windows.Forms.GroupBox programIdGroupBox;
        private System.Windows.Forms.Label programIdLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label extensionLabel;
       private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button extensionUpdateButton;
        private System.Windows.Forms.Button refreshExtensionsButton;
        private System.Windows.Forms.Button updateProgIdButton;
        private System.Windows.Forms.Button removeVerbButton;
        private System.Windows.Forms.Button addVerbButton;
       private System.Windows.Forms.Button removeSingleVerbButton;
       private System.Windows.Forms.Button addSingleVerbButton;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.TextBox iconIndexTextBox;
       private System.Windows.Forms.TextBox iconPathTextBox;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.TextBox persistentHandlerTextBox;
       private System.Windows.Forms.CheckBox alwaysShowExtCheckBox;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.GroupBox groupBox1;
       private System.Windows.Forms.Label label10;
       private System.Windows.Forms.Label label9;
       private System.Windows.Forms.Button addExtension;
       private System.Windows.Forms.Button newProgramAssociationButton;
    }
}