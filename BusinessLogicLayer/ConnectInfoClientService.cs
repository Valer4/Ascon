namespace BusinessLogicLayer
{
    public class ConnectInfoClientService
    {
        public string _Url;
        public short _Port;

        public string HostAddress => $"{_Url}:{_Port}";

        public ConnectInfoClientService(string url, short port)
        {
            _Url = url;
            _Port = port;
        }
    }
}
