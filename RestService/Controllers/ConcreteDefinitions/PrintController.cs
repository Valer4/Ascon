using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Print;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestService.Controllers.ConcreteDefinitions
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrintController : AbstractController
	{
		private readonly IPrintPresenter _printPresenter;

		public PrintController(IPrintPresenter printPresenter) => _printPresenter = printPresenter;

		[HttpPost("report")]
		[Authorize(Roles = "admin")]
		public async Task<JsonResult> GetReportOnDetailInMSWord()
		{
			string json = GetPostBody();
			if (string.IsNullOrEmpty(json))
				return Json(new { byteArray = Array.Empty<byte>(), warningMessage = string.Empty }); // Нужно придумать описание ошибки.

			var pattern = new {
				selectedDetail = new DetailRelationEntity(),
				warningMessage = string.Empty };
			var paramsObj = JsonConvert.DeserializeAnonymousType(json, pattern);

			string? warningMessage = null;

			byte[] byteArray = await Task.Run(() => _printPresenter.GetReportOnDetailInMSWord(paramsObj?.selectedDetail, out warningMessage));

			return Json(new {
				byteArray,
				warningMessage });
		}
	}
}
