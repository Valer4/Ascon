namespace BusinessLogicLayer
{
    public class ConnectInfoClientService
    {
        public string Url;
        public short Port;

        public string HostAddress => $"{Url}:{Port}";

        public ConnectInfoClientService(string url, short port)
        {
            Url = url;
            Port = port;
        }
    }
}
