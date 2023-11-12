namespace EnvCrime.Models.dto
{
    /* Dto (Data Transfer Object) är ofta synonymt med "vy-modell". I den här applikationen skapar vi dto-klasser för de poco-klasser som vi vill presentera för användaren
    * med annat data än det som poco-klassen innehåller.
    * 
    * I det här fallet vill vi ersätta id på avdelning med namnet.
    */
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string RoleTitle { get; set; }

        public string DepartmentName { get; set; }
    }
}
