using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Offwind.Charts.WindRose;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.MesoWind
{
    /// <summary>
    /// Interaction logic for CWindRose.xaml
    /// </summary>
    public partial class CWindRose : UserControl, IProjectItemView
    {
        private VMesoWind _model;

        public CWindRose()
        {
            InitializeComponent();
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
        }

        public Action GetSaveCommand()
        {
            return null;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _model = (VMesoWind)vProject.ProjectModel;
            _model.TargetNotified += _model_TargetNotified;
            DataContext = _model;
        }

        void _model_TargetNotified(ProductTargets target)
        {
            PlotWindRose();
        }

        private void PlotWindRose()
        {
            if (_model.NDirs <= 0) return;
            if (_model.NBins <= 0) return;
            if (_model.FreqByDirs.Count <= 0) return;
            if (_model.FreqByBins.Count <= 0) return;

            double width = gridWindRose.ActualWidth;
            double height = gridWindRose.ActualHeight;
            double side = width;
            if (side > height)
                side = height;
            Debug.WriteLine("W: {0}, H: {1}, S: {2}", width, height, side);
            canvasWindRose.Width = side;
            canvasWindRose.Height = side;
            canvasWindRose.Children.Clear();

            var plotter = new WindRosePlotter();
            var plot = plotter.CreatePlot("W1", Colors.Gray, Colors.Red);
            for (var i = 0; i < _model.FreqByDirs.Count; i++)
            {
                var freq = _model.FreqByDirs[i];
                plot.AddPoint(i, Convert.ToDouble(freq));
            }

            var max = _model.FreqByDirs.Max();
            max = max + (max / 100) * 15;
            plotter.Plot(canvasWindRose, 0, Convert.ToDouble(max), _model.FreqByDirs.Count);
        }

        private void gridWindRose_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PlotWindRose();
        }
    }
}
