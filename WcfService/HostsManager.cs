using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using WcfService;
using WcfService.InstancesCreatorWithParams;
using WcfService.Security.Authorization;
using WcfService.Security.RoleBasedAccessControl;

namespace BusinessLogicLayer
{
    internal class HostsManager
    {
        internal void CreateHost(Type contractInterface, Type contractClass, string hostName)
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            string hostAddress = WcfServiceConfigurator.ConnectInfo.HostAddress;
            string uriString = $"net.tcp://{hostAddress}/{hostName}";
            Uri uri = new Uri(uriString);

            ServiceHost host = new WcfServiceHost(WcfServiceConfigurator.DiContainer, contractClass);

            host.Credentials.ServiceCertificate.SetCertificate
                (StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySerialNumber, "01");
            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new Validator();

            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host.Authorization.ExternalAuthorizationPolicies =
                new ReadOnlyCollection<IAuthorizationPolicy>(new IAuthorizationPolicy[] { new AuthorizationPolicy() });

            // Исключения для клиента.
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

            host.AddServiceEndpoint(contractInterface, binding, uri);
            host.Open();
        }
    }
}
