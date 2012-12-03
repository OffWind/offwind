using System;
using System.IO;

namespace WakeCode
{
    public class DataReader
    {
        //----------------------------------------------------
        //************************************************
        //  SUBROUTINE READ THE DATA !
        //------------------------------------------------
        public void Read(GeneralData generalData, string dir)
        {
            using (var fileStream = File.Open(Path.Combine(dir, "initial_data.inp"), FileMode.OpenOrCreate, FileAccess.Read))
            using (var streamReader = new StreamReader(fileStream))
            {
                generalData.GridPointsX = ReadInt(streamReader); // The number of grid points in x direction
                generalData.GridPointsY = ReadInt(streamReader); // The number of the grid points in Y direction

                generalData.TurbineDiameter = ReadDouble(streamReader);    // THE DIAMETER OF THE TURBIN
                generalData.TurbineHeight = ReadDouble(streamReader);        //  THE HEIGHT OF THE TURBINE
                generalData.TurbineThrust = ReadDouble(streamReader);       // TURBINE THRUST COEFFICIENT
                generalData.WakeDecay = ReadDouble(streamReader);    // wake expand scalar
                generalData.VelocityAtHub = ReadDouble(streamReader);     //m/s - VELOCITY AT THE HUB, WITHOUT THE INFLUENCE OF THE WIND TURBIN
                generalData.TurbinesAmount = ReadInt(streamReader);     //THE NUMBER OF THE TURBINE

                generalData.x_turb = new double[generalData.TurbinesAmount];
                generalData.y_turb = new double[generalData.TurbinesAmount];

                generalData.AirDensity = ReadDouble(streamReader);      // THE DENSITY OF THE AIR 
                generalData.PowerDistance = ReadDouble(streamReader);     // the distance behind the turbine where the power is computed
                generalData.RotationAngle = ReadDouble(streamReader);     // rotational angle of the axis: vellocity has the same direction as Ox
                ReadEmpty(streamReader);
                ReadEmpty(streamReader);
                for (var i = 0; i <= generalData.TurbinesAmount - 1; i++)
                {
                    var t = ReadXY(streamReader); // position of the turbine
                    generalData.x_turb[i] = t.Item1;
                    generalData.y_turb[i] = t.Item2;
                }
                ReadEmpty(streamReader);
            }
        }

        private static void ReadEmpty(TextReader textReader)
        {
            textReader.ReadLine();
        }

        private static int ReadInt(TextReader textReader)
        {
            int intValue;
            string line = textReader.ReadLine();
            string[] lineParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (!(lineParts.Length >= 1) || !int.TryParse(lineParts[0], out intValue))
            {
                throw new FormatException();
            }
            return intValue;
        }

        private static double ReadDouble(TextReader textReader)
        {
            double doubleValue;
            string line = textReader.ReadLine();
            string[] lineParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (!(lineParts.Length >= 1) || !double.TryParse(lineParts[0], out doubleValue))
            {
                throw new FormatException();
            }
            return doubleValue;
        }

        private static Tuple<double, double> ReadXY(TextReader textReader)
        {
            double doubleValue1;
            double doubleValue2;
            string line = textReader.ReadLine();
            string[] lineParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (!(lineParts.Length >= 2) || !double.TryParse(lineParts[0], out doubleValue1) || !double.TryParse(lineParts[1], out doubleValue2))
            {
                throw new FormatException();
            }
            return new Tuple<double, double>(doubleValue1, doubleValue2);
        }
    }
}