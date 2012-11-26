using Offwind.Infrastructure.Models;

namespace Offwind.RemoteClient
{
    public sealed class VConnectToServer : BaseViewModel
    {
        public string Server
        {
            get { return GetProperty<string>("Server"); }
            set { SetProperty("Server", value); }
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


        public int Z
        {
            get { return GetProperty<int>("Z"); }
            set { SetProperty("Z", value); }
        }


    }
}
