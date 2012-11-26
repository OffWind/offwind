namespace Offwind.Products.Sowfa.UI.LesProperties
{
    partial class FLesProperties
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
            this.txtDeltaCoeff = new DevExpress.XtraEditors.TextEdit();
            this.txtDelta = new DevExpress.XtraEditors.TextEdit();
            this.txtLesModel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioPrintCoeffs = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeltaCoeff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLesModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioPrintCoeffs.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDeltaCoeff
            // 
            this.txtDeltaCoeff.Location = new System.Drawing.Point(184, 117);
            this.txtDeltaCoeff.Name = "txtDeltaCoeff";
            this.txtDeltaCoeff.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDeltaCoeff.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDeltaCoeff.Properties.Mask.BeepOnError = true;
            this.txtDeltaCoeff.Properties.Mask.EditMask = "n4";
            this.txtDeltaCoeff.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDeltaCoeff.Size = new System.Drawing.Size(47, 20);
            this.txtDeltaCoeff.TabIndex = 74;
            // 
            // txtDelta
            // 
            this.txtDelta.Location = new System.Drawing.Point(125, 41);
            this.txtDelta.Name = "txtDelta";
            this.txtDelta.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDelta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDelta.Properties.Mask.BeepOnError = true;
            this.txtDelta.Properties.Mask.EditMask = "n4";
            this.txtDelta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDelta.Size = new System.Drawing.Size(106, 20);
            this.txtDelta.TabIndex = 75;
            // 
            // txtLesModel
            // 
            this.txtLesModel.Location = new System.Drawing.Point(125, 9);
            this.txtLesModel.Name = "txtLesModel";
            this.txtLesModel.Properties.Appearance.Options.UseTextOptions = true;
            this.txtLesModel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtLesModel.Properties.Mask.BeepOnError = true;
            this.txtLesModel.Size = new System.Drawing.Size(106, 20);
            this.txtLesModel.TabIndex = 73;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 45);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 78;
            this.labelControl9.Text = "Delta";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 13);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 13);
            this.labelControl10.TabIndex = 77;
            this.labelControl10.Text = "LES Model";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 83);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 13);
            this.labelControl1.TabIndex = 78;
            this.labelControl1.Text = "PrintCoeffs";
            // 
            // radioPrintCoeffs
            // 
            this.radioPrintCoeffs.Location = new System.Drawing.Point(125, 73);
            this.radioPrintCoeffs.Name = "radioPrintCoeffs";
            this.radioPrintCoeffs.Properties.Columns = 2;
            this.radioPrintCoeffs.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "On"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Off")});
            this.radioPrintCoeffs.Size = new System.Drawing.Size(106, 32);
            this.radioPrintCoeffs.TabIndex = 79;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 120);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 78;
            this.labelControl2.Text = "DeltaCoeff";
            // 
            // FLesProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 329);
            this.Controls.Add(this.radioPrintCoeffs);
            this.Controls.Add(this.txtDeltaCoeff);
            this.Controls.Add(this.txtDelta);
            this.Controls.Add(this.txtLesModel);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl10);
            this.Name = "FLesProperties";
            this.Text = "LES Properties";
            ((System.ComponentModel.ISupportInitialize)(this.txtDeltaCoeff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLesModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioPrintCoeffs.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtDeltaCoeff;
        private DevExpress.XtraEditors.TextEdit txtDelta;
        private DevExpress.XtraEditors.TextEdit txtLesModel;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup radioPrintCoeffs;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}