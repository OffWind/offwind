using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Sowfa.UI.DimensionsAndValue
{
    public partial class FDimensionsAndValue : Form, IProjectItemView
    {
        private readonly VDimensionsAndValue _model = new VDimensionsAndValue();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private DimensionedValue _dimensions = new DimensionedValue();
        private FoamFileHandler _fileHandler;

        public FDimensionsAndValue()
        {
            InitializeComponent();

            //txtGravitation.DataBindings.Add("EditValue", _model, "Grav/itation.Z", true, DataSourceUpdateMode.OnPropertyChanged);
            txtX.DataBindings.Add("EditValue", _model, "X", true, DataSourceUpdateMode.OnPropertyChanged);
            txtY.DataBindings.Add("EditValue", _model, "Y", true, DataSourceUpdateMode.OnPropertyChanged);
            txtZ.DataBindings.Add("EditValue", _model, "Z", true, DataSourceUpdateMode.OnPropertyChanged);

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

        public void InitValue(decimal x, decimal y, decimal z)
        {
            _model.X = x;
            _model.Y = y;
            _model.Z = z;
            _model.AcceptChanges();
        }

        public void InitDimText(string dim)
        {
            txtDimText.Text = dim;
        }

        public void InitDim(int mass,
            int length,
            int time,
            int temperature,
            int quantity,
            int current,
            int luminousIntensity)
        {
            _dimensions.Mass = mass;
            _dimensions.Length = length;
            _dimensions.Time = time;
            _dimensions.Temperature = temperature;
            _dimensions.Quantity = quantity;
            _dimensions.Current = current;
            _dimensions.LuminousIntensity = luminousIntensity;
        }
    }
}
