using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Offwind.Charts.WindRose;

namespace Offwind.Charts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = chartGrid.ActualWidth;
            double height = chartGrid.ActualHeight;
            double side = width;
            if (width > height)
                side = height;
            chartCanvas.Width = side;
            chartCanvas.Height = side;
            chartCanvas.Children.Clear();
            //AddChart1();
            var plotter = new WindRosePlotter();
            plotter.CreatePlot("W1", Colors.Gray, Colors.Red)
                .AddPoint(0, 4)
                .AddPoint(1, 4.4)
                .AddPoint(2, 1.3)
                .AddPoint(3, 1.3)
                .AddPoint(4, 2.3)
                .AddPoint(5, 1.3)
                .AddPoint(6, 3.3)
                .AddPoint(7, 9)
                .AddPoint(8, 1.3)
                .AddPoint(9, 3)
                .AddPoint(10, 1)
                .AddPoint(11, 3);
            //plotter.CreatePlot("W2", Colors.Gray, Colors.Blue)
            //    .AddPoint(0, 5)
            //    .AddPoint(1, 6)
            //    .AddPoint(2, 2.3)
            //    .AddPoint(3, 3.3)
            //    .AddPoint(4, 4.3)
            //    .AddPoint(5, 3.3)
            //    .AddPoint(6, 5.3)
            //    .AddPoint(7, 9.5)
            //    .AddPoint(8, 2.3)
            //    .AddPoint(9, 4)
            //    .AddPoint(10, 2)
            //    .AddPoint(11, 5);

            plotter.Plot(chartCanvas, 0, 10, 12);
        }

        private void AddChart1()
        {
            var cs = new ChartStylePolar();
            var dc = new DataCollectionPolar();
            cs.ChartCanvas = chartCanvas;
            cs.Rmin = -7.0;
            cs.Rmax = 3.0;
            cs.NTicks = 4;
            cs.AngleStep = 15;
            cs.AngleOffset = -90;
            cs.AngleDirection = ChartStylePolar.AngleDirectionEnum.ClockWise;
            cs.LinePattern = ChartStylePolar.LinePatternEnum.Dot;
            cs.LineColor = Brushes.Black;
            cs.SetPolarAxes();

            var ds1 = new DataSeries();
            ds1.LineColor = Brushes.Red;
            for (int i = 0; i < 360; i++)
            {
                double theta = 1.0 * i;
                double r = Math.Log(1.001 + Math.Sin(2 * theta * Math.PI / 180));
                ds1.LineSeries.Points.Add(new Point(theta, r));
            }
            dc.DataList.Add(ds1);

            var ds2 = new DataSeries();
            ds2.LineColor = Brushes.Blue;
            for (int i = 0; i < 360; i++)
            {
                double theta = 1.0 * i;
                double r = Math.Log(1.001 + Math.Cos(2 * theta * Math.PI / 180));
                ds2.LineSeries.Points.Add(new Point(theta, r));
            }
            //dc.DataList.Add(ds2);

            var ds3 = new DataSeries();
            ds3.LineColor = Brushes.Green;
            //ds3.LineSeries.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Unspecified);
            ds3.LineSeries.Points.Add(new Point(0, -7));
            var fill = new SolidColorBrush(Colors.Red);
            fill.Opacity = .5;
            ds3.LineSeries.Fill = fill;
            //ds3.LineSeries.Points.Add(new Point(-15, 1));

            for (int i = -15; i <= 15; i++)
            {
                double theta = 1.0 * i;
                //double r = Math.Log(1.001 + Math.Cos(2 * theta * Math.PI / 180));
                ds3.LineSeries.Points.Add(new Point(theta, 1));
            }
            //ds3.LineSeries.Points.Add(new Point(15, 1));
            ds3.LineSeries.Points.Add(new Point(0, -7));
            dc.DataList.Add(ds3);

            dc.AddPolar(cs);
        }
    }
}
