using System;
using System.Windows;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.WindWave.Computations;
using Offwind.Projects;

namespace Offwind.Products.WindWave
{
    /// <summary>
    /// Interaction logic for CWindWave.xaml
    /// </summary>
    public partial class CWindWave : UserControl, IProjectItemView
    {
        private VWindWave _model;

        public CWindWave()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public Action GetSaveCommand()
        {
            return null;
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
        }

        public void UpdateFromProject(VProject vProject)
        {
            _model = (VWindWave) vProject.ProjectModel;
            _model.PropertyChanged += _model_PropertyChanged;
            DataContext = _model;
            CalculateOutput();
        }

        void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Ug":
                case "Zg":
                case "Zhub":
                case "Td":
                case "Ef":
                case "Cw":
                    CalculateOutput();
                    break;
            }
        }

        private void CalculateOutput()
        {
            var calc = new Calculator();
            try
            {
                calc.Do(_model.GetInput());
                _model.PowerOutputItems.Clear();
                foreach (var po in calc.PowerOutput)
                {
                    _model.PowerOutputItems.Add(new VPowerOutput
                                                    {
                                                        Method = po.Method,
                                                        Velocity = po.Velocity,
                                                        Output = po.Output,
                                                        Differences = po.Differences
                                                    });
                }
                _model.AdvancedCfdItems.Clear();
                foreach (var po in calc.AdvancedCfdItems)
                {
                    _model.AdvancedCfdItems.Add(new VAdvancedCfd
                                                    {
                                                        Method = po.Method,
                                                        FrictionVelocity = po.FrictionVelocity,
                                                        RoughnessHeight = po.RoughnessHeight,
                                                    });
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(this, "Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
