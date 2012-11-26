using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;
using Offwind.Products.OpenFoam.Models.ControlDict;

namespace Offwind.Products.OpenFoam.UI.ControlDict
{
    public sealed class VControlDict : BaseViewModel
    {
        public VControlDict()
        {
            Libs = new ObservableCollection<string>();
        }

        public ObservableCollection<string> Libs { get; private set; }

        public ApplicationSolver Application
        {
            get { return GetProperty<ApplicationSolver>("Application"); }
            set { SetPropertyEnum("Application", value); }
        }


        public StartFrom StartFrom
        {
            get { return GetProperty<StartFrom>("StartFrom"); }
            set { SetPropertyEnum("StartFrom", value); }
        }


        public decimal StartTime
        {
            get { return GetProperty<decimal>("StartTime"); }
            set { SetProperty("StartTime", value); }
        }


        public StopAt StopAt
        {
            get { return GetProperty<StopAt>("StopAt"); }
            set { SetPropertyEnum("StopAt", value); }
        }


        public decimal EndTime
        {
            get { return GetProperty<decimal>("EndTime"); }
            set { SetProperty("EndTime", value); }
        }


        public decimal DeltaT
        {
            get { return GetProperty<decimal>("DeltaT"); }
            set { SetProperty("DeltaT", value); }
        }


        public WriteControl WriteControl
        {
            get { return GetProperty<WriteControl>("WriteControl"); }
            set { SetPropertyEnum("WriteControl", value); }
        }


        public decimal WriteInterval
        {
            get { return GetProperty<decimal>("WriteInterval"); }
            set { SetProperty("WriteInterval", value); }
        }


        public decimal PurgeWrite
        {
            get { return GetProperty<decimal>("PurgeWrite"); }
            set { SetProperty("PurgeWrite", value); }
        }


        public WriteFormat WriteFormat
        {
            get { return GetProperty<WriteFormat>("WriteFormat"); }
            set { SetPropertyEnum("WriteFormat", value); }
        }


        public decimal WritePrecision
        {
            get { return GetProperty<decimal>("WritePrecision"); }
            set { SetProperty("WritePrecision", value); }
        }


        public WriteCompression WriteCompression
        {
            get { return GetProperty<WriteCompression>("WriteCompression"); }
            set { SetPropertyEnum("WriteCompression", value); }
        }


        public TimeFormat TimeFormat
        {
            get { return GetProperty<TimeFormat>("TimeFormat"); }
            set { SetPropertyEnum("TimeFormat", value); }
        }


        public decimal TimePrecision
        {
            get { return GetProperty<decimal>("TimePrecision"); }
            set { SetProperty("TimePrecision", value); }
        }


        public bool IsRunTimeModifiable
        {
            get { return GetProperty<bool>("IsRunTimeModifiable"); }
            set { SetProperty("IsRunTimeModifiable", value); }
        }


        public bool AdjustTimeStep
        {
            get { return GetProperty<bool>("AdjustTimeStep"); }
            set { SetProperty("AdjustTimeStep", value); }
        }


        public decimal MaxCo
        {
            get { return GetProperty<decimal>("MaxCo"); }
            set { SetProperty("MaxCo", value); }
        }


        public decimal MaxDeltaT
        {
            get { return GetProperty<decimal>("MaxDeltaT"); }
            set { SetProperty("MaxDeltaT", value); }
        }

    }
}
