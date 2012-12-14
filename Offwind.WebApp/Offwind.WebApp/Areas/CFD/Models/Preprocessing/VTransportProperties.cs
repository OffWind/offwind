using System;

namespace Offwind.WebApp.Areas.CFD.Models.Preprocessing
{
    public class VTransportProperties
    {
        public Decimal MolecularViscosity { get; set; }

        public Decimal CplcNu0 { get; set; }
        public Decimal CplcNuInf { get; set; }
        public Decimal CplcM { get; set; }
        public Decimal CplcN { get; set; }

        public Decimal BccNu0 { get; set; }
        public Decimal BccNuInf { get; set; }
        public Decimal BccM { get; set; }
        public Decimal BccN { get; set; }
    }
}