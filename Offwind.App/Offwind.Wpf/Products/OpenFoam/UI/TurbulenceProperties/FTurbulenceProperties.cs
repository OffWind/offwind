using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.UI.TurbulenceProperties
{
    public partial class FTurbulenceProperties : Form, IProjectItemView
    {
        private readonly VTurbulenceProperties _model = new VTurbulenceProperties();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;

        public FTurbulenceProperties()
        {
            InitializeComponent();

            _model.SimulationType = SimulationType.LesModel;

            radioSimulationType.Properties.Items.Clear();
            radioSimulationType.Properties.Items.Add(new RadioGroupItem(SimulationType.RasModel, "RAS Model"));
            radioSimulationType.Properties.Items.Add(new RadioGroupItem(SimulationType.LesModel, "LES Model"));
            radioSimulationType.Properties.Items.Add(new RadioGroupItem(SimulationType.Laminar, "Laminar"));

            radioSimulationType.DataBindings.Add("EditValue", _model, "SimulationType", true, DataSourceUpdateMode.OnPropertyChanged);

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
