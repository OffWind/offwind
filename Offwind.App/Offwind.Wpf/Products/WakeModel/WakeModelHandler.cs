using System.IO;
using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Products.WakeModel
{
    public sealed class WakeModelHandler : FoamFileHandler
    {
        public WakeModelHandler()
            : base("initial_data.inp", null, null, WakeModelRes._default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new VWakeModel();
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (VWakeModel)data;
            var t = new StringBuilder(WakeModelRes.initial_data_inp);
            t.Replace("({[[GridPointsX]]})", d.GridPointsX.ToString());
            t.Replace("({[[GridPointsY]]})", d.GridPointsY.ToString());
            t.Replace("({[[TurbineDiameter]]})", d.TurbineDiameter.ToString());
            t.Replace("({[[TurbineHeight]]})", d.TurbineHeight.ToString());
            t.Replace("({[[HubThrust]]})", d.HubThrust.ToString());
            t.Replace("({[[WakeDecay]]})", d.WakeDecay.ToString());
            t.Replace("({[[VelocityAtHub]]})", d.VelocityAtHub.ToString());
            t.Replace("({[[TurbinesAmount]]})", d.TurbinesAmount.ToString());
            t.Replace("({[[AirDensity]]})", d.AirDensity.ToString());
            t.Replace("({[[UnknownProperty]]})", d.UnknownProperty.ToString());
            t.Replace("({[[RotationAngle]]})", d.RotationAngle.ToString());

            var turbines = new StringBuilder();
            foreach (var turb in d.Turbines)
            {
                turbines.AppendFormat("{0}      {1}", turb.X, turb.Y);
                turbines.AppendLine();
            }
            turbines.AppendLine(); // This line is necessary, otherwise "wake6.exe" throws an error

            t.Replace("({[[Turbines]]})", turbines.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
