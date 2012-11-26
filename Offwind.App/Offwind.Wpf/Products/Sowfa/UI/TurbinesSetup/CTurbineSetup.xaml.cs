using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Offwind.Sowfa.Constant.TurbineArrayProperties;


namespace Offwind.Products.Sowfa.UI.TurbinesSetup
{
    public partial class CTurbineSetup : UserControl, IProjectItemView
    {
        private VProject _vProject;
        private FoamFileHandler _fileHandler;

        private readonly ObservableCollection<string> turbineNameArray = new ObservableCollection<string>(){"none"};
        private readonly VTurbineArrayProperties viewPropArray = new VTurbineArrayProperties();
        private readonly VTurbineProperties viewTurbineType = new VTurbineProperties();

        private int _propNameChanged = 0;


        public CTurbineSetup()
        {
            InitializeComponent();

            comboOutputControl.Items.Add(OutputControl.runTime);
            comboOutputControl.Items.Add(OutputControl.timeStep);

            viewPropArray.Turbines = new ObservableCollection<VTurbineArrayInstance>();                        
            viewPropArray.Turbines.Add( new VTurbineArrayInstance()
                                    {
                                        PropName = "+",
                                        ShowInstance = Visibility.Collapsed
                                    } );

            viewTurbineType.TurbineTypes = new ObservableCollection<VTurbineType>();
            viewTurbineType.TurbineTypes.Add(new VTurbineType(null, null, false)
                                            {
                                                PropName = "+",
                                                ShowInstance = Visibility.Collapsed
                                            });

            TurbineArrayProperties.DataContext = viewPropArray;
            TurbineProp1.DataContext = viewTurbineType;
        }

        public Action GetSaveCommand()
        {
            return () =>
                       {
                           foreach (var x in viewTurbineType.TurbineTypes)
                           {
                               if (x.ShowInstance != Visibility.Collapsed)
                               {
                                   x.Save(_vProject.ProjectDir);
                               }
                           }
                           var p = new TurbineArrayPropData()
                                       {
                                           outputControl = viewPropArray.OutputControl,
                                           outputInterval = viewPropArray.OutputInterval
                                       };
                           foreach (var item in viewPropArray.Turbines)
                           {
                               if (item.ShowInstance != Visibility.Collapsed)
                               {
                                   p.turbine.Add(new TurbineInstance()
                                                     {
                                                         azimuth = item.Azimuth,
                                                         baseLocation = new Vertice(item.BaseLocation.X,
                                                                                    item.BaseLocation.Y,
                                                                                    item.BaseLocation.Z),
                                                         turbineType = item.TurbineType,
                                                         bladeUpdateType = item.BladeUpdateType,
                                                         epsilon = item.Epsilon,
                                                         fluidDensity = item.FluidDensity,
                                                         nacYaw = item.NacYaw,
                                                         numBladePoints = item.NumBladePoints,
                                                         pitch = item.Pitch,
                                                         pointDistType = item.PointDistType,
                                                         pointInterpType = item.PointInterpType,
                                                         rotSpeed = item.RotSpeed,
                                                         rotationDir = item.RotationDir,
                                                         tipRootLossCorrType = item.TipRootLossCorrType
                                                     });
                               }
                           }
                           var path = _fileHandler.GetPath(_vProject.ProjectDir);
                           _fileHandler.Write(path, p);
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
            var d = (TurbineArrayPropData) _fileHandler.Read(path);            

            if (d == null) return;
            viewPropArray.OutputControl = d.outputControl;
            viewPropArray.OutputInterval = d.outputInterval;

            foreach (var x in d.turbine)
            {
                if (x.turbineType != "")
                {
                    turbineNameArray.Add(x.turbineType);
                    viewTurbineType.TurbineTypes.Insert( viewTurbineType.TurbineTypes.Count - 1,
                                                         new VTurbineType(x.turbineType, _vProject.ProjectDir, true));
                }
                viewPropArray.Turbines.Insert( viewPropArray.Turbines.Count - 1,
                                               new VTurbineArrayInstance()
                                               {
                                                    Azimuth = x.azimuth,
                                                    BaseLocation = new VVertice(x.baseLocation),
                                                    BladeUpdateType = x.bladeUpdateType,
                                                    Epsilon = x.epsilon,
                                                    FluidDensity = x.fluidDensity,
                                                    NacYaw = x.nacYaw,
                                                    NumBladePoints = x.numBladePoints,
                                                    Pitch = x.pitch,
                                                    PointInterpType = x.pointInterpType,
                                                    PointDistType = x.pointDistType,
                                                    RotSpeed = x.rotSpeed,
                                                    RotationDir = x.rotationDir,
                                                    TipRootLossCorrType = x.tipRootLossCorrType,
                                                    TypesCopy = turbineNameArray, // TODO
                                                    TurbineType = x.turbineType,
                                                    PropName = "turbine" + viewPropArray.Turbines.Count,
                                               });
            }
            viewPropArray.AcceptChanges();
        }
            
        private void TurbinesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tab = (TabControl) sender;

            var item = (VTurbineArrayInstance) tab.SelectedItem;
            if (item == null) return;
            if (item.ShowInstance == Visibility.Collapsed)
            {
                var turbine = new VTurbineArrayInstance()
                {
                    PropName = "turbine" + viewPropArray.Turbines.Count,
                    TurbineType = "none",
                    TypesCopy = turbineNameArray // TODO
                    
                };
                viewPropArray.Turbines.Insert(viewPropArray.Turbines.Count - 1, turbine);
                TurbinesList.SelectedItem = turbine;
                viewPropArray.AcceptChanges();
            }
        }

        private void TurbinesType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tab = (TabControl)sender;

            var item = (VTurbineType)tab.SelectedItem;
            if (item == null) return;
            if (item.ShowInstance == Visibility.Collapsed)
            {
                var newPropName = "NewPropSet" + tab.Items.Count;
                turbineNameArray.Add(newPropName);
                var prop = new VTurbineType(newPropName, _vProject.ProjectDir, false);
                viewTurbineType.TurbineTypes.Insert(viewTurbineType.TurbineTypes.Count - 1, prop);
                TurbineTypeList.SelectedItem = prop;
                UpdateTurbinePropArray();
            }
        }

        private void UpdateTurbinePropArray()
        {
            foreach (var x in viewPropArray.Turbines)
            {
                if (x.ShowInstance == Visibility.Visible)
                {
                    x.TypesCopy = turbineNameArray; // Looks very bad !!! TODO
                }
            }
        }

        private void BladesNumber_Changed(object sender, RoutedEventArgs e)
        {
            var item = (VTurbineType) TurbineTypeList.SelectedItem;
            if (item == null) return;
            item.Update_TipPicture();
        }

        private void TurbinePropName_Changed(object sender, TextChangedEventArgs e)
        {
            _propNameChanged++; // Hmm...
        }

        private void TurbinePropName_LostFocus(object sender, RoutedEventArgs e)
        {
            var item = (VTurbineType)TurbineTypeList.SelectedItem;
            if (item == null || _propNameChanged == 0) return;

            _propNameChanged = 0;

            if (TurbineTypeList.SelectedIndex < turbineNameArray.Count())
            {
                turbineNameArray[TurbineTypeList.SelectedIndex + 1] = item.PropName;
                UpdateTurbinePropArray(); // TODO
            }

        }
    }
}
