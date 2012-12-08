using System;
using System.ComponentModel;

namespace Offwind.Models
{
    public sealed class VVector : INotifyPropertyChanged, IComparable<VVector>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public int CompareTo(VVector o)
        {
            if (X == o.X && Y == o.Y && Z == o.Z) return -1;
            return 0;
        }
    }
}
