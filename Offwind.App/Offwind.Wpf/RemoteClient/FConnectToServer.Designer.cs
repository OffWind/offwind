namespace Offwind.RemoteClient
{
    partial class FConnectToServer
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
            this.buttonTestConnection = new DevExpress.XtraEditors.SimpleButton();
            this.txtSettingsLogin = new DevExpress.XtraEditors.TextEdit();
            this.txtSettingsPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtSettingsServerAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsServerAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(78, 120);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(107, 23);
            this.buttonTestConnection.TabIndex = 47;
            this.buttonTestConnection.Text = "Test connection";
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // txtSettingsLogin
            // 
            this.txtSettingsLogin.Location = new System.Drawing.Point(108, 53);
            this.txtSettingsLogin.Name = "txtSettingsLogin";
            this.txtSettingsLogin.Properties.Mask.BeepOnError = true;
            this.txtSettingsLogin.Size = new System.Drawing.Size(132, 20);
            this.txtSettingsLogin.TabIndex = 43;
            // 
            // txtSettingsPassword
            // 
            this.txtSettingsPassword.Location = new System.Drawing.Point(108, 83);
            this.txtSettingsPassword.Name = "txtSettingsPassword";
            this.txtSettingsPassword.Properties.Mask.BeepOnError = true;
            this.txtSettingsPassword.Properties.PasswordChar = '*';
            this.txtSettingsPassword.Size = new System.Drawing.Size(132, 20);
            this.txtSettingsPassword.TabIndex = 41;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 45;
            this.labelControl2.Text = "Login";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 28);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(73, 13);
            this.labelControl6.TabIndex = 44;
            this.labelControl6.Text = "Server address";
            // 
            // txtSettingsServerAddress
            // 
            this.txtSettingsServerAddress.Location = new System.Drawing.Point(108, 24);
            this.txtSettingsServerAddress.Name = "txtSettingsServerAddress";
            this.txtSettingsServerAddress.Size = new System.Drawing.Size(239, 20);
            this.txtSettingsServerAddress.TabIndex = 42;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 46;
            this.labelControl3.Text = "Password";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(191, 120);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 48;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(272, 120);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 49;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FConnectToServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 155);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.txtSettingsLogin);
            this.Controls.Add(this.txtSettingsPassword);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtSettingsServerAddress);
            this.Controls.Add(this.labelControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FConnectToServer";
            this.Text = "Connect to Offwind server";
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsServerAddress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton buttonTestConnection;
        private DevExpress.XtraEditors.TextEdit txtSettingsLogin;
        private DevExpress.XtraEditors.TextEdit txtSettingsPassword;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtSettingsServerAddress;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonOK;
    }
}