using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using UserControl = System.Windows.Controls.UserControl;

namespace Offwind.Products.WakeModel
{
    /// <summary>
    /// Interaction logic for CSolver.xaml
    /// </summary>
    public partial class CSolver : UserControl, IProjectItemView
    {
        private VWakeModel _model;

        public CSolver()
        {
            InitializeComponent();
        }

        private void bbWakeModelRun_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            var h = new WakeModelHandler();
            var path = h.GetPath(_model.SolverOutputDir);
            h.Write(path, _model);

            var p = new Process();
            var currentDir = Path.GetDirectoryName(Application.ExecutablePath);
            p.StartInfo.FileName = Path.Combine(currentDir, "exec", "wake6.exe"); ;
            p.StartInfo.Arguments = String.Format("\"{0}\"", _model.SolverOutputDir);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
        }

        private void bbWakeModelStop_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
        }

        private void bbWakeModelViewResults_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!Directory.Exists(_model.SolverOutputDir)) return;
            ShowSelectedInExplorer.FileOrFolder(_model.SolverOutputDir);
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
        }

        public Action GetSaveCommand()
        {
            return null;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _model = (VWakeModel)vProject.ProjectModel;
            _model.SolverOutputDir = Path.Combine(vProject.ProjectDir, "solver");
            if (!Directory.Exists(_model.SolverOutputDir))
                Directory.CreateDirectory(_model.SolverOutputDir);

            DataContext = _model;
        }
    }
}
