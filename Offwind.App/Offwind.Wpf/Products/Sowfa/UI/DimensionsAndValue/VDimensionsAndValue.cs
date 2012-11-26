using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.DimensionsAndValue
{
    public sealed class VDimensionsAndValue : BaseViewModel
    {
        public decimal X
        {
            get { return GetProperty<decimal>("X"); }
            set { SetProperty("X", value); }
        }


        public decimal Y
        {
            get { return GetProperty<decimal>("Y"); }
            set { SetProperty("Y", value); }
        }


        public decimal Z
        {
            get { return GetProperty<decimal>("Z"); }
            set { SetProperty("Z", value); }
        }

    }
}
