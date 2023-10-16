using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class InvestigatorController : Controller
	{

		private readonly IEnvCrimeRepository repository;

		public InvestigatorController(IEnvCrimeRepository repo)
		{
			repository = repo;
		}

		public ViewResult StartInvestigator()
		{
			return View(repository);
		}

		public ViewResult CrimeInvestigator(string errandId)
		{
			ViewBag.RoleName = "handläggare";
			ViewBag.ErrandId = errandId;
			return View(repository);
		}
	}
}
