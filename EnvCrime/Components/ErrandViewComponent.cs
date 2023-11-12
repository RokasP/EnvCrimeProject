using EnvCrime.Infrastructure.Services;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
    public class ErrandViewComponent : ViewComponent
	{
		private readonly ErrandService errandService;
		private readonly ErrandStatusService errandStatusService;
		private readonly DepartmentService departmentService;
		private readonly EmployeeService employeeService;

		public ErrandViewComponent(ErrandService errService, ErrandStatusService errStatService, DepartmentService deptService, EmployeeService empService)
		{
			errandService = errService;
			errandStatusService = errStatService;
			departmentService = deptService;
			employeeService = empService;
		}

		public IViewComponentResult Invoke(string roleName)
		{ 
			ViewBag.RoleName = roleName;
			
			Errand errand = errandService.GetByIdWithMedia(ViewBag.ErrandId);
			var status = errandStatusService.GetById(errand.StatusId);
			var dept = departmentService.GetById(errand.DepartmentId);
			var employee = employeeService.GetById(errand.EmployeeId);

			ViewBag.StatusName = status.StatusName;
			ViewBag.DepartmentName = dept != null ? dept.DepartmentName : "Ej tillsatt";
			ViewBag.EmployeeName = employee != null ? employee.EmployeeName : "Ej tillsatt";

			return View("SpecificErrand", errand);
		}
	}
}
