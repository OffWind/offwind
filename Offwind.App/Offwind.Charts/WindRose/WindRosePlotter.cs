using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Offwind.Charts.WindRose
{
    public class WindRosePlotter
    {
        private readonly Dictionary<string, WindRosePlot> _plots = new Dictionary<string, WindRosePlot>();

        public WindRosePlot CreatePlot(string key, Color lineColor, Color fillColor)
        {
            var plot = new WindRosePlot();
            plot.LineColor = lineColor;
            plot.FillColor = fillColor;
            _plots.Add(key, plot);
            return plot;
        }

        public void Plot(Canvas canvas, double min, double max, int directions)
        {
            var cs = new ChartStylePolar();
            cs.ChartCanvas = canvas;
            cs.Rmin = min;
            cs.Rmax = max;
            cs.NTicks = 4;
            cs.AngleStep = 360 / directions;
            cs.AngleOffset = -90;
            cs.AngleDirection = ChartStylePolar.AngleDirectionEnum.ClockWise;
            cs.LinePattern = ChartStylePolar.LinePatternEnum.Dot;
            cs.LineColor = Brushes.Black;
            cs.SetPolarAxes(true);

            var dc = new DataCollectionPolar();
            foreach (var plot in _plots.Values)
            {
                var ds = new DataSeries();
                ds.LineColor = new SolidColorBrush(plot.LineColor);
                var fill = new SolidColorBrush(plot.FillColor);
                fill.Opacity = .5;
                ds.LineSeries.Fill = fill;

                var dt = 360/directions/2;
                foreach (var sector in plot.Points)
                {
                    ds.LineSeries.Points.Add(new Point(0, cs.Rmin));
                    for (int i = 0; i <= cs.AngleStep; i++)
                    {
                        double angle = i + sector.Direction * cs.AngleStep - dt;
                        ds.LineSeries.Points.Add(new Point(angle, sector.Value));
                    }
                    ds.LineSeries.Points.Add(new Point(0, cs.Rmin));
                }
                dc.DataList.Add(ds);
            }
            dc.AddPolar(cs);
        }
    }
}
