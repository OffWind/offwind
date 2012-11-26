using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;
using Offwind.Products.WindWave.Computations;

namespace Offwind.Products.WindWave
{
    public sealed class VWindWave : BaseViewModel
    {
        public ObservableCollection<VPowerOutput> PowerOutputItems { get; set; }
        public ObservableCollection<VAdvancedCfd> AdvancedCfdItems { get; set; }

        public double Ug
        {
            get { return GetProperty<double>("Ug"); }
            set { SetProperty("Ug", value); }
        }


        public double Zg
        {
            get { return GetProperty<double>("Zg"); }
            set { SetProperty("Zg", value); }
        }


        public double Zhub
        {
            get { return GetProperty<double>("Zhub"); }
            set { SetProperty("Zhub", value); }
        }


        public double Td
        {
            get { return GetProperty<double>("Td"); }
            set { SetProperty("Td", value); }
        }


        public double Ef
        {
            get { return GetProperty<double>("Ef"); }
            set { SetProperty("Ef", value); }
        }


        public double Cw
        {
            get { return GetProperty<double>("Cw"); }
            set { SetProperty("Cw", value); }
        }


        public VWindWave()
        {
            PowerOutputItems = new ObservableCollection<VPowerOutput>();
            AdvancedCfdItems = new ObservableCollection<VAdvancedCfd>();
        }

        public Input GetInput()
        {
            return new Input
            {
                Ug = Ug,
                Zg = Zg,
                Zhub = Zhub,
                Td = Td,
                Ef = Ef,
                Cw = Cw,
            };
        }
    }
}
