using System.ComponentModel.DataAnnotations;

namespace EnvCrime.Models.dto
{
    /* Dto (Data Transfer Object) är ofta synonymt med "vy-modell". I den här applikationen skapar vi dto-klasser för de poco-klasser som vi vill presentera för användaren
     * med annat data än det som poco-klassen innehåller.
     * 
     * I det här fallet vill vi ersätta id på status, avdelning och anställd med namn på samma egenskaper.
     */
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
