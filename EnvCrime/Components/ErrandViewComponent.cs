using EnvCrime.Models;
using EnvCrime.Models.poco;
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
			
			Errand errand = repository.GetErrand(ViewBag.ErrandId);
			var status = repository.GetErrandStatus(errand.StatusId);
			var dept = repository.GetDepartment(errand.DepartmentId);
			var employee = repository.GetEmployee(errand.EmployeeId);

			ViewBag.StatusName = status.StatusName;
			ViewBag.DepartmentName = dept != null ? dept.DepartmentName : "Ej tillsatt";
			ViewBag.EmployeeName = employee != null ? employee.EmployeeName : "Ej tillsatt";

			return View("SpecificErrand", errand);
		}
	}
}
