using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.Products.OpenFoam.UI.FieldData
{
    public sealed class VFieldData : BaseViewModel
    {
        public VDimensions Dimensions { get; private set; }

        public ObservableCollection<VBoundaryPatch> Patches { get; private set; }

        public FieldType InternalFieldType
        {
            get { return GetProperty<FieldType>("InternalFieldType"); }
            set { SetPropertyEnum("InternalFieldType", value); }
        }


        public decimal InternalFieldValue1
        {
            get { return GetProperty<decimal>("InternalFieldValue1"); }
            set { SetProperty("InternalFieldValue1", value); }
        }


        public decimal InternalFieldValue2
        {
            get { return GetProperty<decimal>("InternalFieldValue2"); }
            set { SetProperty("InternalFieldValue2", value); }
        }


        public decimal InternalFieldValue3
        {
            get { return GetProperty<decimal>("InternalFieldValue3"); }
            set { SetProperty("InternalFieldValue3", value); }
        }


        public VFieldData()
        {
            Dimensions = new VDimensions();
            Patches = new ObservableCollection<VBoundaryPatch>();
        }
    }
}
