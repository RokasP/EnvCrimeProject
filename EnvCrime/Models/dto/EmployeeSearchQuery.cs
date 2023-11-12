namespace EnvCrime.Models.dto
{
    /* Dto (Data Transfer Object) är ofta synonymt med "vy-modell". Objekt av typen EmployeeSearchQuery skapas för ett annat ändamål.
    * 
    * Man vill samla ihop alla egenskaper som man kan söka anställda på i en klass. Då kan man använda samma backend metod
    * för sökningen (filtreringen) av anställda oavsett vilken vy eller sida användaren har utfört sökning i. Det gäller att
    * alltid bygga och fylla på ett söknings dto-objekt som det här och kalla på samma sökningsmetod som tar 
    * in ett objekt av typen EmployeeSearchQuery.
    */
    public class EmployeeSearchQuery
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }
        
        public string DepartmentId { get; set; }

        public string RoleTitle { get; set; }
    }
}
