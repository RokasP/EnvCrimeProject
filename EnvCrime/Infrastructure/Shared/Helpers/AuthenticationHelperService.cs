using EnvCrime.Infrastructure.Services;
using EnvCrime.Models;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Identity;

namespace EnvCrime.Infrastructure.Shared.Helpers
{
    public class AuthenticationHelperService
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly AppIdentityDbContext identityContext;
        private readonly EmployeeService employeeService;

        public AuthenticationHelperService(IHttpContextAccessor httpCtx, AppIdentityDbContext appIdCtx, EmployeeService empService)
        {
            httpContext = httpCtx;
            identityContext = appIdCtx;
            employeeService = empService;
        }

        public string GetLoggedInEmployeeId()
        {
            return httpContext.HttpContext.User.Identity.Name;
        }

        public Employee GetLoggedInEmployee()
        {
            return employeeService.GetById(GetLoggedInEmployeeId());
        }

        public string GetLoggedInEmployeeRoleTitle()
        {
            return GetLoggedInEmployee().RoleTitle;
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            return identityContext.Roles;
        }
    }
}
