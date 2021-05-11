namespace BusinessLogicLayer
{
    public class ConnectInfoDataAccess
    {
        private string _url,
                       _databaseName,
                       _login,
                       _password;
        private short? _port;

        public string ConnectionString =>
            $"Server = {_url}{(_port != null ? $",{_port}" : string.Empty)}; Database = {_databaseName}; User id = {_login}; Password = {_password}";

        public ConnectInfoDataAccess(string url, string databaseName, string login, string password, short? port = null)
        {
            _url = url;
            _databaseName = databaseName;
            _login = login;
            _password = password;
            _port = port;
        }
    }
}
