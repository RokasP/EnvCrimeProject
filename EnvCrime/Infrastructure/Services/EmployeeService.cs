using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Services
{
    public class EmployeeService : GenericRepository<Employee, string>
    {
        public EmployeeService(ApplicationDbContext dbCtx) : base(dbCtx) { }

        public IQueryable<Employee> SearchByDepartment(string departmentId)
        {
            return Search(employee => employee.DepartmentId == departmentId);
        }
    }
}
