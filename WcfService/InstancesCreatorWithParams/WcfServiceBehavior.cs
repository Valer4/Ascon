using BusinessLogicLayer.DiContainer;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfService.InstancesCreatorWithParams
{
	internal class WcfServiceBehavior : IServiceBehavior
	{
		private readonly IDiContainer _diContainer;

		internal WcfServiceBehavior(IDiContainer diContainer) => _diContainer = diContainer;

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
							new WcfInstanceProvider(_diContainer, serviceEndpoint.Contract.ContractType);
					}
		}
	}
}
