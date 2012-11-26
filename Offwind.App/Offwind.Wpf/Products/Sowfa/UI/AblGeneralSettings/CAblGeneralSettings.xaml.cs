using System;
using System.Diagnostics;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Offwind.Sowfa.System.DecomposeParDict;

namespace Offwind.Products.Sowfa.UI.AblGeneralSettings
{
    /// <summary>
    /// Interaction logic for CAblGeneralSettings.xaml
    /// </summary>
    public partial class CAblGeneralSettings : UserControl, IProjectItemView
    {
        private readonly VGeneralSettings _model = new VGeneralSettings();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;
        private VProject _vProject;

        public CAblGeneralSettings()
        {
            InitializeComponent();
            _model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_model_PropertyChanged);
            DataContext = _model;
        }

        void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Trace.WriteLine(e.PropertyName + ": " + _model.GetProperty(e.PropertyName));
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = handler;
        }

        public Action GetSaveCommand()
        {
            return () =>
            {
                var path = _fileHandler.GetPath(_vProject.ProjectDir);
                var d = (DecomposeParDictData)_fileHandler.Read(path);
                d.numberOfSubdomains = _model.ParallelProcessors;
                //TODO: Mesh refinement
                _fileHandler.Write(path, d);
                _model.AcceptChanges();
            };
        }

        public void UpdateFromProject(VProject vProject)
        {
            _vProject = vProject;

            var path = _fileHandler.GetPath(_vProject.ProjectDir);
            var d = (DecomposeParDictData)_fileHandler.Read(path);
            _model.ParallelProcessors = d.numberOfSubdomains;
            _model.RequireMeshRefinement = true;
            _model.MeshRefinementLevel = 3;
            _model.AcceptChanges();
        }
    }
}
