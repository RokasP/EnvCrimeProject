using EnvCrime.Infrastructure.Services;
using EnvCrime.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ErrandService errandService;
        private readonly ErrandStatusService errandStatusService;
        private readonly EmployeeService employeeService;

        public ManagerController(ErrandService errService, ErrandStatusService errStatService, EmployeeService empService)
        {
            errandService = errService;
            errandStatusService = errStatService;
            employeeService = empService;
        }

        public ViewResult StartManager(SearchQueryDto searchQuery)
        {
            ViewBag.Statuses = errandStatusService.GetAll();
            ViewBag.Employees = employeeService.SearchByDepartment(employeeService.GetLoggedInEmployee().DepartmentId);
            LimitSearchToRole(searchQuery);
            return View(searchQuery);
        }

        public ViewResult CrimeManager(int errandId)
        {
            ViewBag.ErrandId = errandId;
            ViewBag.Employees = employeeService.SearchByDepartment(employeeService.GetLoggedInEmployee().DepartmentId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCrime(ErrandUpdateDto dto)
        {
            if (dto.NoAction)
            {
                errandService.SetNoAction(dto);
            }
            else
            {
                await errandService.UpdateErrand(dto);
            }
            return RedirectToAction("CrimeManager", new { dto.ErrandId });
        }

        private void LimitSearchToRole(SearchQueryDto searchQuery)
        {
            searchQuery.DepartmentId = employeeService.GetLoggedInEmployee().DepartmentId;
        }
    }
}
