using EnvCrime.Models.poco;

namespace EnvCrime.Models
{
    public interface IEnvCrimeRepository
	{
		IQueryable<Errand> Errands { get; }

		Errand GetErrand(int errandId);

		String SaveErrand(Errand errand);

		IQueryable<Employee> Employees { get; }

		IQueryable<Department> Departments { get; }

		IQueryable<ErrandStatus> ErrandStatuses { get; }

		IQueryable<Sample> Samples { get; }

		int SaveSample(Sample sample);

		IQueryable<Picture> Pictures { get; }

		int SavePicture(Picture picture);

		IQueryable<Sequence> Sequences { get; }

		Sequence GetLastSequence();
	}
}
