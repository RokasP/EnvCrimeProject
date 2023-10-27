using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.poco
{
    public class LoginData
    {
        [Required(ErrorMessage = "Vänligen fyll i användarnamn")]
        [Display(Name = "Användarnamn")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i lösenord")]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public String ReturnUrl { get; set; }
    }
}
