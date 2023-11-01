namespace EnvCrime.Models.dto
{
	public class ErrandUpdateDto
	{
		public int ErrandId { get; set; }

		public String DepartmentId { get; set; }

		public String EmployeeId { get; set; }

		public String StatusId { get; set; }

		public String Events { get; set; }

		public String Information { get; set; }

		public IFormFile SampleFile { get; set; }

		public IFormFile ImageFile { get; set; }
	}
}
