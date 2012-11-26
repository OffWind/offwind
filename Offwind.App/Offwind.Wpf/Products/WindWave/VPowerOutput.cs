using Offwind.Infrastructure.Models;

namespace Offwind.Products.WindWave
{
    public sealed class VPowerOutput : BaseViewModel
    {
        public string Method
        {
            get { return GetProperty<string>("Method"); }
            set { SetProperty("Method", value); }
        }


        public double Velocity
        {
            get { return GetProperty<double>("Velocity"); }
            set { SetProperty("Velocity", value); }
        }


        public double Output
        {
            get { return GetProperty<double>("Output"); }
            set { SetProperty("Output", value); }
        }


        public double Differences
        {
            get { return GetProperty<double>("Differences"); }
            set { SetProperty("Differences", value); }
        }

    }
}
