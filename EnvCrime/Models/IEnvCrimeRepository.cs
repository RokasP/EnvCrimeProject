namespace EnvCrime.Models
{
	public interface IEnvCrimeRepository
	{
		IQueryable<Errand> Errands { get; }

		IQueryable<Employee> Employees { get; }

		IQueryable<Department> Departments { get; }

		IQueryable<ErrandStatus> ErrandStatuses { get; }
	}
}
