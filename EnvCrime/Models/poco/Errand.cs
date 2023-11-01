using EnvCrime.Infrastructure.Shared.Generics;
using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class Errand : GenericEntity
    {
        public int ErrandId { get; set; }

        public string RefNumber { get; set; }

        [Required(ErrorMessage = "Du måste fylla i platsen")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Du måste fylla i brottstypen")]
        public string TypeOfCrime { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Du måste fylla i datumet")]
        public DateTime DateOfObservation { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ditt namn")]
        public string InformerName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ditt mobilnummer")]
        [RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$", ErrorMessage = "Formatet är riktnummer-telefonnummer")]
        public string InformerPhone { get; set; }

        public string? Observation { get; set; }

        public string InvestigatorInfo { get; set; }

        public string InvestigatorAction { get; set; }

        public string StatusId { get; set; }

        public string DepartmentId { get; set; }

        public string EmployeeId { get; set; }

        public ICollection<Sample> Samples { get; set; }

        public ICollection<Picture> Pictures { get; set; }

		public override bool IsNew()
		{
			return ErrandId == 0;
		}
	}
}
