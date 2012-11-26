using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;
using Offwind.Sowfa.Constant.TurbineArrayProperties;

namespace Offwind.Products.Sowfa.UI.TurbinesSetup
{
    public sealed class VTurbineArrayProperties : BaseViewModel
    {
        public OutputControl OutputControl
        {
            get { return GetProperty<OutputControl>("OutputControl"); }
            set { SetPropertyEnum("OutputControl", value); }
        }

        public decimal OutputInterval
        {
            get { return GetProperty<decimal>("OutputInterval"); }
            set { SetProperty("OutputInterval", value); }
        }

        public ObservableCollection<VTurbineArrayInstance> Turbines { get; set; }
    }
}

 