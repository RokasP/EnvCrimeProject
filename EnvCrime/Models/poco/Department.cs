using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Department : GenericEntity
    {
        public string DepartmentId { get; set; }

        public string DepartmentName { get; set; }

		public override bool IsNew()
		{
			return string.IsNullOrWhiteSpace(DepartmentId);
		}
	}
}
