using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Services
{
    public class DepartmentService : GenericRepository<Department, string>
    {
        public DepartmentService(ApplicationDbContext dbCtx) : base(dbCtx) { }
    }
}
