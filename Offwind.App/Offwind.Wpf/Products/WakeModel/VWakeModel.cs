using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using Offwind.Infrastructure.Models;

namespace Offwind.Products.WakeModel
{
    public class VWakeModel : BaseViewModel
    {
        public ObservableCollection<VTurbine> Turbines { get; private set; }
        public ListCollectionView VTurbines { get; private set; }

        public string SolverState
        {
            get { return GetProperty<string>("SolverState"); }
            set { SetProperty("SolverState", value); }
        }

        public string SolverOutputDir
        {
            get { return GetProperty<string>("SolverOutputDir"); }
            set { SetProperty("SolverOutputDir", value); }
        }

        public decimal GridPointsX
        {
            get { return GetProperty<decimal>("GridPointsX"); }
            set { SetProperty("GridPointsX", value); }
        }

        public decimal GridPointsY
        {
            get { return GetProperty<decimal>("GridPointsY"); }
            set { SetProperty("GridPointsY", value); }
        }

        public decimal TurbineDiameter
        {
            get { return GetProperty<decimal>("TurbineDiameter"); }
            set { SetProperty("TurbineDiameter", value); }
        }

        public decimal TurbineHeight
        {
            get { return GetProperty<decimal>("TurbineHeight"); }
            set { SetProperty("TurbineHeight", value); }
        }

        public decimal HubThrust
        {
            get { return GetProperty<decimal>("HubThrust"); }
            set { SetProperty("HubThrust", value); }
        }

        public decimal WakeDecay
        {
            get { return GetProperty<decimal>("WakeDecay"); }
            set { SetProperty("WakeDecay", value); }
        }

        public decimal VelocityAtHub
        {
            get { return GetProperty<decimal>("VelocityAtHub"); }
            set { SetProperty("VelocityAtHub", value); }
        }

        public int TurbinesAmount
        {
            get { return GetProperty<int>("TurbinesAmount"); }
            set { SetProperty("TurbinesAmount", value); }
        }

        public decimal AirDensity
        {
            get { return GetProperty<decimal>("AirDensity"); }
            set { SetProperty("AirDensity", value); }
        }

        public decimal UnknownProperty
        {
            get { return GetProperty<decimal>("UnknownProperty"); }
            set { SetProperty("UnknownProperty", value); }
        }

        public decimal RotationAngle
        {
            get { return GetProperty<decimal>("RotationAngle"); }
            set { SetProperty("RotationAngle", value); }
        }

        public VWakeModel()
        {
            Turbines = new ObservableCollection<VTurbine>();
            Turbines.CollectionChanged += (s, e) => { TurbinesAmount = Turbines.Count; };

            VTurbines = new ListCollectionView(Turbines);
        }
    }
}
