using System;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Offwind.Sowfa.Constant.AblProperties;

namespace Offwind.Products.Sowfa.UI.AblProperties
{
    /// <summary>
    /// Interaction logic for CABLProperties.xaml
    /// </summary>
    public partial class CABLProperties : UserControl, IProjectItemView
    {
        private readonly VABLProperties _model = new VABLProperties();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;
        private VProject _vProject;
        
        public CABLProperties()
        {
            InitializeComponent();

            DataContext = _model;
        }

        public Action GetSaveCommand()
        {
            return () =>
            {
                var path = _fileHandler.GetPath(_vProject.ProjectDir);
                var d = (AblPropertiesData)_fileHandler.Read(path);

                d.turbineArrayOn = _model.TurbineArrayOn;
                d.driveWindOn = _model.DriveWindOn;
                d.UWindSpeedDim.ScalarValue = _model.UWindSpeed;
                d.UWindDir = _model.UWindDir;
                d.HWindDim.ScalarValue = _model.HWind;
                d.alpha = _model.Alpha;
                d.lowerBoundaryName = _model.LowerBoundaryName;
                d.upperBoundaryName = _model.UpperBoundaryName;
                d.meanAvgStartTime = _model.MeanAvgStartTime;
                d.corrAvgStartTime = _model.CorrAvgStartTime;
                d.statisticsOn = _model.StatisticsOn;
                d.statisticsFrequency = _model.StatisticsFrequency;

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
            var d = (AblPropertiesData)data;

            _model.TurbineArrayOn = d.turbineArrayOn;
            _model.DriveWindOn = d.driveWindOn;
            _model.UWindSpeed = d.UWindSpeedDim.ScalarValue;
            _model.UWindDir = d.UWindDir;
            _model.HWind = d.HWindDim.ScalarValue;
            _model.Alpha = d.alpha;
            _model.LowerBoundaryName = d.lowerBoundaryName;
            _model.UpperBoundaryName = d.upperBoundaryName;
            _model.MeanAvgStartTime = d.meanAvgStartTime;
            _model.CorrAvgStartTime = d.corrAvgStartTime;

            _model.AcceptChanges();
        }
    }
}
