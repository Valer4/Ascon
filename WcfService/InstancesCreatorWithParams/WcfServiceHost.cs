using BusinessLogicLayer.DiContainer;
using System;
using System.ServiceModel;

namespace WcfService.InstancesCreatorWithParams
{
	public class WcfServiceHost : ServiceHost
	{
		private IDiContainer diContainer;

		public WcfServiceHost(IDiContainer diContainer, Type serviceType) :
			base(serviceType) =>
				this.diContainer = diContainer;

		protected override void OnOpening()
		{
			base.OnOpening();

			if (null == Description.Behaviors.Find<WcfServiceBehavior>())
				Description.Behaviors.Add(new WcfServiceBehavior(diContainer));
		}
	}
}
