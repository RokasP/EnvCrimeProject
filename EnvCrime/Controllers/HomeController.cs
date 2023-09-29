using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class HomeController : Controller
	{
		
		private readonly IEnvCrimeRepository repository;

		public HomeController(IEnvCrimeRepository repo)
		{
			repository = repo;
		}
		
		public ViewResult Index()
		{
			return View(repository);
		}

		public ViewResult Login()
		{
			return View();
		}
	}
}
