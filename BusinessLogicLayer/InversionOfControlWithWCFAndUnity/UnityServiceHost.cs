using Microsoft.Practices.Unity;
using System;
using System.ServiceModel;

internal class UnityServiceHost : ServiceHost
{
    private IUnityContainer unityContainer;

    internal UnityServiceHost(IUnityContainer unityContainer, Type serviceType) :
        base(serviceType) =>
            this.unityContainer = unityContainer;

    protected override void OnOpening()
    {
        base.OnOpening();

        if (Description.Behaviors.Find<UnityServiceBehavior>() == null)
            Description.Behaviors.Add(new UnityServiceBehavior(unityContainer));
    }
}
