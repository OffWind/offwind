using System;
using System.IO;
using System.Linq;

namespace WakeCode
{
    public class DataWriter
    {
        /// <summary>
        /// SUBROUTINE  _DATA
        /// </summary>
        /// <param name="generalData"></param>
        /// <param name="generalData"></param>
        /// <param name="calcData"> </param>
        /// <param name="dir"> </param>
        public void Write(GeneralData generalData, CalcData calcData, string dir)
        {
            using (var fileStream = File.Open(Path.Combine(dir, "FLOW.xyz"), FileMode.OpenOrCreate, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                WRITE(streamWriter, generalData.GridPointsX, generalData.GridPointsY);
                for (var j = 1; j <= generalData.GridPointsY; j++)
                {
                    for (var i = 0; i <= generalData.GridPointsX - 1; i++)
                    {
                        WRITE(streamWriter, calcData.x[i]);
                    }
                }
                WRITE(streamWriter);
                for (var j = 0; j <= generalData.GridPointsY - 1; j++)
                {
                    for (var i = 1; i <= generalData.GridPointsX; i++)
                    {
                        WRITE(streamWriter, calcData.y[j]);
                    }
                }
            }

            using (var fileStream = File.Open(Path.Combine(dir, "FLOW.q"), FileMode.OpenOrCreate, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                WRITE(streamWriter, generalData.GridPointsX, generalData.GridPointsY);
                WRITE(streamWriter, "0.1   ", "  10  ", "  10000  ", "  0.1 ");
                for (var j = 1; j <= generalData.GridPointsY; j++)
                {
                    for (var i = 1; i <= generalData.GridPointsX; i++)
                    {
                        WRITE(streamWriter, generalData.AirDensity);
                    }
                }
                for (var j = 0; j <= generalData.GridPointsY - 1; j++)
                {
                    for (var i = 0; i <= generalData.GridPointsX - 1; i++)
                    {
                        WRITE(streamWriter, generalData.AirDensity * calcData.vell_i[i, j]);
                    }
                }
                for (var j = 1; j <= generalData.GridPointsY; j++)
                {
                    for (var i = 1; i <= generalData.GridPointsX; i++)
                    {
                        WRITE(streamWriter, 0);
                    }
                }
                for (var j = 1; j <= generalData.GridPointsY; j++)
                {
                    for (var i = 1; i <= generalData.GridPointsX; i++)
                    {
                        WRITE(streamWriter, 0);
                    }
                }
            }
        }

        /// <summary>
        /// SUBROUTINE  _DATA Power
        /// </summary>
        /// <param name="generalData"></param>
        /// <param name="calcData"> </param>
        /// <param name="dir"> </param>
        public void WritePower(GeneralData generalData, CalcData calcData, string dir)
        {
            using (var fileStream = File.Open(Path.Combine(dir, "Power_Output.dat"), FileMode.OpenOrCreate, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                WRITE(streamWriter, "   Turbine Number(m)   ", "Turbine Location-X(m)   ",
                      "Turbine Location-Y(m)    ", "POWER(W)");
                for (var i = 1; i <= generalData.TurbinesAmount; i++)
                {
                    WRITE(streamWriter, i, generalData.x_turb[i - 1], generalData.y_turb[i - 1],
                          calcData.WPOWER[i - 1]);
                }
            }
        }

        private void WRITE(TextWriter textWriter, params object[] values)
        {
            var n = 0;
            foreach (var value in values)
            {
                if (n > 0) textWriter.Write(" ");
                textWriter.Write(value);
                n++;
            }
            textWriter.WriteLine();
        }
    }
}