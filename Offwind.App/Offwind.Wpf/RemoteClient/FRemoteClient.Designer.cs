namespace Offwind.RemoteClient
{
    partial class FRemoteClient
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcResult = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtDuration = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtResult = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtStarted = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.buttonTestConnection = new DevExpress.XtraEditors.SimpleButton();
            this.txtSettingsLogin = new DevExpress.XtraEditors.TextEdit();
            this.txtSettingsPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtSettingsServerAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStarted.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsServerAddress.Properties)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(753, 440);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Appearance.PageClient.BackColor = System.Drawing.Color.DarkRed;
            this.xtraTabPage1.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPage1.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(697, 437);
            this.xtraTabPage1.Text = "Jobs";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(3, 3);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(691, 431);
            this.splitContainerControl1.SplitterPosition = 329;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(329, 431);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcNumber,
            this.gcProjectName,
            this.gcState,
            this.gcResult});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // gcNumber
            // 
            this.gcNumber.Caption = "#";
            this.gcNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcNumber.FieldName = "Number";
            this.gcNumber.Name = "gcNumber";
            this.gcNumber.OptionsColumn.AllowEdit = false;
            this.gcNumber.OptionsColumn.ReadOnly = true;
            this.gcNumber.Visible = true;
            this.gcNumber.VisibleIndex = 0;
            // 
            // gcProjectName
            // 
            this.gcProjectName.Caption = "Project Name";
            this.gcProjectName.FieldName = "ProjectName";
            this.gcProjectName.Name = "gcProjectName";
            this.gcProjectName.OptionsColumn.AllowEdit = false;
            this.gcProjectName.OptionsColumn.ReadOnly = true;
            this.gcProjectName.Visible = true;
            this.gcProjectName.VisibleIndex = 1;
            // 
            // gcState
            // 
            this.gcState.AppearanceCell.Options.UseTextOptions = true;
            this.gcState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcState.AppearanceHeader.Options.UseTextOptions = true;
            this.gcState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcState.Caption = "State";
            this.gcState.FieldName = "State";
            this.gcState.Name = "gcState";
            this.gcState.OptionsColumn.AllowEdit = false;
            this.gcState.OptionsColumn.ReadOnly = true;
            this.gcState.Visible = true;
            this.gcState.VisibleIndex = 2;
            // 
            // gcResult
            // 
            this.gcResult.AppearanceCell.Options.UseTextOptions = true;
            this.gcResult.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcResult.AppearanceHeader.Options.UseTextOptions = true;
            this.gcResult.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcResult.Caption = "Result";
            this.gcResult.FieldName = "Result";
            this.gcResult.Name = "gcResult";
            this.gcResult.OptionsColumn.AllowEdit = false;
            this.gcResult.OptionsColumn.ReadOnly = true;
            this.gcResult.Visible = true;
            this.gcResult.VisibleIndex = 3;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtLog);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.txtDuration);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.txtResult);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.txtState);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.txtStarted);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(357, 431);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Details";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(17, 156);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(335, 268);
            this.txtLog.TabIndex = 2;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(17, 137);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(17, 13);
            this.labelControl8.TabIndex = 55;
            this.labelControl8.Text = "Log";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(98, 107);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Properties.Mask.BeepOnError = true;
            this.txtDuration.Properties.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size(116, 20);
            this.txtDuration.TabIndex = 52;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 110);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(41, 13);
            this.labelControl5.TabIndex = 55;
            this.labelControl5.Text = "Duration";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(98, 81);
            this.txtResult.Name = "txtResult";
            this.txtResult.Properties.Mask.BeepOnError = true;
            this.txtResult.Properties.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(116, 20);
            this.txtResult.TabIndex = 51;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(17, 84);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(30, 13);
            this.labelControl7.TabIndex = 56;
            this.labelControl7.Text = "Result";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(98, 55);
            this.txtState.Name = "txtState";
            this.txtState.Properties.Mask.BeepOnError = true;
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(116, 20);
            this.txtState.TabIndex = 50;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 54;
            this.labelControl4.Text = "State";
            // 
            // txtStarted
            // 
            this.txtStarted.Location = new System.Drawing.Point(98, 29);
            this.txtStarted.Name = "txtStarted";
            this.txtStarted.Properties.Mask.BeepOnError = true;
            this.txtStarted.Properties.ReadOnly = true;
            this.txtStarted.Size = new System.Drawing.Size(116, 20);
            this.txtStarted.TabIndex = 49;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 53;
            this.labelControl1.Text = "Started";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.panelControl3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(652, 437);
            this.xtraTabPage2.Text = "Settings";
            // 
            // panelControl3
            // 
            this.panelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl3.Controls.Add(this.buttonTestConnection);
            this.panelControl3.Controls.Add(this.txtSettingsLogin);
            this.panelControl3.Controls.Add(this.txtSettingsPassword);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Controls.Add(this.txtSettingsServerAddress);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Location = new System.Drawing.Point(3, 3);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(646, 431);
            this.panelControl3.TabIndex = 40;
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(15, 109);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(107, 23);
            this.buttonTestConnection.TabIndex = 40;
            this.buttonTestConnection.Text = "Test connection";
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // txtSettingsLogin
            // 
            this.txtSettingsLogin.Location = new System.Drawing.Point(111, 40);
            this.txtSettingsLogin.Name = "txtSettingsLogin";
            this.txtSettingsLogin.Properties.Mask.BeepOnError = true;
            this.txtSettingsLogin.Size = new System.Drawing.Size(132, 20);
            this.txtSettingsLogin.TabIndex = 37;
            // 
            // txtSettingsPassword
            // 
            this.txtSettingsPassword.Location = new System.Drawing.Point(111, 70);
            this.txtSettingsPassword.Name = "txtSettingsPassword";
            this.txtSettingsPassword.Properties.Mask.BeepOnError = true;
            this.txtSettingsPassword.Properties.PasswordChar = '*';
            this.txtSettingsPassword.Size = new System.Drawing.Size(132, 20);
            this.txtSettingsPassword.TabIndex = 36;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 44);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 38;
            this.labelControl2.Text = "Login";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 15);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(73, 13);
            this.labelControl6.TabIndex = 38;
            this.labelControl6.Text = "Server address";
            // 
            // txtSettingsServerAddress
            // 
            this.txtSettingsServerAddress.Location = new System.Drawing.Point(111, 11);
            this.txtSettingsServerAddress.Name = "txtSettingsServerAddress";
            this.txtSettingsServerAddress.Size = new System.Drawing.Size(313, 20);
            this.txtSettingsServerAddress.TabIndex = 37;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 39;
            this.labelControl3.Text = "Password";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.groupControl4);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(555, 437);
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl4.Location = new System.Drawing.Point(3, 3);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(162, 432);
            this.groupControl4.TabIndex = 6;
            // 
            // FRemoteClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 440);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FRemoteClient";
            this.Text = "Remote Connection";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStarted.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSettingsServerAddress.Properties)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtSettingsServerAddress;
        private DevExpress.XtraEditors.TextEdit txtSettingsPassword;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSettingsLogin;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton buttonTestConnection;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtDuration;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtResult;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtStarted;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gcNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gcProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn gcState;
        private DevExpress.XtraGrid.Columns.GridColumn gcResult;
        private DevExpress.XtraEditors.MemoEdit txtLog;
        private DevExpress.XtraEditors.LabelControl labelControl8;


    }
}