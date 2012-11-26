using Offwind.Products.OpenFoam.Models;

namespace Offwind.Infrastructure.Models
{
    public sealed class VDimensions : BaseViewModel
    {
        private readonly Dimensions _dim;

        public Dimensions InnerValue { get { return _dim; } }

        public VDimensions()
        {
            _dim = new Dimensions();
            PropertyChanged += VDimensions_PropertyChanged;
        }

        void VDimensions_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _dim.Mass = Mass;
            _dim.Length = Length;
            _dim.Time = Time;
            _dim.Temperature = Temperature;
            _dim.Quantity = Quantity;
            _dim.Current = Current;
            _dim.LuminousIntensity = LuminousIntensity;
        }

        public decimal Mass
        {
            get { return GetProperty<decimal>("Mass"); }
            set { SetProperty("Mass", value); }
        }


        public decimal Length
        {
            get { return GetProperty<decimal>("Length"); }
            set { SetProperty("Length", value); }
        }


        public decimal Time
        {
            get { return GetProperty<decimal>("Time"); }
            set { SetProperty("Time", value); }
        }


        public decimal Temperature
        {
            get { return GetProperty<decimal>("Temperature"); }
            set { SetProperty("Temperature", value); }
        }


        public decimal Quantity
        {
            get { return GetProperty<decimal>("Quantity"); }
            set { SetProperty("Quantity", value); }
        }


        public decimal Current
        {
            get { return GetProperty<decimal>("Current"); }
            set { SetProperty("Current", value); }
        }


        public decimal LuminousIntensity
        {
            get { return GetProperty<decimal>("LuminousIntensity"); }
            set { SetProperty("LuminousIntensity", value); }
        }

    }
}
