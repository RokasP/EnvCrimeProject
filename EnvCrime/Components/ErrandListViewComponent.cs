using EnvCrime.Infrastructure.Services;
using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Components
{
    public class ErrandListViewComponent : ViewComponent
    {
        private readonly AuthenticationHelperService authenticationService;
        private readonly ErrandService errandService;

        public ErrandListViewComponent(AuthenticationHelperService authHelper, ErrandService errService)
        {
            authenticationService = authHelper;
            errandService = errService;
        }

        public IViewComponentResult Invoke(SearchQueryDto dto)
        {
            ClearEmptyStrings(dto);

            var errandDtos = errandService.Search(dto);
            ViewBag.RoleTitle = authenticationService.GetLoggedInEmployeeRoleTitle();
            
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
