using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.LesProperties
{
    public sealed class VLesProperties : BaseViewModel
    {
        public string LesModel
        {
            get { return GetProperty<string>("LesModel"); }
            set { SetProperty("LesModel", value); }
        }


        public string Delta
        {
            get { return GetProperty<string>("Delta"); }
            set { SetProperty("Delta", value); }
        }


        public bool PrintCoeffsOn
        {
            get { return GetProperty<bool>("PrintCoeffsOn"); }
            set { SetProperty("PrintCoeffsOn", value); }
        }


        public int DeltaCoeff
        {
            get { return GetProperty<int>("DeltaCoeff"); }
            set { SetProperty("DeltaCoeff", value); }
        }


    }
}
