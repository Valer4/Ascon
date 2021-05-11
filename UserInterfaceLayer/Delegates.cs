namespace UserInterfaceLayer
{
    public delegate void SimpleEventHandler();

    public delegate void GenericEventHandler<T>(T parameter);

    public delegate TReturn ParamReturnDelegate<TReturn, T>(T parameter);
}
