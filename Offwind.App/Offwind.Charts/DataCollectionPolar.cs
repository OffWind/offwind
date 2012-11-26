using System;
using System.Windows;

namespace Offwind.Charts
{
    public class DataCollectionPolar : DataCollection
    {
        public void AddPolar(ChartStylePolar csp)
        {
            double xc = csp.ChartCanvas.Width / 2;
            double yc = csp.ChartCanvas.Height / 2;
            int j = 0;
            foreach (DataSeries ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                {
                    ds.SeriesName = "DataSeries" + j.ToString();
                }
                ds.AddLinePattern();
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    double r = ds.LineSeries.Points[i].Y;
                    var angle = ds.LineSeries.Points[i].X;
                    var offset = csp.AngleOffset;
                    if (csp.AngleDirection == ChartStylePolar.AngleDirectionEnum.CounterClockWise)
                    {
                        angle = -angle;
                        offset = -csp.AngleOffset;
                    }
                    double theta = (angle + offset) * Math.PI / 180;
                    double x = xc + csp.RNormalize(r) * Math.Cos(theta);
                    double y = yc + csp.RNormalize(r) * Math.Sin(theta);
                    ds.LineSeries.Points[i] = new Point(x, y);
                }
                csp.ChartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }
    }
}
