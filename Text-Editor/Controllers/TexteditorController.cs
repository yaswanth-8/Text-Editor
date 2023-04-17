using Microsoft.AspNetCore.Mvc;

namespace Text_Editor.Controllers
{
	public class TexteditorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
