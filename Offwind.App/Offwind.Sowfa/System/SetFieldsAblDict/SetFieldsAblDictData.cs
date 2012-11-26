namespace Offwind.Sowfa.System.SetFieldsAblDict
{
    public sealed class SetFieldsAblDictData
    {
        public decimal xMax { get; set; }
        public decimal yMax { get; set; }
        public decimal zMax { get; set; }

        public bool logInit { get; set; }

        public decimal deltaU { get; set; }
        public decimal deltaV { get; set; }

        public decimal Uperiods { get; set; }
        public decimal Vperiods { get; set; }

        public decimal zPeak { get; set; }

        public decimal zInversion { get; set; }

        public decimal widthInversion { get; set; }

        public decimal Tbottom { get; set; }
        public decimal Ttop { get; set; }

        public decimal dTdz { get; set; }

        public decimal Ug { get; set; }

        public decimal UgDir { get; set; }

        public decimal z0 { get; set; }

        public decimal kappa { get; set; }

        public bool updateInternalFields { get; set; }

        public bool updateBoundaryFields { get; set; }
    }
}
