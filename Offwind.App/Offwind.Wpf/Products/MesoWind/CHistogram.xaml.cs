using System;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.MesoWind
{
    /// <summary>
    /// Interaction logic for CHistogram.xaml
    /// </summary>
    public partial class CHistogram : UserControl, IProjectItemView
    {
        private VMesoWind _model;

        public CHistogram()
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
            _model = (VMesoWind)vProject.ProjectModel;
            _model.TargetNotified += _model_TargetNotified;
            VelocityFreq.DataSource = _model.VelocityFreq;
        }

        void _model_TargetNotified(ProductTargets target)
        {
        }
    }
}
