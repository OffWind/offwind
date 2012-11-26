using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Offwind.RemoteClient
{
    public partial class FConnectToServer : Form
    {
        private readonly VConnectToServer _model = new VConnectToServer();

        public VConnectToServer Data
        {
            get { return _model; }
        }

        public FConnectToServer()
        {
            InitializeComponent();

            txtSettingsServerAddress.DataBindings.Add("EditValue", _model, "Server", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSettingsLogin.DataBindings.Add("EditValue", _model, "Login", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSettingsPassword.DataBindings.Add("EditValue", _model, "Password", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
