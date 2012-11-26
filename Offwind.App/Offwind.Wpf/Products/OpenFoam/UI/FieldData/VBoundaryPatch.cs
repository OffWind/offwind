using Offwind.Infrastructure.Models;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.Products.OpenFoam.UI.FieldData
{
    public sealed class VBoundaryPatch : BaseViewModel
    {
        public string Name
        {
            get { return GetProperty<string>("Name"); }
            set { SetProperty("Name", value); }
        }


        public PatchType PatchType
        {
            get { return GetProperty<PatchType>("PatchType"); }
            set { SetPropertyEnum("PatchType", value); }
        }


        public FieldType GradientFieldType
        {
            get { return GetProperty<FieldType>("GradientFieldType"); }
            set { SetPropertyEnum("GradientFieldType", value); }
        }


        public decimal GradientValue1
        {
            get { return GetProperty<decimal>("GradientValue1"); }
            set { SetProperty("GradientValue1", value); }
        }


        public decimal GradientValue2
        {
            get { return GetProperty<decimal>("GradientValue2"); }
            set { SetProperty("GradientValue2", value); }
        }


        public decimal GradientValue3
        {
            get { return GetProperty<decimal>("GradientValue3"); }
            set { SetProperty("GradientValue3", value); }
        }


        public FieldType ValueFieldType
        {
            get { return GetProperty<FieldType>("ValueFieldType"); }
            set { SetPropertyEnum("ValueFieldType", value); }
        }


        public decimal ValueValue1
        {
            get { return GetProperty<decimal>("ValueValue1"); }
            set { SetProperty("ValueValue1", value); }
        }


        public decimal ValueValue2
        {
            get { return GetProperty<decimal>("ValueValue2"); }
            set { SetProperty("ValueValue2", value); }
        }


        public decimal ValueValue3
        {
            get { return GetProperty<decimal>("ValueValue3"); }
            set { SetProperty("ValueValue3", value); }
        }


        public string Rho
        {
            get { return GetProperty<string>("Rho"); }
            set { SetProperty("Rho", value); }
        }

    }
}
