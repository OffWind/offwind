using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Offwind.Common;

namespace Offwind.RemoteClient
{
    public partial class FRemoteClient : Form
    {
        private readonly VRemoteClient _model = new VRemoteClient();
        private VJob _currentJob;

        public FRemoteClient()
        {
            InitializeComponent();

            txtSettingsServerAddress.DataBindings.Add("EditValue", _model, "ServerAddress", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSettingsLogin.DataBindings.Add("EditValue", _model, "Login", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSettingsPassword.DataBindings.Add("EditValue", _model, "Password", true, DataSourceUpdateMode.OnPropertyChanged);

            for (int i = 0; i < 13; i++)
            {
                _model.Jobs.Add(new VJob
                                    {
                                        Number = i,
                                        ProjectName = "Project #" + i,
                                        Id = Guid.NewGuid(),
                                        ProjectId = Guid.NewGuid(),
                                        Started = DateTime.Now.AddHours(-i),
                                        Duration = DateTime.Now.AddHours(-i) - DateTime.Now,
                                        State = (JobState) (i % 4),
                                        Result = (JobResult) (i % 3)
                                    });
            }

            gridControl1.DataSource = _model.Jobs;
        }

        private void UnbindDetails()
        {
            txtLog.DataBindings.Clear();
            txtStarted.DataBindings.Clear();
            txtDuration.DataBindings.Clear();
            txtState.DataBindings.Clear();
            txtResult.DataBindings.Clear();
        }

        private void BindDetails()
        {
            if (_currentJob == null) return;
            txtLog.DataBindings.Add("EditValue", _currentJob, "Log", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStarted.DataBindings.Add("EditValue", _currentJob, "Started", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDuration.DataBindings.Add("EditValue", _currentJob, "Duration", true, DataSourceUpdateMode.OnPropertyChanged);
            txtState.DataBindings.Add("EditValue", _currentJob, "State", true, DataSourceUpdateMode.OnPropertyChanged);
            txtResult.DataBindings.Add("EditValue", _currentJob, "Result", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        
        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UnbindDetails();

            try
            {
                if (gridView1.FocusedRowHandle < 0) return;
                _currentJob = (VJob)gridControl1.Views[0].GetRow(gridView1.FocusedRowHandle);
            }
            catch (Exception)
            {
                _currentJob = null;
            }

            BindDetails();
        }
    }
}
