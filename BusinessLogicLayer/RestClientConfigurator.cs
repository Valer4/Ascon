using CommonHelpers.Any.Interfaces;

namespace BusinessLogicLayer
{
	public class RestClientConfigurator
    {
        public IRestApi RestApi { get; }
        public ConnectInfoClientService ConnectInfo { get; }

        public UserInfo UserInfo { get; }

        public RestClientConfigurator(
            IRestApi restApi,
            ConnectInfoClientService connectInfo
        )
        {
            RestApi = restApi;
            ConnectInfo = connectInfo;

            UserInfo = new UserInfo(
                "UserNameX",
                "PasswordX"
            );
        }
    }
}
