using System;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace UserInterfaceLayer
{
    internal class ChannelsManager
    {
        internal T GetChannel<T>()
        {
            if( ! Configurator.HostNames.TryGetValue(typeof(T), out string hostName))
                throw new ArgumentException("Имя хоста не найдено.");

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            string hostAddress = Configurator.ConnectInfo.HostAddress;
            string uriString = $"net.tcp://{hostAddress}/{hostName}";
            Uri uri = new Uri(uriString);

            EndpointAddress address = new EndpointAddress(uri);

            ChannelFactory<T> channel = new ChannelFactory<T>(binding, address);
            channel.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            channel.Credentials.UserName.UserName = Configurator.UserInfo.Login;
            channel.Credentials.UserName.Password = Configurator.UserInfo.Password;

            return channel.CreateChannel();
        }
    }
}
