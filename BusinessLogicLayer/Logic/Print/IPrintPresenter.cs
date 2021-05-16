using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace BusinessLogicLayer.Logic.Print
{
    public interface IPrintPresenter
    {
        byte[] GetMSWord(DetailRelationEntity selectedDetail, out string warningMessage);
    }
}
