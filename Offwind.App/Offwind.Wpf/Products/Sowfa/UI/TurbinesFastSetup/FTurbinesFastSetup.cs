using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Sowfa.UI.TurbinesFastSetup
{
    public partial class FTurbinesFastSetup : Form, IProjectItemView
    {
        private readonly VTurbinesFastSetup _model = new VTurbinesFastSetup();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;
        private VTurbine _currentTurbine;

        public FTurbinesFastSetup()
        {
            InitializeComponent();

            SetSampleModel();

            txtYawAngle.DataBindings.Add("EditValue", _model, "YawAngle", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNumberOfBld.DataBindings.Add("EditValue", _model, "NumberOfBld", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNumberOfBldPts.DataBindings.Add("EditValue", _model, "NumberOfBldPts", true, DataSourceUpdateMode.OnPropertyChanged);
            txtRotorDiameter.DataBindings.Add("EditValue", _model, "RotorDiameter", true, DataSourceUpdateMode.OnPropertyChanged);
            txtEpsilon.DataBindings.Add("EditValue", _model, "Epsilon", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSmearRadius.DataBindings.Add("EditValue", _model, "SmearRadius", true, DataSourceUpdateMode.OnPropertyChanged);
            txtEffectiveRadiusFactor.DataBindings.Add("EditValue", _model, "EffectiveRadiusFactor", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPointInterpType.DataBindings.Add("EditValue", _model, "PointInterpType", true, DataSourceUpdateMode.OnPropertyChanged);

            listTurbines.DisplayMember = "Name";
            listTurbines.DataSource = _model.Turbines;
            listTurbines.SelectedIndexChanged += listTurbines_SelectedIndexChanged;
            if (listTurbines.ItemCount > 0)
            {
                listTurbines_SelectedIndexChanged(null, null);
            }

            _modelChangedIndicator = new ModelChangedIndicator(this);
            _model.ModelChanged += _modelChangedIndicator.ModelChangedHandler;
        }

        private void SetSampleModel()
        {
            _model.YawAngle = 0;
            _model.NumberOfBld = 3;
            _model.NumberOfBldPts = 62;
            _model.RotorDiameter = 126.3992m;
            _model.Epsilon = 5m;
            _model.SmearRadius = 13.15m;
            _model.EffectiveRadiusFactor = 1.21m;
            _model.PointInterpType = 1;

            _model.Turbines.Add(new VTurbine("turbine0", 200m, 0, 0, 100));
            _model.Turbines.Add(new VTurbine("turbine1", 400m, 0, 0, 100));
            _model.Turbines.Add(new VTurbine("turbine2", 600m, 0, 0, 100));
            _model.Turbines.Add(new VTurbine("turbine3", 800m, 0, 0, 100));
        }

        private void UnbindCurrentTurbine()
        {
            txtTurbineName.DataBindings.Clear();
            txtTurbineRefX.DataBindings.Clear();
            txtTurbineRefY.DataBindings.Clear();
            txtTurbineRefZ.DataBindings.Clear();
            txtTurbineHubZ.DataBindings.Clear();
        }

        private void BindCurrentTurbine()
        {
            if (_currentTurbine == null) return;
            txtTurbineName.DataBindings.Add("EditValue", _currentTurbine, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTurbineRefX.DataBindings.Add("EditValue", _currentTurbine, "RefX", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTurbineRefY.DataBindings.Add("EditValue", _currentTurbine, "RefY", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTurbineRefZ.DataBindings.Add("EditValue", _currentTurbine, "RefZ", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTurbineHubZ.DataBindings.Add("EditValue", _currentTurbine, "HubZ", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void listTurbines_SelectedIndexChanged(object sender, EventArgs e)
        {
            var turbine = listTurbines.SelectedItem as VTurbine;
            if (turbine == null) return;
            _currentTurbine = turbine;
            UnbindCurrentTurbine();
            BindCurrentTurbine();
        }

        private void buttonAddTurbine_Click(object sender, EventArgs e)
        {
            _currentTurbine = new VTurbine("NewTurbine", 0, 0, 0, 0);
            _model.Turbines.Add(_currentTurbine);
            UnbindCurrentTurbine();
            BindCurrentTurbine();
        }

        private void buttonDeleteTurbine_Click(object sender, EventArgs e)
        {
            var turbine = listTurbines.SelectedItem as VTurbine;
            if (turbine == null) return;
            _model.Turbines.Remove(turbine);
            _currentTurbine = null;
            UnbindCurrentTurbine();
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
