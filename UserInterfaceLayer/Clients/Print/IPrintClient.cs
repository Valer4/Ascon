namespace UserInterfaceLayer.Clients.Print
{
    public interface IPrintClient
    {
        byte[] GetReportOnDetailInMSWord(long id);
    }
}
