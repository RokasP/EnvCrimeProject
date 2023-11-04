using EnvCrime.Infrastructure.Shared.Generics;
using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class ErrandStatus : GenericEntity<string>
    {
        [Key]
        [Required(ErrorMessage = "Du måste fylla i id")]
        [RegularExpression(@"^S_[A-Z]", ErrorMessage = "Id ska börja med S_ följt av en singel stor bokstav")]
        public string StatusId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i namnet")]
        public string StatusName { get; set; }

        public override string GetId()
        {
            return StatusId;
        }
    }
}
