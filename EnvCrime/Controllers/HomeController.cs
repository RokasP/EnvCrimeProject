using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class HomeController : Controller
	{		
		public ViewResult Index()
		{
			return View();
		}

		public ViewResult Login()
		{
			return View();
		}
	}
}
