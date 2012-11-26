using System;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.ControlDict;
using Offwind.Projects;
using Offwind.Sowfa.System.ControlDict;

namespace Offwind.Products.OpenFoam.UI.ControlDict
{
    /// <summary>
    /// Interaction logic for CControlDict.xaml
    /// </summary>
    public partial class CControlDict : UserControl, IProjectItemView
    {
        private VProject _vProject;
        private FoamFileHandler _fileHandler;
        private readonly VControlDict _model = new VControlDict();

        public CControlDict()
        {
            InitializeComponent();

            comboApplication.Items.Add(ApplicationSolver.icoFoam);
            comboApplication.Items.Add(ApplicationSolver.ABLPisoSolver);
            comboApplication.Items.Add(ApplicationSolver.fastfoam);
            comboApplication.Items.Add(ApplicationSolver.windPlantPisoSolver);

            comboStartFrom.Items.Add(StartFrom.firstTime);
            comboStartFrom.Items.Add(StartFrom.startTime);
            comboStartFrom.Items.Add(StartFrom.latestTime);

            comboStopAt.Items.Add(StopAt.endTime);
            comboStopAt.Items.Add(StopAt.writeNow);
            comboStopAt.Items.Add(StopAt.noWriteNow);
            comboStopAt.Items.Add(StopAt.nextWrite);

            comboWriteControl.Items.Add(WriteControl.timeStep);
            comboWriteControl.Items.Add(WriteControl.runTime);
            comboWriteControl.Items.Add(WriteControl.adjustableRunTime);
            comboWriteControl.Items.Add(WriteControl.cpuTime);
            comboWriteControl.Items.Add(WriteControl.clockTime);

            comboWriteFormat.Items.Add(WriteFormat.ascii);
            comboWriteFormat.Items.Add(WriteFormat.binary);

            comboWriteCompression.Items.Add(WriteCompression.off);
            comboWriteCompression.Items.Add(WriteCompression.uncompressed);
            comboWriteCompression.Items.Add(WriteCompression.compressed);

            comboTimeFormat.Items.Add(TimeFormat.@fixed);
            comboTimeFormat.Items.Add(TimeFormat.scientific);
            comboTimeFormat.Items.Add(TimeFormat.general);

            _model.PropertyChanged += _model_PropertyChanged;
            DataContext = _model;
        }

        void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //var val = _model.GetProperty(e.PropertyName) ?? "<NULL>";
            //MessageBox.Show(e.PropertyName + ": " + val);
        }

        public Action GetSaveCommand()
        {
            return () =>
            {
                var path = _fileHandler.GetPath(_vProject.ProjectDir);
                var d = (ControlDictData)_fileHandler.Read(path);

                d.application = _model.Application;
                d.startFrom = _model.StartFrom;
                d.startTime = _model.StartTime;
                d.stopAt = _model.StopAt;
                d.endTime = _model.EndTime;
                d.deltaT = _model.DeltaT;
                d.writeControl = _model.WriteControl;
                d.writeInterval = _model.WriteInterval;
                d.purgeWrite = _model.PurgeWrite;
                d.writeFormat = _model.WriteFormat;
                d.writePrecision = _model.WritePrecision;
                d.writeCompression = _model.WriteCompression;
                d.timeFormat = _model.TimeFormat;
                d.timePrecision = _model.TimePrecision;
                d.runTimeModifiable = _model.IsRunTimeModifiable
                    ? FlagYesNo.yes
                    : FlagYesNo.no;
                d.adjustTimeStep = _model.AdjustTimeStep
                    ? FlagYesNo.yes
                    : FlagYesNo.no;
                d.maxCo = _model.MaxCo;
                d.maxDeltaT = _model.MaxDeltaT;

                // Functions

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
            var d = (ControlDictData)data;
            _model.Application = d.application;
            _model.StartFrom = d.startFrom;
            _model.StartTime = d.startTime;
            _model.StopAt = d.stopAt;
            _model.EndTime = d.endTime;
            _model.DeltaT = d.deltaT;
            _model.WriteControl = d.writeControl;
            _model.WriteInterval = d.writeInterval;
            _model.PurgeWrite = d.purgeWrite;
            _model.WriteFormat = d.writeFormat;
            _model.WritePrecision = d.writePrecision;
            _model.WriteCompression = d.writeCompression;
            _model.TimeFormat = d.timeFormat;
            _model.TimePrecision = d.timePrecision;
            _model.IsRunTimeModifiable = d.runTimeModifiable == FlagYesNo.yes;
            _model.AdjustTimeStep = d.adjustTimeStep == FlagYesNo.yes;
            _model.MaxCo = d.maxCo;
            _model.MaxDeltaT = d.maxDeltaT;
            _model.AcceptChanges();
        }
        
    }
}
