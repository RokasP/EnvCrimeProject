using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Services
{
    public class SampleService : GenericRepository<Sample, int>
    {
        public SampleService(ApplicationDbContext dbCtx) : base(dbCtx) { }
    }
}
