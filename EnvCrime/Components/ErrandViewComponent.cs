using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
	public class ErrandViewComponent : ViewComponent
	{
		private readonly IEnvCrimeRepository repository;

		public ErrandViewComponent(IEnvCrimeRepository repo)
		{
			repository = repo;
		}

		public IViewComponentResult Invoke(string errandId)
		{ 
			Errand errand = repository.GetErrandById(errandId);
			return View("SpecificErrand", errand);
		}
	}
}
