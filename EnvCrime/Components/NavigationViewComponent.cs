using EnvCrime.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly EmployeeService employeeService;

        public NavigationViewComponent(EmployeeService empService)
        {
            employeeService = empService;
        }

        public IViewComponentResult Invoke()
        {
            var roleName = employeeService.GetLoggedInEmployeeRoleTitle();
            if (roleName != "Investigator" && roleName != "Coordinator" && roleName != "Manager" && roleName != "Administrator")
            {
                throw new ArgumentException("Invalid role name passed to the Navigation ViewComponent");
            }
            
            ViewBag.ControllerName = roleName;
            ViewBag.StartActionName = "Start" + roleName;
            return View("Navigation");
        }
    }
}
