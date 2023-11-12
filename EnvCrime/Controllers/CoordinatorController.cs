using EnvCrime.Infrastructure.Services;
using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models.dto;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    [Authorize(Roles = "Coordinator")]
	public class CoordinatorController : Controller
	{
		private readonly ErrandService errandService;
		private readonly DepartmentService departmentService;
		private readonly ErrandStatusService errandStatusService;

		public CoordinatorController(ErrandService errService, DepartmentService deptService, ErrandStatusService errStatService)
		{
			errandService = errService;
			departmentService = deptService;
			errandStatusService = errStatService;
		}

		public ViewResult StartCoordinator(ErrandSearchQueryDto searchQuery)
		{
            ViewBag.Statuses = errandStatusService.GetAll();
            ViewBag.Departments = departmentService.GetAll();
            return View(searchQuery); 
		}

		public ViewResult CrimeCoordinator(int errandId)
		{
			ViewBag.ErrandId = errandId;
			ViewBag.Departments = departmentService.GetAll();
			return View();
		}

		public ViewResult ReportCrime()
		{
			var errand = HttpContext.Session.Get<Errand>("NewErrandCoordinator");
			if (errand != null)
			{
				return View(errand);
			}
			return View();
		}

		public ViewResult Validate(Errand errand)
		{
			HttpContext.Session.Set<Errand>("NewErrandCoordinator", errand);
			return View(errand);
		}

		public ViewResult Thanks()
		{
			var errand = HttpContext.Session.Get<Errand>("NewErrandCoordinator"); // borde normalt finnas
			ViewBag.RefNumber = errandService.Save(errand);
			HttpContext.Session.Remove("NewErrandCoordinator");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCrime(ErrandUpdateDto dto)
		{
			await errandService.UpdateErrand(dto);
			return RedirectToAction("CrimeCoordinator", new { dto.ErrandId });
		}
	}
}
