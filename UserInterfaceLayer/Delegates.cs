namespace UserInterfaceLayer
{
    public delegate void SimpleEventHandler();

    public delegate void GenericEventHandler<T>(T parameter);

    public delegate void BoolEventHandler(bool value = true);

    public delegate TReturn ParamReturnDelegate<TReturn, T>(T parameter);
    public delegate TReturn ParamReturnDelegate<TReturn, T1, T2>(T1 parameter1, T2 parameter2);
    public delegate TReturn ParamReturnDelegate<TReturn, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3);
    public delegate TReturn ParamReturnDelegate<TReturn, T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
}
