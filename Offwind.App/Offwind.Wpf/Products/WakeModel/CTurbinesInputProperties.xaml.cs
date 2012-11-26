using System;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Bars;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.WakeModel
{
    /// <summary>
    /// Interaction logic for CTurbinesInputProperties.xaml
    /// </summary>
    public partial class CTurbinesInputProperties : UserControl, IProjectItemView
    {
        private VWakeModel _model;

        public CTurbinesInputProperties()
        {
            InitializeComponent();
        }

        private void bbWakeModelAddTurbine_ItemClick(object sender, ItemClickEventArgs e)
        {
            _model.Turbines.Add(new VTurbine());
        }

        private void bbWakeModelDeleteTurbine_ItemClick(object sender, ItemClickEventArgs e)
        {
            var row = (VTurbine)gridDatabase.GetFocusedRow();
            _model.Turbines.Remove(row);
        }

        private void bbWakeModelEditTurbine_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void gridDatabase_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void gridDatabase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
            DataContext = _model;
            gridDatabase.ItemsSource = _model.VTurbines;
        }
    }
}
