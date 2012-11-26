using Offwind.Infrastructure.Models;

namespace Offwind.Products.WindWave
{
    public sealed class VAdvancedCfd : BaseViewModel
    {
        public string Method
        {
            get { return GetProperty<string>("Method"); }
            set { SetProperty("Method", value); }
        }


        public double FrictionVelocity
        {
            get { return GetProperty<double>("FrictionVelocity"); }
            set { SetProperty("FrictionVelocity", value); }
        }


        public double RoughnessHeight
        {
            get { return GetProperty<double>("RoughnessHeight"); }
            set { SetProperty("RoughnessHeight", value); }
        }

    }
}
