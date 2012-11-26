using System;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Offwind.Sowfa.Constant.TransportProperties;

namespace Offwind.Products.OpenFoam.UI.TransportProperties
{
    /// <summary>
    /// Interaction logic for CTransportProperties.xaml
    /// </summary>
    public partial class CTransportProperties : UserControl, IProjectItemView
    {
        private readonly VTransportProperties _model = new VTransportProperties();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;
        private VProject _vProject;
        
        public CTransportProperties()
        {
            InitializeComponent();

            DataContext = _model;
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
                var d = (TransportPropertiesData)_fileHandler.Read(path);

                d.betaM = _model.BetaM;
                d.betaSurfaceStress = _model.BetaSurfaceStress;
                d.deltaLESCoeff = _model.DeltaLESCoeff;
                d.gammM = _model.GammM;
                d.LESModel = _model.LESModel;
                d.nu = _model.MolecularViscosity;
                d.z0 = _model.RoughnessHeight;
                d.Cs = _model.SmagorinskyConstant;
                d.surfaceStressModel = _model.SurfaceStressModel;
                d.q0 = _model.SurfaceTemperatureFlux;
                d.TRef = _model.TRef;
                d.transportModel = _model.TransportModel;
                d.kappa = _model.VonKarmanConstant;
                _fileHandler.Write(path, d);
                _model.AcceptChanges();
            };
        }

        public void UpdateFromProject(VProject vProject)
        {
            _vProject = vProject;
            var path = _fileHandler.GetPath(_vProject.ProjectDir);
            var data = _fileHandler.Read(path);
            var d = (TransportPropertiesData)data;
            _model.BetaM = d.betaM;
            _model.BetaSurfaceStress = d.betaSurfaceStress;
            _model.DeltaLESCoeff = d.deltaLESCoeff;
            _model.GammM = d.gammM;
            _model.LESModel = d.LESModel;
            _model.MolecularViscosity = d.nu;
            _model.RoughnessHeight = d.z0;
            _model.SmagorinskyConstant = d.Cs;
            _model.SurfaceStressModel = d.surfaceStressModel;
            _model.SurfaceTemperatureFlux = d.q0;
            _model.TRef = d.TRef;
            _model.TransportModel = d.transportModel;
            _model.VonKarmanConstant = d.kappa;
            _model.AcceptChanges();
        }
    }
}
