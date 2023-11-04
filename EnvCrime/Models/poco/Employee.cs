using EnvCrime.Infrastructure.Shared.Generics;
using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class Employee : GenericEntity<string>
    {
        [Required(ErrorMessage = "Du måste fylla i id")]
        [RegularExpression(@"^E[0-9][0-9][0-9]", ErrorMessage = "Id ska börja med E följt av tre siffror")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i namnet")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Du måste välja rollen")]
        public string RoleTitle { get; set; }

        [Required(ErrorMessage = "Du måste välja avdelningen")]
        public string DepartmentId { get; set; }

        public override string GetId()
        {
            return EmployeeId;
        }
    }
}
