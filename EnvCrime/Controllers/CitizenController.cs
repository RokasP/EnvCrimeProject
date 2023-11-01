using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class CitizenController : Controller
    {
		private readonly IEnvCrimeRepository repository;

		public CitizenController(IEnvCrimeRepository repo)
		{
			repository = repo;
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
            ViewBag.RefNumber = repository.SaveErrand(errand);
			HttpContext.Session.Remove("NewErrandCitizen");
			return View();
		}
	}
}