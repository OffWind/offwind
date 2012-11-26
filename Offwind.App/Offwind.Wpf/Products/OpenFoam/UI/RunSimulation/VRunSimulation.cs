using System.ComponentModel;
using Offwind.Common;
using Offwind.Infrastructure.Models;

namespace Offwind.Products.OpenFoam.UI.RunSimulation
{
    public class VRunSimulation : BaseViewModel
    {
        public bool ParallelExecution
        {
            get { return GetProperty<bool>("ParallelExecution"); }
            set { SetProperty("ParallelExecution", value); }
        }

        public int ParallelProcessorsAmount
        {
            get { return GetProperty<int>("ParallelProcessorsAmount"); }
            set { SetProperty("ParallelProcessorsAmount", value); }
        }

        public string SolverDirectory
        {
            get { return GetProperty<string>("SolverDirectory"); }
            set { SetProperty("SolverDirectory", value); }
        }

        public JobState State
        {
            get { return GetProperty<JobState>("State"); }
            set { SetPropertyEnum("State", value); }
        }

        public VRunSimulation()
        {
            PropertyChanged += VRunSimulation_PropertyChanged;
        }

        void VRunSimulation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
