using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class CitizenController : Controller
    {
		public ViewResult Services()
		{
			return View();
		}

		public ViewResult Faq()
		{
			return View();
		}

		public ViewResult Contact()
		{
			return View();
		}

		public ViewResult Validate(Errand errand)
		{
			return View(errand);
		}

		public ViewResult Thanks()
		{
			return View();
		}
	}
}