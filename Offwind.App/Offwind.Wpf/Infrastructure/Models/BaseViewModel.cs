using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Offwind.Infrastructure.Models
{
    public delegate void ModelChangedEventHandler(BaseViewModel sender);

    public abstract class BaseViewModel : INotifyPropertyChanged, IChangeTracking
    {
        public event ModelChangedEventHandler ModelChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isChanged;
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            if (ModelChanged != null)
            {
                ModelChanged(this);
            }
        }

        public object GetProperty(string name)
        {
            if (!_values.ContainsKey(name))
                return null;

            return _values[name];
        }

        protected T GetProperty<T>(string name)
        {
            if (!_values.ContainsKey(name))
            {
                _values[name] = default(T);
            }
            return (T) _values[name];
        }

        protected void SetProperty<T>(string name, T newValue)
            where T : IComparable<T>
        {
            if (!_values.ContainsKey(name) || _values[name] == null)
            {
                _values[name] = newValue;
                NotifyPropertyChanged(name);
                return;
            }
            var oldValue = (T)_values[name];
            if (oldValue.CompareTo(newValue) != 0)
            {
                _values[name] = newValue;
                _isChanged = true;
                NotifyPropertyChanged(name);
            }
        }

        // For enum types
        protected void SetPropertyEnum<T>(string name, T newValue)
            where T : IComparable
        {
            if (!_values.ContainsKey(name) || _values[name] == null)
            {
                _values[name] = newValue;
                NotifyPropertyChanged(name);
                return;
            }
            var oldValue = (T)_values[name];
            if (oldValue.CompareTo(newValue) != 0)
            {
                _values[name] = newValue;
                _isChanged = true;
                NotifyPropertyChanged(name);
            }
        }

        public virtual void AcceptChanges()
        {
            _isChanged = false;

            if (ModelChanged != null)
            {
                ModelChanged(this);
            }
        }

        public virtual bool IsChanged
        {
            get { return _isChanged; }
        }
    }
}
