namespace UserInterfaceLayer.Forms.IViews
{
    public interface IPrintView
    {
        event ParamReturnDelegate<byte[], long> GetMSWord;
    }
}
