using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Shared.Helpers
{
    public class AuthenticationHelperService
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IEnvCrimeRepository repository;

        public AuthenticationHelperService(IHttpContextAccessor httpCtx, IEnvCrimeRepository repo)
        {
            httpContext = httpCtx;
            repository = repo;
        }

        public string GetLoggedInEmployeeId()
        {
            return httpContext.HttpContext.User.Identity.Name;
        }

        public Employee GetLoggedInEmployee()
        {
            return repository.GetEmployee(GetLoggedInEmployeeId());
        }

        public string GetLoggedInEmployeeRoleTitle()
        {
            return GetLoggedInEmployee().RoleTitle;
        }
    }
}
