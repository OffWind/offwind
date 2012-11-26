using System;
using System.IO;
using System.Text;
using Offwind.Infrastructure.SaveCommands;

namespace Offwind.Products.Sowfa.UI.LesProperties
{
    public sealed class CLesPropertiesSave : SaveCommand<VLesProperties>
    {
        public override void Execute()
        {
            var dir = InitBaseDir(BaseDirType.Constant);
       
            var header = new FileHeader();
            header.Location = Location.Constant;
            header.Object = "LESProperties";

            var path = Path.Combine(dir, "LESProperties");
            using (var f = new StreamWriter(path))
            {
                f.Write(header);
                WriteBody(f);
            }
        }

        private void WriteBody(StreamWriter f)
        {
            var nl = Environment.NewLine;
            var b = new StringBuilder();
            b.AppendFormat("LESModel        {0};{1}", Model.LesModel, nl);
            b.AppendFormat("delta           {0};{1}", Model.Delta, nl);
            var printCoeffs = Model.PrintCoeffsOn ? "on" : "off";
            b.AppendFormat("printCoeffs     {0};{1}", printCoeffs, nl);
            b.AppendFormat("cubeRootVolCoeffs{0}", nl);
            b.AppendFormat("{{{0}", nl);
            b.AppendFormat("    deltaCoeff           {0};{1}", Model.DeltaCoeff, nl);
            b.AppendFormat("}}{0}", nl);
            f.Write(b);
        }
    }
}
