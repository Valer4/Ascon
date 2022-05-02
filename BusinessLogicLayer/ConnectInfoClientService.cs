namespace BusinessLogicLayer
{
    public class ConnectInfoClientService
    {
        public string Url;
        public ushort Port;

        public string HostAddress => $"{Url}:{Port}";

        public ConnectInfoClientService(string url, ushort port)
        {
            Url = url;
            Port = port;
        }
    }
}
