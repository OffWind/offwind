using System;
using System.ComponentModel;

namespace Offwind.WebApp.Areas.CFD.Models.Preprocessing
{
    public class VTransportProperties
    {
        [DisplayName("nu ")]
        public Decimal MolecularViscosity { get; set; }

        [DisplayName("nu0")]
        public Decimal CplcNu0 { get; set; }
        [DisplayName("nuInf")]
        public Decimal CplcNuInf { get; set; }
        [DisplayName("m")]
        public Decimal CplcM { get; set; }
        [DisplayName("n")]
        public Decimal CplcN { get; set; }

        [DisplayName("nu0")]
        public Decimal BccNu0 { get; set; }
        [DisplayName("nuInf")]
        public Decimal BccNuInf { get; set; }
        [DisplayName("m")]
        public Decimal BccM { get; set; }
        [DisplayName("n")]
        public Decimal BccN { get; set; }
    }
}