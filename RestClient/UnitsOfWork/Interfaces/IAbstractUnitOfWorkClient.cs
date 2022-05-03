namespace RestClient.UnitsOfWork.Interfaces
{
	internal interface IAbstractUnitOfWorkClient<TModel>
    {
        TModel Get();
    }
}
