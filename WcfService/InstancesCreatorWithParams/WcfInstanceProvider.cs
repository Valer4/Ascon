using BusinessLogicLayer.DiContainer;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfService.InstancesCreatorWithParams
{
	internal class WcfInstanceProvider : IInstanceProvider
	{
		private readonly IDiContainer _diContainer;
		private readonly Type _contractType;

		public WcfInstanceProvider(IDiContainer diContainer, Type contractType)
		{
			_diContainer = diContainer;
			_contractType = contractType;
		}

		public object GetInstance(InstanceContext instanceContext) => GetInstance(instanceContext, null);
		public object GetInstance(InstanceContext instanceContext, Message message) => _diContainer.Resolve(_contractType);

		public void ReleaseInstance(InstanceContext instanceContext, object instance) => (instance as IDisposable)?.Dispose();
	}
}
