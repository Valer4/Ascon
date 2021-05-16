using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Print
{
    public interface IPrintClient
    {
        byte[] GetMSWord(DetailRelationEntity selectedDetail, out string message);
    }
}
