using System;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.WakeModel
{
    /// <summary>
    /// Interaction logic for CGeneralInputProperties.xaml
    /// </summary>
    public partial class CGeneralInputProperties : UserControl, IProjectItemView
    {
        private VWakeModel _model;

        public CGeneralInputProperties()
        {
            InitializeComponent();
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
        }
    }
}
