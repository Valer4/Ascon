namespace UserInterfaceLayer.Clients.Print
{
    public interface IPrintClient
    {
        byte[] GetMSWord(long id);
    }
}
