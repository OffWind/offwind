using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;

namespace Offwind.RemoteClient
{
    public sealed class VRemoteClient : BaseViewModel
    {
        private ObservableCollection<VJob> _jobs = new ObservableCollection<VJob>();

        public ObservableCollection<VJob> Jobs
        {
            get { return _jobs; }
            set
            {
                if (_jobs == value) return;
                _jobs = value;
                NotifyPropertyChanged("Jobs");
            }
        }

        public string ServerAddress
        {
            get { return GetProperty<string>("ServerAddress"); }
            set { SetProperty("ServerAddress", value); }
        }


        public string Login
        {
            get { return GetProperty<string>("Login"); }
            set { SetProperty("Login", value); }
        }


        public string Password
        {
            get { return GetProperty<string>("Password"); }
            set { SetProperty("Password", value); }
        }

    }
}
