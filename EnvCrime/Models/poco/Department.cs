using EnvCrime.Infrastructure.Shared.Generics;
using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class Department : GenericEntity<string>
    {
        [Required(ErrorMessage = "Du måste fylla i id")]
        [RegularExpression(@"^D[0-9][0-9]", ErrorMessage = "Id ska börja med D följt av två siffror")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i namnet")]
        public string DepartmentName { get; set; }

        public override string GetId()
        {
            return DepartmentId;
        }
    }
}
