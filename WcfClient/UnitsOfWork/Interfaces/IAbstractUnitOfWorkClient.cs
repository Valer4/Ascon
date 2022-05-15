namespace WcfClient.UnitsOfWork.Interfaces
{
	internal interface IAbstractUnitOfWorkClient<TModel>
	{
		TModel Get();
	}
}
