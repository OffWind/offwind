using Offwind.Infrastructure.Models;

namespace Offwind.Products.WakeModel
{
    public class VTurbine : BaseViewModel
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

        public VTurbine()
        {
        }

        public VTurbine(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
    }
}
