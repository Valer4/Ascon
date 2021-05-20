namespace BusinessLogicLayer.Managers.Print
{
    public interface IPrintManager
    {
        byte[] GetReportOnDetailInMSWord(long id);
    }
}
