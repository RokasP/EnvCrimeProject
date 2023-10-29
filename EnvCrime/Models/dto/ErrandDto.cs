using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.dto
{
    public class ErrandDto
    {
        [DataType(DataType.Date)]
        public DateTime DateOfObservation { get; set; }

        public int ErrandId { get; set; }

        public string RefNumber { get; set; }

        public string TypeOfCrime { get; set; }

        public string StatusName { get; set; }

        public string DepartmentName { get; set; }

        public string EmployeeName { get; set; }
    }
}
