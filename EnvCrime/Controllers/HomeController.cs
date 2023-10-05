using EnvCrime.Infrastructure;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class HomeController : Controller
	{		
		public ViewResult Index()
		{
			var errand = HttpContext.Session.Get<Errand>("NewErrandCitizen");
			if (errand != null)
			{
				return View(errand);
			}
			return View();
		}

		public ViewResult Login()
		{
			return View();
		}
	}
}
