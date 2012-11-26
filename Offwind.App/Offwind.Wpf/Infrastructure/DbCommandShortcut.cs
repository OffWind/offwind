using System;
using MySql.Data.MySqlClient;
using Offwind.Settings;

namespace Offwind.Infrastructure
{
    public sealed class DbCommandShortcut : IDisposable
    {
        private MySqlConnection _cnn;
        private MySqlCommand _cmd;
        private MySqlDataReader _rdr;

        public DbCommandShortcut()
        {
            var settings = new NonRoamingUserSettings().Read();
                            var connectionString = String.Format("server={0};uid={1};pwd={2};database={3};",
                    settings.DatabaseAddress,
                    settings.DatabaseUser,
                    settings.DatabasePassword,
                    settings.DatabaseName);

            _cnn = new MySqlConnection(connectionString);
            _cmd = new MySqlCommand();

            _cnn.Open();
            _cmd.Connection = _cnn;
        }

        public string CommandText
        {
            get { return _cmd.CommandText; }
            set { _cmd.CommandText = value; }
        }

        public MySqlParameterCollection Parameters
        {
            get { return _cmd.Parameters; }
        }

        public MySqlDataReader ExecuteReader()
        {
            _rdr = _cmd.ExecuteReader();
            return _rdr;
        }

        public int ExecuteNonQuery()
        {
            return _cmd.ExecuteNonQuery();
        }

        public long LastInsertedId
        {
            get { return _cmd.LastInsertedId; }
        }

        public void Dispose()
        {
            if (_rdr != null)
            {
                _rdr.Close();
                _rdr.Dispose();
                _rdr = null;
            }

            if (_cmd != null)
            {
                _cmd.Dispose();
                _cmd = null;
            }

            if (_cnn != null)
            {
                _cnn.Close();
                _cnn.Dispose();
                _cnn = null;
            }
        }
    }
}
