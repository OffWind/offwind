using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.OpenFoam.Fields;
using Offwind.Projects;
using Offwind.Sowfa.Time.FieldData;

namespace Offwind.Products.Sowfa.UI.FieldData
{
    public partial class FFieldData : Form, IProjectItemView
    {
        private readonly VFieldData _model;
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private VBoundaryPatch _currentPatch;
        private FieldDataHandler _fileHandler;
        private VProject _vProject;
        
        public FFieldData()
        {
            InitializeComponent();

            _model = new VFieldData();

            InitFieldValue(comboInternalFieldType, null);
            comboInternalFieldType.DataBindings.Add("EditValue", _model, "InternalFieldType", true, DataSourceUpdateMode.OnPropertyChanged);
            txtInternalFieldValue1.DataBindings.Add("EditValue", _model, "InternalFieldValue1", true, DataSourceUpdateMode.OnPropertyChanged);
            txtInternalFieldValue2.DataBindings.Add("EditValue", _model, "InternalFieldValue2", true, DataSourceUpdateMode.OnPropertyChanged);
            txtInternalFieldValue3.DataBindings.Add("EditValue", _model, "InternalFieldValue3", true, DataSourceUpdateMode.OnPropertyChanged);
            gridControl1.DataSource = _model.Patches;

            InitPatchTypes(comboPatchType);
            InitFieldValue(comboPatchGradientType, null);
            InitFieldValue(comboPatchValueType, null);

            _modelChangedIndicator = new ModelChangedIndicator(this);
            _model.ModelChanged += _modelChangedIndicator.ModelChangedHandler;

            comboPatchGradientType.SelectedIndexChanged += comboPatchGradientType_SelectedIndexChanged;
            comboPatchValueType.SelectedIndexChanged += comboPatchValueType_SelectedIndexChanged;
        }

        void comboPatchGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentPatch == null) return;
            txtPatchGradientValue1.Enabled = _currentPatch.GradientFieldType != FieldType.undefined;
            txtPatchGradientValue2.Enabled = _currentPatch.GradientFieldType != FieldType.undefined;
            txtPatchGradientValue3.Enabled = _currentPatch.GradientFieldType != FieldType.undefined;
        }

        void comboPatchValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentPatch == null) return;
            txtPatchValueValue1.Enabled = _currentPatch.ValueFieldType != FieldType.undefined;
            txtPatchValueValue2.Enabled = _currentPatch.ValueFieldType != FieldType.undefined;
            txtPatchValueValue3.Enabled = _currentPatch.ValueFieldType != FieldType.undefined;
        }

        private void FFieldData_Load(object sender, EventArgs e)
        {
            txtDimensionsRaw.Text = _model.Dimensions.InnerValue.ToString();
            labelDimensionsFormatted.Text = _model.Dimensions.InnerValue.Formatted();
        }

        private static void InitFieldValue(ComboBoxEdit combo, TextEdit text)
        {
            combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            combo.Properties.Items.Clear();
            combo.Properties.Items.Add(FieldType.undefined);
            combo.Properties.Items.Add(FieldType.uniform);
            combo.Properties.Items.Add(FieldType.nonuniform);
            combo.SelectedIndex = 0;
        }

        private void UnbindCurrentPatch()
        {
            comboPatchType.DataBindings.Clear();
            comboPatchGradientType.DataBindings.Clear();
            comboPatchValueType.DataBindings.Clear();
            txtPatchGradientValue1.DataBindings.Clear();
            txtPatchGradientValue2.DataBindings.Clear();
            txtPatchGradientValue3.DataBindings.Clear();
            txtPatchValueValue1.DataBindings.Clear();
            txtPatchValueValue2.DataBindings.Clear();
            txtPatchValueValue3.DataBindings.Clear();
        }

        private void BindCurrentPatch()
        {
            if (_currentPatch == null) return;
            comboPatchType.DataBindings.Add("EditValue", _currentPatch, "PatchType", true, DataSourceUpdateMode.OnPropertyChanged);
            comboPatchGradientType.DataBindings.Add("EditValue", _currentPatch, "GradientFieldType", true, DataSourceUpdateMode.OnPropertyChanged);
            comboPatchValueType.DataBindings.Add("EditValue", _currentPatch, "ValueFieldType", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPatchGradientValue1.DataBindings.Add("EditValue", _currentPatch, "GradientValue1", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPatchGradientValue2.DataBindings.Add("EditValue", _currentPatch, "GradientValue2", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPatchGradientValue3.DataBindings.Add("EditValue", _currentPatch, "GradientValue3", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPatchValueValue1.DataBindings.Add("EditValue", _currentPatch, "ValueValue1", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPatchValueValue2.DataBindings.Add("EditValue", _currentPatch, "ValueValue2", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPatchValueValue3.DataBindings.Add("EditValue", _currentPatch, "ValueValue3", true, DataSourceUpdateMode.OnPropertyChanged);

            comboPatchGradientType_SelectedIndexChanged(null, null);
            comboPatchValueType_SelectedIndexChanged(null, null);
        }

        private static void InitPatchTypes(ComboBoxEdit combo)
        {
            combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            combo.Properties.Items.AddRange(EnumExtensions.GetValues(typeof (PatchType)));
            combo.SelectedIndex = 0;
        }

        public Action GetSaveCommand()
        {
            return () =>
            {
                var path = _fileHandler.GetPath(_vProject.ProjectDir);
                var d = (Offwind.Sowfa.Time.FieldData.FieldData) _fileHandler.Read(path);

                d.InternalFieldType = _model.InternalFieldType;

                if (d.FieldClass == FieldClass.volScalarField)
                {
                    d.InternalFieldValue = new decimal[1];
                    d.InternalFieldValue[0] = _model.InternalFieldValue1;
                }
                else if (d.FieldClass == FieldClass.volVectorField)
                {
                    d.InternalFieldValue = new decimal[3];
                    d.InternalFieldValue[0] = _model.InternalFieldValue1;
                    d.InternalFieldValue[1] = _model.InternalFieldValue2;
                    d.InternalFieldValue[2] = _model.InternalFieldValue3;
                }

                d.Patches.Clear();
                foreach (var vp in _model.Patches)
                {
                    var p = new BoundaryPatch();
                    p.Name = vp.Name;
                    p.PatchType = vp.PatchType;
                    p.Rho = vp.Rho;
                    p.GradientFieldType = vp.GradientFieldType;
                    p.ValueFieldType = vp.ValueFieldType;

                    if (d.FieldClass == FieldClass.volScalarField)
                    {
                        p.GradientValue = new decimal[1];
                        p.GradientValue[0] = vp.GradientValue1;

                        p.ValueValue = new decimal[1];
                        p.ValueValue[0] = vp.ValueValue1;
                    }
                    else if (d.FieldClass == FieldClass.volVectorField)
                    {
                        p.GradientValue = new decimal[3];
                        p.GradientValue[0] = vp.GradientValue1;
                        p.GradientValue[1] = vp.GradientValue2;
                        p.GradientValue[2] = vp.GradientValue3;

                        p.ValueValue = new decimal[3];
                        p.ValueValue[0] = vp.ValueValue1;
                        p.ValueValue[1] = vp.ValueValue2;
                        p.ValueValue[2] = vp.ValueValue3;
                    }

                    d.Patches.Add(p);
                }

                _fileHandler.Write(path, d);
                _model.AcceptChanges();
            };
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = (FieldDataHandler) handler;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _vProject = vProject;

            var path = _fileHandler.GetPath(_vProject.ProjectDir);
            var data = _fileHandler.Read(path);
            var d = (Offwind.Sowfa.Time.FieldData.FieldData)data;
            _model.InternalFieldType = d.InternalFieldType;

            if (d.FieldClass == FieldClass.volScalarField)
            {
                _model.InternalFieldValue1 = d.InternalFieldValue[0];
            }
            else if (d.FieldClass == FieldClass.volVectorField)
            {
                _model.InternalFieldValue1 = d.InternalFieldValue[0];
                _model.InternalFieldValue2 = d.InternalFieldValue[1];
                _model.InternalFieldValue3 = d.InternalFieldValue[2];
            }

            _model.Dimensions.Mass = d.Dimensions.Mass;
            _model.Dimensions.Length = d.Dimensions.Length;
            _model.Dimensions.Time = d.Dimensions.Time;
            _model.Dimensions.Temperature = d.Dimensions.Temperature;
            _model.Dimensions.Quantity = d.Dimensions.Quantity;
            _model.Dimensions.Current = d.Dimensions.Current;
            _model.Dimensions.LuminousIntensity = d.Dimensions.LuminousIntensity;

            _model.Patches.Clear();
            foreach (var p in d.Patches)
            {
                var vPatch = new VBoundaryPatch();
                
                vPatch.Name = p.Name;
                vPatch.PatchType = p.PatchType;

                vPatch.GradientFieldType = p.GradientFieldType;
                vPatch.ValueFieldType = p.ValueFieldType;

                if (d.FieldClass == FieldClass.volScalarField)
                {
                    vPatch.GradientValue1 = p.GradientValue[0];
                    vPatch.ValueValue1 = p.ValueValue[0];
                }
                else if (d.FieldClass == FieldClass.volVectorField)
                {
                    if (p.GradientValue.Length > 0) vPatch.GradientValue1 = p.GradientValue[0];
                    if (p.GradientValue.Length > 1) vPatch.GradientValue2 = p.GradientValue[1];
                    if (p.GradientValue.Length > 2) vPatch.GradientValue3 = p.GradientValue[2];

                    if (p.ValueValue.Length > 0) vPatch.ValueValue1 = p.ValueValue[0];
                    if (p.ValueValue.Length > 1) vPatch.ValueValue2 = p.ValueValue[1];
                    if (p.ValueValue.Length > 2) vPatch.ValueValue3 = p.ValueValue[2];
                }

                vPatch.Rho = p.Rho;

                _model.Patches.Add(vPatch);
            }

            if (d.FieldClass == FieldClass.volScalarField)
            {
                txtInternalFieldValue2.Visible = false;
                txtInternalFieldValue3.Visible = false;

                txtPatchGradientValue2.Visible = false;
                txtPatchGradientValue3.Visible = false;

                txtPatchValueValue2.Visible = false;
                txtPatchValueValue3.Visible = false;
            }

            gridView1.FocusedRowHandle = this.gridView1.GetVisibleRowHandle(0);
            _model.AcceptChanges();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            var rows = gridView1.GetSelectedRows();
            if (rows.Length == 1)
            {
                _currentPatch = (VBoundaryPatch)gridControl1.Views[0].GetRow(gridView1.FocusedRowHandle);
            }
            else
            {
                _currentPatch = (VBoundaryPatch)gridControl1.Views[0].GetRow(rows[0]);
            }
            UnbindCurrentPatch();
            BindCurrentPatch();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            gridControl1_Click(null, null);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridControl1_Click(null, null);
        }
    }
}
