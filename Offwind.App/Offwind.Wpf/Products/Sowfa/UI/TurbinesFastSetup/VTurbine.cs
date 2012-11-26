using System.ComponentModel;

namespace Offwind.Products.Sowfa.UI.TurbinesFastSetup
{
    public sealed class VTurbine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public VTurbine()
        {
        }

        public VTurbine(string name, decimal refx, decimal refy, decimal refz, decimal hubz)
        {
            _name = name;
            _refX = refx;
            _refY = refy;
            _refZ = refz;
            _hubZ = hubz;
        }

        private string _name;

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name) || string.IsNullOrEmpty(_name.Trim()))
                    return "NewTurbine";
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        private decimal _refX;

        public decimal RefX
        {
            get { return _refX; }
            set
            {
                if (_refX == value) return;
                _refX = value;
                OnPropertyChanged("RefX");
            }
        }

        private decimal _refY;

        public decimal RefY
        {
            get { return _refY; }
            set
            {
                if (_refY == value) return;
                _refY = value;
                OnPropertyChanged("RefY");
            }
        }

        private decimal _refZ;

        public decimal RefZ
        {
            get { return _refZ; }
            set
            {
                if (_refZ == value) return;
                _refZ = value;
                OnPropertyChanged("RefZ");
            }
        }

        private decimal _hubZ;

        public decimal HubZ
        {
            get { return _hubZ; }
            set
            {
                if (_hubZ == value) return;
                _hubZ = value;
                OnPropertyChanged("HubZ");
            }
        }

    }
}
