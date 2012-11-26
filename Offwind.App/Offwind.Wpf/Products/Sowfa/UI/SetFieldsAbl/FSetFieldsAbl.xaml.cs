using System;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Offwind.Sowfa.System.SetFieldsAblDict;
namespace Offwind.Products.Sowfa.UI.SetFieldsAbl
{
    /// <summary>
    /// Логика взаимодействия для FSetFieldsAbl.xaml
    /// </summary>
    public partial class FSetFieldsAbl : UserControl, IProjectItemView
    {
		private readonly VSetFieldsAblDict _model = new VSetFieldsAblDict();
        private FoamFileHandler _fileHandler;
        private VProject _vProject;

        public FSetFieldsAbl()
        {
            InitializeComponent();
	        DataContext = _model;
        }

        public Action GetSaveCommand()
        {
            return () =>
            {
                var path = _fileHandler.GetPath(_vProject.ProjectDir);
                var d = (SetFieldsAblDictData)_fileHandler.Read(path);
                d.xMax = _model.xMax;
                d.yMax = _model.yMax;
                d.zMax = _model.zMax;
                d.logInit = _model.logInit;
                d.deltaU = _model.deltaU;
                d.deltaV = _model.deltaV;
                d.Uperiods = _model.Uperiods;
                d.Vperiods = _model.Vperiods;
                d.zPeak = _model.zPeak;
                d.zInversion = _model.zInversion;
                d.widthInversion = _model.widthInversion;
                d.Tbottom = _model.Tbottom;
                d.Ttop = _model.Ttop;
                d.dTdz = _model.dTdz;
                d.Ug = _model.Ug;
                d.UgDir = _model.UgDir;
                d.z0 = _model.z0;
                d.kappa = _model.kappa;
                d.updateInternalFields = _model.updateInternalFields;
                d.updateBoundaryFields = _model.updateBoundaryFields;
                _fileHandler.Write(path, d);
                _model.AcceptChanges();
            };
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = handler;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _vProject = vProject;

            var path = _fileHandler.GetPath(_vProject.ProjectDir);
            var data = _fileHandler.Read(path);
            var d = (SetFieldsAblDictData)data;
            _model.xMax = d.xMax;
            _model.yMax = d.yMax;
            _model.zMax = d.zMax;
            _model.logInit = d.logInit;
            _model.deltaU = d.deltaU;
            _model.deltaV = d.deltaV;
            _model.Uperiods = d.Uperiods;
            _model.Vperiods = d.Vperiods;
            _model.zPeak = d.zPeak;
            _model.zInversion = d.zInversion;
            _model.widthInversion = d.widthInversion;
            _model.Tbottom = d.Tbottom;
            _model.Ttop = d.Ttop;
            _model.dTdz = d.dTdz;
            _model.Ug = d.Ug;
            _model.UgDir = d.UgDir;
            _model.z0 = d.z0;
            _model.kappa = d.kappa;
            _model.updateInternalFields = d.updateInternalFields;
            _model.updateBoundaryFields = d.updateBoundaryFields;
            _model.AcceptChanges();
        }
    }
}