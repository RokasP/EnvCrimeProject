using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class InvestigatorController : Controller
	{
		public ViewResult StartInvestigator()
		{
			return View();
		}

		public ViewResult CrimeInvestigator()
		{
			return View();
		}
	}
}
