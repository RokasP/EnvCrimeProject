using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class ErrandStatus
    {
        [Key]
        public string StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
