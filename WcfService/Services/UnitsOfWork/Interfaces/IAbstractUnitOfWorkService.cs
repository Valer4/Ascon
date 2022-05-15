using System.ServiceModel;

namespace WcfService.Services.UnitsOfWork.Interfaces
{
	[ServiceContract(SessionMode = SessionMode.Required)]
	public interface IAbstractUnitOfWorkService<TDataModel>
	{
		[OperationContract]
		TDataModel Get();
	}
}
