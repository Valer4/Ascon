using Microsoft.Practices.Unity;
using System;
using System.ServiceModel;

public class UnityServiceHost : ServiceHost
{
    private IUnityContainer unityContainer;

    public UnityServiceHost(IUnityContainer unityContainer, Type serviceType) :
        base(serviceType) =>
            this.unityContainer = unityContainer;

    protected override void OnOpening()
    {
        base.OnOpening();

        if (Description.Behaviors.Find<UnityServiceBehavior>() == null)
            Description.Behaviors.Add(new UnityServiceBehavior(unityContainer));
    }
}
