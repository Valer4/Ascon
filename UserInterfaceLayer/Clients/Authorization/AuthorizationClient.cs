using RestAuthorization = RestClient.Authorization;
using WcfAuthorization = WcfClient.Authorization;

namespace UserInterfaceLayer.Clients.Authorization
{
	internal class AuthorizationClient : IAuthorizationClient
    {
        private readonly WcfAuthorization.IAuthorizationClient _wcfAuthorization;
        private readonly RestAuthorization.IAuthorizationClient _restAuthorization;

        internal AuthorizationClient(
            WcfAuthorization.IAuthorizationClient wcfAuthorization,
            RestAuthorization.IAuthorizationClient restAuthorization
        )
        {
            _wcfAuthorization = wcfAuthorization;
            _restAuthorization = restAuthorization;
        }

        public void GetAuthorization()
        {
            if (ChannelType.Wcf == Program.ChannelType)
                Program.AccessToken = _wcfAuthorization.GetAuthorization();

            Program.AccessToken = _restAuthorization.GetAuthorization();
        }
    }
}
