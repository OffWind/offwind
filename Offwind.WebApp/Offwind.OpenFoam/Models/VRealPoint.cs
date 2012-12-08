using System.ComponentModel;

namespace Offwind.Models
{
    public class VRealPoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Preview { get { return string.Format("({0} {1} {2})", _x, _y, _z); } }

        private decimal _x;

        public decimal X
        {
            get { return _x; }
            set
            {
                if (_x == value) return;
                _x = value;
                OnPropertyChanged("X");
            }
        }

        private decimal _y;

        public decimal Y
        {
            get { return _y; }
            set
            {
                if (_y == value) return;
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        private decimal _z;

        public decimal Z
        {
            get { return _z; }
            set
            {
                if (_z == value) return;
                _z = value;
                OnPropertyChanged("Z");
            }
        }
    }
}
