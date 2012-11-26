using System;
using System.ComponentModel;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;
//using devDept.Eyeshot;

namespace Offwind.Products.Sowfa.UI.AblGeometry
{
    public partial class FAblGeometry : Form, IProjectItemView
    {
        private readonly VAblGeometry _model = new VAblGeometry();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        //private readonly Renderer _renderer = new Renderer();
        //private ViewportLayout _viewportLayout;
        private AblGeometryHandler _fileHandler;
        private VProject _vProject;

        public FAblGeometry()
        {
            InitializeComponent();

            panel3dView.Padding = new Padding(5);
            txtWidth.DataBindings.Add("EditValue", _model, "Width", true, DataSourceUpdateMode.OnPropertyChanged);
            txtLength.DataBindings.Add("EditValue", _model, "Length", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHeight.DataBindings.Add("EditValue", _model, "Height", true, DataSourceUpdateMode.OnPropertyChanged);

            _modelChangedIndicator = new ModelChangedIndicator(this);
            _model.ModelChanged += _modelChangedIndicator.ModelChangedHandler;
        }

        private void FGeometry_Load(object sender, EventArgs e)
        {
            //_viewportLayout = _renderer.InitViewport();
            //panel3dView.Controls.Add(_viewportLayout);

            //if (_model.Width == 0) _model.Width = 3000;
            //if (_model.Length == 0) _model.Length = 3000;
            //if (_model.Height == 0) _model.Height = 1000;
            
            //_model.PropertyChanged += _model_PropertyChanged;
            //_model_PropertyChanged(null, null);
        }

        void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //_renderer.Set(_model.Width, _model.Length, _model.Height);
            //_renderer.Render();
            //_viewportLayout.Invalidate();
        }

        public Action GetSaveCommand()
        {
            return () =>
            {
                var d = (AblGeometryData)_fileHandler.Read(_vProject.ProjectDir);
                var box = new Vertice[8];
                for (var i = 0; i < 8; i++) box[i] = new Vertice(0, 0, 0);
                box[1].X = _model.Width;
                box[2].X = _model.Width;
                box[5].X = _model.Width;
                box[6].X = _model.Width;

                box[2].Y = _model.Length;
                box[3].Y = _model.Length;
                box[6].Y = _model.Length;
                box[7].Y = _model.Length;

                box[4].Z = _model.Height;
                box[5].Z = _model.Height;
                box[6].Z = _model.Height;
                box[7].Z = _model.Height;

                d.BlockMesh.vertices.Clear();
                d.BlockMesh.vertices.AddRange(box);

                d.TopoSet.X2 = _model.Width + 0.1m;
                d.TopoSet.Y2 = _model.Length + 0.1m;
                d.TopoSet.Z2 = _model.Height + 0.1m;

                _fileHandler.Write(_vProject.ProjectDir, d);
                _model.AcceptChanges();
            };
        }
        
        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = (AblGeometryHandler)handler;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _vProject = vProject;

            var data = _fileHandler.Read(_vProject.ProjectDir);
            var d = (AblGeometryData)data;
            _model.Width = d.BlockMesh.vertices[6].X;
            _model.Length = d.BlockMesh.vertices[6].Y;
            _model.Height = d.BlockMesh.vertices[6].Z;
            _model.AcceptChanges();
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Front);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Rear);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}

        //private void simpleButton3_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Top);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}

        //private void simpleButton4_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Bottom);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}

        //private void simpleButton5_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Isometric);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}

        //private void simpleButton6_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Dimetric);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}

        //private void simpleButton7_Click(object sender, EventArgs e)
        //{
        //    _viewportLayout.SetView(viewType.Trimetric);
        //    _viewportLayout.ZoomFit();
        //    _viewportLayout.Invalidate();
        //}
    }
}
