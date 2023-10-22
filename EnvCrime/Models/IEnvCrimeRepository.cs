using EnvCrime.Models.poco;

namespace EnvCrime.Models
{
    public interface IEnvCrimeRepository
	{
		IQueryable<Errand> Errands { get; }

		Errand GetErrand(int errandId);

		String SaveErrand(Errand errand);

		void UpdateErrandDepartment(int errandId, String departmentId);

		void UpdateErrandEmployee(int errandId, String employeeId);

        void SetErrandNoAction(int errandId, String noActionReason);

		Task UpdateErrandData(int errandId, String statusId, String events, String information, IFormFile sampleFile, IFormFile imageFile);

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
