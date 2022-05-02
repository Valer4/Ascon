namespace BusinessLogicLayer
{
    public class ConnectInfoDataAccess
    {
        private string _url,
                       _databaseName,
                       _userName,
                       _password;
        private short? _port;

        public string ConnectionString =>
            $"Server = {_url}{(null != _port ? $",{_port}" : string.Empty)}; Database = {_databaseName}; User id = {_userName}; Password = {_password}";

        public ConnectInfoDataAccess(string url, string databaseName, string userName, string password, short? port = null)
        {
            _url = url;
            _databaseName = databaseName;
            _userName = userName;
            _password = password;
            _port = port;
        }
    }
}
