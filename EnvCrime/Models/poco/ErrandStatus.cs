using EnvCrime.Infrastructure.Shared.Generics;
using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class ErrandStatus : GenericEntity
    {
        [Key]
        public string StatusId { get; set; }

        public string StatusName { get; set; }

		public override bool IsNew()
		{
			return string.IsNullOrWhiteSpace(StatusId);
		}
	}
}
