using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.AblGeometry
{
    public sealed class VAblGeometry : BaseViewModel
    {
        public decimal Width
        {
            get { return GetProperty<decimal>("Width"); }
            set { SetProperty("Width", value); }
        }


        public decimal Height
        {
            get { return GetProperty<decimal>("Height"); }
            set { SetProperty("Height", value); }
        }


        public decimal Length
        {
            get { return GetProperty<decimal>("Length"); }
            set { SetProperty("Length", value); }
        }

    }
}
