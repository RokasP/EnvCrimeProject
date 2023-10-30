using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IEnvCrimeRepository repository;

        public NavigationViewComponent(IEnvCrimeRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            var roleName = repository.GetLoggedInUser().RoleTitle;
            if (roleName != "Investigator" && roleName != "Coordinator" && roleName != "Manager")
            {
                throw new ArgumentException("Invalid role name passed to the Navigation ViewComponent");
            }
            
            ViewBag.ControllerName = roleName;
            ViewBag.StartActionName = "Start" + roleName;
            return View("Navigation");
        }
    }
}
