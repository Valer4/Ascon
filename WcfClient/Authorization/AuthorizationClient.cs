using System;
using WcfService;

namespace WcfClient.Authorization
{
    public class AuthorizationClient : IAuthorizationClient
    {
        private readonly WcfClientConfigurator _wcfClientConfigurator;

        public AuthorizationClient(WcfClientConfigurator wcfClientConfigurator)
        {
            _wcfClientConfigurator = wcfClientConfigurator;
        }

        public string GetAuthorization() => throw new NotImplementedException();
    }
}
