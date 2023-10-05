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

		public Errand GetErrand(int errandId)
		{
			return Errands.Where(errand => errand.ErrandId == errandId).First();
		}

		public String SaveErrand(Errand errand)
        {
            if (errand.ErrandId == 0)
            {
                Sequence lastSequence = GetLastSequence();

				errand.RefNumber = DateTime.Now.Year + "-45-" + lastSequence.CurrentValue++;
                errand.StatusId = "S_A";
                
                context.Errands.Add(errand);
            }
            context.SaveChanges();
            return errand.RefNumber;
        }

		public IQueryable<Employee> Employees => context.Employees;

        public IQueryable<Department> Departments => context.Departments;

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatus1;

        public IQueryable<Sample> Samples => context.Samples;

        public IQueryable<Picture> Pictures => context.Pictures;

        public IQueryable<Sequence> Sequences => context.Sequences;

		public Sequence GetLastSequence()
		{
			return Sequences.Where(seq => seq.Id == 1).First();
		}
	}
}
