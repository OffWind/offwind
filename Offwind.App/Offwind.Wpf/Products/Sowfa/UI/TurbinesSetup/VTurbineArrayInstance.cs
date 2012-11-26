using System.Collections.ObjectModel;
using System.Windows;
using Offwind.Infrastructure.Models;
using Offwind.Sowfa.Constant.TurbineArrayProperties;

namespace Offwind.Products.Sowfa.UI.TurbinesSetup
{
    public sealed class VTurbineArrayInstance : BaseViewModel
    {
        public ObservableCollection<string> TypesCopy { set; get; }

        public Visibility ShowInstance
        {
            get { return GetProperty<Visibility>("ShowInstance"); }
            set { SetPropertyEnum("ShowInstance", value); }
        }

        public string PropName
        {
            get { return GetProperty<string>("PropName"); }
            set { SetProperty("PropName", value); }            
        }

        public string TurbineType
        {
            get { return GetProperty<string>("TurbineType"); }
            set { SetProperty("TurbineType", value); }
        }

        public VVertice BaseLocation { set; get; }
        

        public decimal NumBladePoints
        {
            get { return GetProperty<decimal>("NumBladePoints"); }
            set { SetProperty("NumBladePoints", value); }
        }

        public string PointDistType
        {
            get { return GetProperty<string>("PointDistType"); }
            set { SetProperty("PointDistType", value); }
        }

        public PointInterpType PointInterpType
        {
            get { return GetProperty<PointInterpType>("PointInterpType"); }
            set { SetPropertyEnum("PointInterpType", value); }
        }

        public BladeUpdateType BladeUpdateType
        {
            get { return GetProperty<BladeUpdateType>("BladeUpdateType"); }
            set { SetPropertyEnum("BladeUpdateType", value); }
        }

        public decimal Epsilon
        {
            get { return GetProperty<decimal>("Epsilon"); }
            set { SetProperty("Epsilon", value); }
        }

        public TipRootLossCorrType TipRootLossCorrType
        {
            get { return GetProperty<TipRootLossCorrType>("TipRootLossCorrType"); }
            set { SetPropertyEnum("TipRootLossCorrType", value); }
        }

        public string RotationDir
        {
            get { return GetProperty<string>("RotationDir"); }
            set { SetProperty("RotationDir", value); }
        }

        public decimal Azimuth
        {
            get { return GetProperty<decimal>("Azimuth"); }
            set { SetProperty("Azimuth", value); }
        }

        public decimal RotSpeed
        {
            get { return GetProperty<decimal>("RotSpeed"); }
            set { SetProperty("RotSpeed", value); }
        }

        public decimal Pitch
        {
            get { return GetProperty<decimal>("Pitch"); }
            set { SetProperty("Pitch", value); }
        }

        public decimal NacYaw
        {
            get { return GetProperty<decimal>("NacYaw"); }
            set { SetProperty("NacYaw", value); }
        }

        public decimal FluidDensity
        {
            get { return GetProperty<decimal>("FluidDensity"); }
            set { SetProperty("FluidDensity", value); }
        }

        public VTurbineArrayInstance()
        {
            ShowInstance = Visibility.Visible;
            PointDistType = "uniform";
            PointInterpType = PointInterpType.linear;
            BladeUpdateType = BladeUpdateType.newPosition;
            TipRootLossCorrType = TipRootLossCorrType.none;
            RotationDir = "cw";
            TurbineType = "";
            TypesCopy = null;
            BaseLocation = new VVertice(null);
        }
    }
}
