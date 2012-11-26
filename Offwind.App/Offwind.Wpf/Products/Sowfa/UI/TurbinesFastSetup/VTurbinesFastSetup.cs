using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.TurbinesFastSetup
{
    public class VTurbinesFastSetup : BaseViewModel
    {
        private ObservableCollection<VTurbine> _turbines = new ObservableCollection<VTurbine>();

        public ObservableCollection<VTurbine> Turbines
        {
            get { return _turbines; }
            set
            {
                if (_turbines == value) return;
                _turbines = value;
                NotifyPropertyChanged("Turbines");
            }
        }

        public decimal YawAngle
        {
            get { return GetProperty<decimal>("YawAngle"); }
            set { SetProperty("YawAngle", value); }
        }


        public int NumberOfBld
        {
            get { return GetProperty<int>("NumberOfBld"); }
            set { SetProperty("NumberOfBld", value); }
        }


        public int NumberOfBldPts
        {
            get { return GetProperty<int>("NumberOfBldPts"); }
            set { SetProperty("NumberOfBldPts", value); }
        }


        public decimal RotorDiameter
        {
            get { return GetProperty<decimal>("RotorDiameter"); }
            set { SetProperty("RotorDiameter", value); }
        }


        public decimal Epsilon
        {
            get { return GetProperty<decimal>("Epsilon"); }
            set { SetProperty("Epsilon", value); }
        }


        public decimal SmearRadius
        {
            get { return GetProperty<decimal>("SmearRadius"); }
            set { SetProperty("SmearRadius", value); }
        }


        public decimal EffectiveRadiusFactor
        {
            get { return GetProperty<decimal>("EffectiveRadiusFactor"); }
            set { SetProperty("EffectiveRadiusFactor", value); }
        }


        public decimal PointInterpType
        {
            get { return GetProperty<decimal>("PointInterpType"); }
            set { SetProperty("PointInterpType", value); }
        }

    }
}
