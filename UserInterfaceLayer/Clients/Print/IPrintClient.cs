namespace UserInterfaceLayer.Clients.Print
{
    internal interface IPrintClient
    {
        byte[] GetReportOnDetailInMSWord(long id);
    }
}
