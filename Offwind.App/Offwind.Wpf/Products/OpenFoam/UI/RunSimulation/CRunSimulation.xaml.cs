using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.Sowfa;
using Offwind.Projects;

namespace Offwind.Products.OpenFoam.RunSimulation
{
    /// <summary>
    /// Interaction logic for CRunSimulation.xaml
    /// </summary>
    public partial class CRunSimulation : UserControl, IProjectItemView
    {
        private VSowfaNormal _projectModel;
        private VRunSimulation _model;

        public CRunSimulation()
        {
            InitializeComponent();
        }

        private void ButtonSolverDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(_model.SolverDirectory)) return;
            ShowSelectedInExplorer.FileOrFolder(_model.SolverDirectory);
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
            _projectModel = (VSowfaNormal)vProject.ProjectModel;
            _model = _projectModel.RunSimulation;
            _model.SolverDirectory = Path.Combine(vProject.ProjectDir, "solver");
            _model.PropertyChanged += _model_PropertyChanged;
            DataContext = _model;
        }

        void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private void bbSowfaRun_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void bbSowfaStop_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void bbSowfaViewResults_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }
    }
}
