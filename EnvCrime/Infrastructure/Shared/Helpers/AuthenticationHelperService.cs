using EnvCrime.Models;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Identity;

namespace EnvCrime.Infrastructure.Shared.Helpers
{
    public class AuthenticationHelperService
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly AppIdentityDbContext identityContext;
        private readonly UserManager<IdentityUser> userManager;

        public AuthenticationHelperService(IServiceProvider services, IHttpContextAccessor httpCtx, AppIdentityDbContext appIdCtx)
        {
            httpContext = httpCtx;
            identityContext = appIdCtx;

            userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        }

        public string GetLoggedInEmployeeId()
        {
            return httpContext.HttpContext.User.Identity.Name;
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            return identityContext.Roles;
        }

        public async Task AddUser(Employee employee)
        {
            if (!identityContext.Users.Where(user => user.UserName == employee.EmployeeId).Any())
            {
                IdentityUser newUser = new IdentityUser(employee.EmployeeId);
                await userManager.CreateAsync(newUser, "Pass00?");
                await userManager.AddToRoleAsync(newUser, employee.RoleTitle);
            }
        }

        public async Task RemoveUser(Employee employee)
        {
            var user = await userManager.FindByNameAsync(employee.EmployeeId);
            await userManager.RemoveFromRoleAsync(user, employee.RoleTitle);
            await userManager.DeleteAsync(user);
        }
    }
}
