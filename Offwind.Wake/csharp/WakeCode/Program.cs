using System;
using System.IO;
using System.Reflection;

namespace WakeCode
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var dir = "";
            if (args.Length > 0)
            {
                dir = args[0];
            }
            var generalData = new GeneralData();
            var calcData = new CalcData();
            var dataReader = new DataReader();
            var dataWriter = new DataWriter();
            var calc = new WakeCalc();

            dataReader.Read(generalData, dir);

            calc.Initialize(generalData, calcData);
            calc.Run(generalData, calcData);

            dataWriter.Write(generalData, calcData, dir);
            dataWriter.WritePower(generalData, calcData, dir);
        }
    }
}
