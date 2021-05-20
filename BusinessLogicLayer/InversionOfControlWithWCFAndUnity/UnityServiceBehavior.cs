using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

internal class UnityServiceBehavior : IServiceBehavior
{
    private readonly IUnityContainer container;

    internal UnityServiceBehavior(IUnityContainer container) => this.container = container;

    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {}

    public void AddBindingParameters(
        ServiceDescription serviceDescription,
        ServiceHostBase serviceHostBase,
        Collection<ServiceEndpoint> endpoints,
        BindingParameterCollection bindingParameters) {}

    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
        foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                if (endpointDispatcher.ContractName != "IMetadataExchange")
                {
                    string contractName = endpointDispatcher.ContractName;

                    ServiceEndpoint serviceEndpoint = serviceDescription.Endpoints.
                        FirstOrDefault(e => e.Contract.Name == contractName);

                    endpointDispatcher.DispatchRuntime.InstanceProvider =
                        new UnityInstanceProvider(container, serviceEndpoint.Contract.ContractType);
                }
    }
}
