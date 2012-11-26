using System;
using System.Text;

namespace Offwind.Infrastructure.SaveCommands
{
    public class FileHeader
    {
        private readonly string _newLine = Environment.NewLine;

        public string Version { get; set; }
        public string Format { get; set; }
        public string Class { get; set; }
        public Location Location { get; set; }
        public string Object { get; set; }

        public FileHeader()
        {
            Version = "2.0";
            Format = "ascii";
            Class = "dictionary";
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            b.AppendLine(@"/*--------------------------------*- C++ -*----------------------------------*\");
            b.AppendLine(@"| =========                 |                                                 |");
            b.AppendLine(@"| \\      /  F ield         | OpenFOAM: The Open Source CFD Toolbox           |");
            b.AppendLine(@"|  \\    /   O peration     | Version:  1.7.1                                 |");
            b.AppendLine(@"|   \\  /    A nd           | Web:      www.OpenFOAM.com                      |");
            b.AppendLine(@"|    \\/     M anipulation  |                                                 |");
            b.AppendLine(@"\*---------------------------------------------------------------------------*/");
            b.AppendLine(@"FoamFile");
            b.AppendLine(@"{");
            b.AppendFormat(@"    version     {0};{1}", Version, _newLine);
            b.AppendFormat(@"    format     {0};{1}", Format, _newLine);
            b.AppendFormat(@"    class     {0};{1}", Class, _newLine);
            b.AppendFormat(@"    location     ""{0}"";{1}", Location, _newLine);
            b.AppendFormat(@"    object     ""{0}"";{1}", Object, _newLine);
            b.AppendLine(@"}");
            return b.ToString();
        }
    }
}
