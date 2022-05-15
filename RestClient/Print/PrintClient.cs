using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Newtonsoft.Json;
using System.Text;

namespace RestClient.Print
{
	public class PrintClient : IPrintClient
	{
		private readonly RestClientConfigurator _restClientConfigurator;
		private readonly string _controllerAddress;

		public PrintClient(
			RestClientConfigurator restClientConfigurator,
			string controllerAddress
		)
		{
			_restClientConfigurator = restClientConfigurator;
			_controllerAddress = controllerAddress;
		}

		public byte[] GetReportOnDetailInMSWord(
			string accessToken,
			DetailRelationEntity selectedDetail,
			out string warningMessage
		)
		{
			string methodName = "report"; // new StackTrace().GetFrame(0).GetMethod().Name;

			byte[] byteArray = _restClientConfigurator.RestApi.GetHttpData(
				url: $"https://{ _restClientConfigurator.ConnectInfo.HostAddress }/{ _controllerAddress }/{ methodName }",
				method: "POST",
				contentType: "application/json; charset=utf-8",
				sentData: _restClientConfigurator.RestApi.StreamHelper.ObjToJson(
					new
					{
						selectedDetail,
						warningMessage = string.Empty
					},
					Encoding.UTF8
				),
				accessToken: accessToken,
				useCertificate: false,
				msgBadStatusCode: "Ошибка. HttpStatusCode = {0}."
			);

			var response =
				new
				{
					byteArray = new byte[0],
					warningMessage = string.Empty
				};

			var result = JsonConvert.DeserializeAnonymousType(
				Encoding.UTF8.GetString(byteArray),
				response
			);

			warningMessage = result.warningMessage;

			return result.byteArray;
		}
	}
}
