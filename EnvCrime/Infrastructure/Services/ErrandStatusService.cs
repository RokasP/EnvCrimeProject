using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Services
{
    public class ErrandStatusService : GenericRepository<ErrandStatus, string>
    {
        public ErrandStatusService(ApplicationDbContext dbCtx) : base(dbCtx) { }
    }
}
