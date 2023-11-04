using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.dto;
using EnvCrime.Models.poco;
using LinqKit;

namespace EnvCrime.Infrastructure.Services
{
    public class EmployeeService : GenericRepository<Employee, string>
    {
        private readonly DepartmentService departmentService;

        public EmployeeService(ApplicationDbContext dbCtx, DepartmentService deptService) : base(dbCtx)
        {
            departmentService = deptService;
        }

        public IQueryable<Employee> SearchByDepartment(string departmentId)
        {
            return Search(employee => employee.DepartmentId == departmentId);
        }

        public IQueryable<EmployeeDto> Search(EmployeeSearchQuery query)
        {
            var predicate = PredicateBuilder.New<Employee>(true);

            if (query != null)
            {
                if (query.EmployeeId != null)
                {
                    predicate.And(employee => employee.EmployeeId == query.EmployeeId);
                }
                if (query.EmployeeName != null)
                {
                    predicate.And(employee => employee.EmployeeName.StartsWith(query.EmployeeName));
                }
                if (query.DepartmentId != null)
                {
                    predicate.And(employee => employee.DepartmentId == query.DepartmentId);
                }
                if (query.RoleTitle != null)
                {
                    predicate.And(employee => employee.RoleTitle == query.RoleTitle);
                }
            }

            return MapToDtos(Search(predicate));
        }
        
        private IQueryable<EmployeeDto> MapToDtos(IQueryable<Employee> employees)
        {
            return from e in employees
                   join d in departmentService.GetAll() on e.DepartmentId equals d.DepartmentId
                   orderby e.EmployeeId ascending
                   select new EmployeeDto
                   {
                       EmployeeId = e.EmployeeId,
                       EmployeeName = e.EmployeeName,
                       RoleTitle = e.RoleTitle,
                       DepartmentName = d.DepartmentName
                   };
        }
    }
}
