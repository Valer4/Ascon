using Microsoft.AspNetCore.Mvc;

namespace RestService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AbstractController : Controller
	{
		protected string GetPostBody()
		{
			string json;
			using (StreamReader reader = new(Request.Body))
				json = reader.ReadToEnd();
			return json;
		}
	}
}
