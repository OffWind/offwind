using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Sowfa.UI.LesProperties
{
    public partial class FLesProperties : Form, IProjectItemView
    {
        private readonly VLesProperties _model = new VLesProperties();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;

        public FLesProperties()
        {
            InitializeComponent();

            _model.LesModel = "Smagorinsky";
            _model.Delta = "cubeRootVol";
            _model.PrintCoeffsOn = true;
            _model.DeltaCoeff = 1;

            txtLesModel.DataBindings.Add("EditValue", _model, "LesModel", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDelta.DataBindings.Add("EditValue", _model, "Delta", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDeltaCoeff.DataBindings.Add("EditValue", _model, "DeltaCoeff", true, DataSourceUpdateMode.OnPropertyChanged);
            radioPrintCoeffs.DataBindings.Add("EditValue", _model, "PrintCoeffsOn", true, DataSourceUpdateMode.OnPropertyChanged);

            _modelChangedIndicator = new ModelChangedIndicator(this);
            _model.ModelChanged += _modelChangedIndicator.ModelChangedHandler;
        }

        public Action GetSaveCommand()
        {
            return null;
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = handler;
        }

        public void UpdateFromProject(VProject vProject)
        {
        }
    }
}
