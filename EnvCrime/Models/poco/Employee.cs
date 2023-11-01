using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Employee : GenericEntity
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string RoleTitle { get; set; }

        public string DepartmentId { get; set; }

		public override bool IsNew()
		{
			return string.IsNullOrWhiteSpace(EmployeeId);
		}
	}
}
