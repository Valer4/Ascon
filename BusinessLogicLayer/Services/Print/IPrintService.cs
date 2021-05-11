using System.ServiceModel;

namespace BusinessLogicLayer.Services.Print
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IPrintService
    {
        [OperationContract]
        byte[] GetMSWord(long id);
    }
}
