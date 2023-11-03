using EnvCrime.Infrastructure.Services;
using EnvCrime.Infrastructure.Shared.Helpers;
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
        private readonly AuthenticationHelperService authenticationService;

        public ManagerController(ErrandService errService, ErrandStatusService errStatService, EmployeeService empService, AuthenticationHelperService authService)
        {
            errandService = errService;
            errandStatusService = errStatService;
            employeeService = empService;
            authenticationService = authService;
        }

        public ViewResult StartManager(SearchQueryDto searchQuery)
        {
            ViewBag.Statuses = errandStatusService.GetAll();
            ViewBag.Employees = employeeService.Search(employee => employee.DepartmentId == authenticationService.GetLoggedInEmployee().DepartmentId);
            LimitSearchToRole(searchQuery);
            return View(searchQuery);
        }

        public ViewResult CrimeManager(int errandId)
        {
            ViewBag.ErrandId = errandId;
            ViewBag.Employees = employeeService.Search(employee => employee.DepartmentId == authenticationService.GetLoggedInEmployee().DepartmentId);
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
            searchQuery.DepartmentId = authenticationService.GetLoggedInEmployee().DepartmentId;
        }
    }
}
