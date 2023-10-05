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

		public IViewComponentResult Invoke(string roleName)
		{ 
			ViewBag.RoleName = roleName;
			var errand = repository.GetErrand(ViewBag.ErrandId);
			return View("SpecificErrand", errand);
		}
	}
}
