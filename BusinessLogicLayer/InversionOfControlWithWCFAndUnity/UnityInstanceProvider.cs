using Microsoft.Practices.Unity;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

internal class UnityInstanceProvider : IInstanceProvider
{
    private readonly IUnityContainer container;
    private readonly Type contractType;

    public UnityInstanceProvider(IUnityContainer container, Type contractType)
    {
        this.container = container;
        this.contractType = contractType;
    }

    public object GetInstance(InstanceContext instanceContext) => GetInstance(instanceContext, null);
    public object GetInstance(InstanceContext instanceContext, Message message) => container.Resolve(contractType);

    public void ReleaseInstance(InstanceContext instanceContext, object instance) => container.Teardown(instance);
}
