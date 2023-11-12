using EnvCrime.Infrastructure.Services;
using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class CitizenController : Controller
    {
		private readonly ErrandService errandService;

		public CitizenController(ErrandService errService)
		{
			errandService = errService;
		}

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
			HttpContext.Session.Set<Errand>("NewErrandCitizen", errand);
			return View(errand);
		}

		public ViewResult Thanks()
		{
			var errand = HttpContext.Session.Get<Errand>("NewErrandCitizen"); // borde normalt finnas
            ViewBag.RefNumber = errandService.Save(errand).RefNumber;
			HttpContext.Session.Remove("NewErrandCitizen");
			return View();
		}
	}
}