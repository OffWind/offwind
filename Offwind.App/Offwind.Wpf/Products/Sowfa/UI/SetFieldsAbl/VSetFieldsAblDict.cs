using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.SetFieldsAbl
{
    public sealed class VSetFieldsAblDict : BaseViewModel
    {
        public decimal xMax
        {
            get { return GetProperty<decimal>("xMax"); }
            set { SetProperty("xMax", value); }
        }


        public decimal yMax
        {
            get { return GetProperty<decimal>("yMax"); }
            set { SetProperty("yMax", value); }
        }


        public decimal zMax
        {
            get { return GetProperty<decimal>("zMax"); }
            set { SetProperty("zMax", value); }
        }


        public bool logInit
        {
            get { return GetProperty<bool>("logInit"); }
            set { SetProperty("logInit", value); }
        }


        public decimal deltaU
        {
            get { return GetProperty<decimal>("deltaU"); }
            set { SetProperty("deltaU", value); }
        }


        public decimal deltaV
        {
            get { return GetProperty<decimal>("deltaV"); }
            set { SetProperty("deltaV", value); }
        }


        public decimal Uperiods
        {
            get { return GetProperty<decimal>("Uperiods"); }
            set { SetProperty("Uperiods", value); }
        }


        public decimal Vperiods
        {
            get { return GetProperty<decimal>("Vperiods"); }
            set { SetProperty("Vperiods", value); }
        }


        public decimal zPeak
        {
            get { return GetProperty<decimal>("zPeak"); }
            set { SetProperty("zPeak", value); }
        }


        public decimal zInversion
        {
            get { return GetProperty<decimal>("zInversion"); }
            set { SetProperty("zInversion", value); }
        }


        public decimal widthInversion
        {
            get { return GetProperty<decimal>("widthInversion"); }
            set { SetProperty("widthInversion", value); }
        }


        public decimal Tbottom
        {
            get { return GetProperty<decimal>("Tbottom"); }
            set { SetProperty("Tbottom", value); }
        }


        public decimal Ttop
        {
            get { return GetProperty<decimal>("Ttop"); }
            set { SetProperty("Ttop", value); }
        }


        public decimal dTdz
        {
            get { return GetProperty<decimal>("dTdz"); }
            set { SetProperty("dTdz", value); }
        }


        public decimal Ug
        {
            get { return GetProperty<decimal>("Ug"); }
            set { SetProperty("Ug", value); }
        }


        public decimal UgDir
        {
            get { return GetProperty<decimal>("UgDir"); }
            set { SetProperty("UgDir", value); }
        }


        public decimal z0
        {
            get { return GetProperty<decimal>("z0"); }
            set { SetProperty("z0", value); }
        }


        public decimal kappa
        {
            get { return GetProperty<decimal>("kappa"); }
            set { SetProperty("kappa", value); }
        }


        public bool updateInternalFields
        {
            get { return GetProperty<bool>("updateInternalFields"); }
            set { SetProperty("updateInternalFields", value); }
        }


        public bool updateBoundaryFields
        {
            get { return GetProperty<bool>("updateBoundaryFields"); }
            set { SetProperty("updateBoundaryFields", value); }
        }

    }
}
