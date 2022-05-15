using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using WcfService;

namespace WcfClient
{
	public class ChannelsManager
	{
		private readonly WcfClientConfigurator _wcfClientConfigurator;

		public ChannelsManager(WcfClientConfigurator wcfClientConfigurator)
		{
			_wcfClientConfigurator = wcfClientConfigurator;
		}

		public T GetChannel<T>()
		{
			if ( ! _wcfClientConfigurator.HostNames.TryGetValue(typeof(T), out string hostName))
				throw new ArgumentException("Имя хоста не найдено.");

			NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message);
			binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

			string hostAddress = _wcfClientConfigurator.ConnectInfo.HostAddress;
			string uriString = $"net.tcp://{ hostAddress }/{ hostName }";
			Uri uri = new Uri(uriString);

			EndpointAddress address = new EndpointAddress(uri);

			ChannelFactory<T> channel = new ChannelFactory<T>(binding, address);
			channel.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
			channel.Credentials.UserName.UserName = _wcfClientConfigurator.UserInfo.UserName;
			channel.Credentials.UserName.Password = _wcfClientConfigurator.UserInfo.Password;

			return channel.CreateChannel();
		}
	}
}
