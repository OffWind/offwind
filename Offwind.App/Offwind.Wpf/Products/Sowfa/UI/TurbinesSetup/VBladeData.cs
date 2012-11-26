using System.ComponentModel;

namespace Offwind.Products.Sowfa.UI.TurbinesSetup
{
    public sealed class VBladeData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal _radius;

        public decimal Radius
        {
            get { return _radius; }
            set
            {
                if (_radius == value) return;
                _radius = value;
                OnPropertyChanged("Radius");
            }
        }

        private decimal _c;

        public decimal C
        {
            get { return _c; }
            set
            {
                if (_c == value) return;
                _c = value;
                OnPropertyChanged("C");
            }
        }

        private decimal _twist;

        public decimal Twist
        {
            get { return _twist; }
            set
            {
                if (_twist == value) return;
                _twist = value;
                OnPropertyChanged("Twist");
            }
        }

        private int _airfoilIndex;

        public int AirfoilIndex
        {
            get { return _airfoilIndex; }
            set
            {
                if (_airfoilIndex == value) return;
                _airfoilIndex = value;
                OnPropertyChanged("AirfoilIndex");
            }
        }
    }
}
