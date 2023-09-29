using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class ManagerController : Controller
	{
		public ViewResult StartManager()
		{
			return View();
		}

		public ViewResult CrimeManager()
		{
			return View();
		}
	}
}
