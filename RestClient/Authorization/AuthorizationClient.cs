using BusinessLogicLayer;
using Newtonsoft.Json;
using System.Text;

namespace RestClient.Authorization
{
	public class AuthorizationClient : IAuthorizationClient
	{
		private readonly RestClientConfigurator _restClientConfigurator;
		private readonly string _controllerAddress;

		public AuthorizationClient(
			RestClientConfigurator restClientConfigurator,
			string controllerAddress
		)
		{
			_restClientConfigurator = restClientConfigurator;
			_controllerAddress = controllerAddress;
		}

		public string GetAuthorization()
		{
			string methodName = "token"; // new StackTrace().GetFrame(0).GetMethod().Name;

			byte[] byteArray = _restClientConfigurator.RestApi.GetHttpData(
				url: $"https://{ _restClientConfigurator.ConnectInfo.HostAddress }/{ _controllerAddress }/{ methodName }",
				method: "POST",
				contentType: "application/json; charset=utf-8",
				sentData: _restClientConfigurator.RestApi.StreamHelper.ObjToJson(
					new
					{
						_restClientConfigurator.UserInfo.UserName,
						_restClientConfigurator.UserInfo.Password
					},
					Encoding.UTF8
				),
				useCertificate: false,
				msgBadStatusCode: "Ошибка. HttpStatusCode = {0}."
			);

			var response =
				new
				{
					accessToken = string.Empty,
					username = string.Empty
				};

			var result =
				JsonConvert.DeserializeAnonymousType(
					Encoding.UTF8.GetString(byteArray),
					response
				);

			return result.accessToken;
		}
	}
}
