namespace Offwind.Products.Sowfa.UI.DimensionsAndValue
{
    partial class FDimensionsAndValue
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
            this.txtX = new DevExpress.XtraEditors.TextEdit();
            this.txtDimText = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtY = new DevExpress.XtraEditors.TextEdit();
            this.txtZ = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDimText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZ.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(188, 44);
            this.txtX.Name = "txtX";
            this.txtX.Properties.Appearance.Options.UseTextOptions = true;
            this.txtX.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtX.Properties.Mask.BeepOnError = true;
            this.txtX.Properties.Mask.EditMask = "n4";
            this.txtX.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtX.Size = new System.Drawing.Size(106, 20);
            this.txtX.TabIndex = 70;
            // 
            // txtDimText
            // 
            this.txtDimText.Location = new System.Drawing.Point(188, 12);
            this.txtDimText.Name = "txtDimText";
            this.txtDimText.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDimText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDimText.Properties.ReadOnly = true;
            this.txtDimText.Size = new System.Drawing.Size(106, 20);
            this.txtDimText.TabIndex = 69;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 47);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(26, 13);
            this.labelControl9.TabIndex = 72;
            this.labelControl9.Text = "Value";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 16);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(53, 13);
            this.labelControl10.TabIndex = 71;
            this.labelControl10.Text = "Dimensions";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(188, 70);
            this.txtY.Name = "txtY";
            this.txtY.Properties.Appearance.Options.UseTextOptions = true;
            this.txtY.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtY.Properties.Mask.BeepOnError = true;
            this.txtY.Properties.Mask.EditMask = "n4";
            this.txtY.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtY.Size = new System.Drawing.Size(106, 20);
            this.txtY.TabIndex = 70;
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(188, 96);
            this.txtZ.Name = "txtZ";
            this.txtZ.Properties.Appearance.Options.UseTextOptions = true;
            this.txtZ.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtZ.Properties.Mask.BeepOnError = true;
            this.txtZ.Properties.Mask.EditMask = "n4";
            this.txtZ.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtZ.Size = new System.Drawing.Size(106, 20);
            this.txtZ.TabIndex = 70;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(172, 48);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(6, 13);
            this.labelControl1.TabIndex = 72;
            this.labelControl1.Text = "X";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(172, 73);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(6, 13);
            this.labelControl2.TabIndex = 72;
            this.labelControl2.Text = "Y";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(172, 99);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 13);
            this.labelControl3.TabIndex = 72;
            this.labelControl3.Text = "Z";
            // 
            // FDimensionsAndValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 338);
            this.Controls.Add(this.txtZ);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtDimText);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl10);
            this.Name = "FDimensionsAndValue";
            this.Text = "Common Properties";
            ((System.ComponentModel.ISupportInitialize)(this.txtX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDimText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZ.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtX;
        private DevExpress.XtraEditors.TextEdit txtDimText;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtY;
        private DevExpress.XtraEditors.TextEdit txtZ;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}