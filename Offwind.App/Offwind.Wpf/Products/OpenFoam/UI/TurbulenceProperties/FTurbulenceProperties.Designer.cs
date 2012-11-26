namespace Offwind.UI.TurbulenceProperties
{
    partial class FTurbulenceProperties
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
            this.radioSimulationType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.radioSimulationType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radioSimulationType
            // 
            this.radioSimulationType.Location = new System.Drawing.Point(222, 12);
            this.radioSimulationType.Name = "radioSimulationType";
            this.radioSimulationType.Size = new System.Drawing.Size(122, 75);
            this.radioSimulationType.TabIndex = 104;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(12, 12);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(75, 13);
            this.labelControl13.TabIndex = 105;
            this.labelControl13.Text = "Simulation Type";
            // 
            // FTurbulenceProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 474);
            this.Controls.Add(this.radioSimulationType);
            this.Controls.Add(this.labelControl13);
            this.Name = "FTurbulenceProperties";
            this.Text = "Turbulence Properties";
            ((System.ComponentModel.ISupportInitialize)(this.radioSimulationType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioSimulationType;
        private DevExpress.XtraEditors.LabelControl labelControl13;
    }
}