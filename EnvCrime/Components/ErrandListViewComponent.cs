using EnvCrime.Infrastructure.Services;
using EnvCrime.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
    public class ErrandListViewComponent : ViewComponent
    {
        private readonly ErrandService errandService;
        private readonly EmployeeService employeeService;

        public ErrandListViewComponent(ErrandService errService, EmployeeService empService)
        {
            errandService = errService;
            employeeService = empService;
        }

        public IViewComponentResult Invoke(SearchQueryDto dto)
        {
            ClearEmptyStrings(dto);

            var errandDtos = errandService.Search(dto);
            ViewBag.RoleTitle = employeeService.GetLoggedInEmployeeRoleTitle();
            
            return View("ErrandList", errandDtos);
        }

        private void ClearEmptyStrings(SearchQueryDto dto)
        {
            if (dto == null) return;

            dto.EmployeeId = string.IsNullOrWhiteSpace(dto.EmployeeId) ? null : dto.EmployeeId;
            dto.DepartmentId = string.IsNullOrWhiteSpace(dto.DepartmentId) ? null : dto.DepartmentId;
            dto.StatusId = string.IsNullOrWhiteSpace(dto.StatusId) ? null : dto.StatusId;
        }
    }
}
