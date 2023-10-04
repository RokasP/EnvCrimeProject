using EnvCrime.Models.poco;

namespace EnvCrime.Models
{
    public class EFRepository : IEnvCrimeRepository
    {
        private readonly ApplicationDbContext context;

        public EFRepository(ApplicationDbContext ctx) 
        {
            context = ctx;
        }

        public IQueryable<Errand> Errands => context.Errands;

        public IQueryable<Employee> Employees => context.Employees;

        public IQueryable<Department> Departments => context.Departments;

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatus1;

        public IQueryable<Sample> Samples => context.Samples;

        public IQueryable<Picture> Pictures => context.Pictures;

        public IQueryable<Sequence> Sequences => context.Sequences;
    }
}
