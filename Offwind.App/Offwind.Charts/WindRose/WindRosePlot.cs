using System.Collections.Generic;
using System.Windows.Media;

namespace Offwind.Charts.WindRose
{
    public class WindRosePlot
    {
        public List<WindRoseAreaPoint> Points { get; set; }
        public Color LineColor { get; set; }
        public Color FillColor { get; set; }

        public WindRosePlot()
        {
            Points = new List<WindRoseAreaPoint>();
        }

        public WindRosePlot AddPoint(int direction, double value)
        {
            Points.Add(new WindRoseAreaPoint { Direction = direction, Value = value });
            return this;
        }
    }
}
