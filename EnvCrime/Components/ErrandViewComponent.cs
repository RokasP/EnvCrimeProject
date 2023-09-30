using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
	public class ErrandViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(string roleName)
		{ 
			ViewBag.RoleName = roleName;
			return View("SpecificErrand");
		}
	}
}
