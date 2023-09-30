using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class CoordinatorController : Controller
	{

		private readonly IEnvCrimeRepository repository;

		public CoordinatorController(IEnvCrimeRepository repo)
		{
			repository = repo;
		}

		public ViewResult StartCoordinator()
		{
			return View(repository);
		}

		public ViewResult CrimeCoordinator()
		{
			return View(repository);
		}

		public ViewResult ReportCrime()
		{
			return View();
		}

		public ViewResult Validate()
		{
			return View();
		}

		public ViewResult Thanks()
		{
			return View();
		}
	}
}
