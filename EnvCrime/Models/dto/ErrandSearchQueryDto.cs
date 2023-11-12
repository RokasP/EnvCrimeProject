namespace EnvCrime.Models.dto
{
    /* Dto (Data Transfer Object) är ofta synonymt med "vy-modell". Objekt av typen ErrandSearchDto skapas för ett annat ändamål.
    * 
    * Man vill samla ihop alla egenskaper som man kan söka ärenden på i en klass. Då kan man använda samma backend metod
    * för sökningen (filtreringen) av ärenden oavsett vilken vy eller sida användaren har utfört sökning i. Det gäller att
    * alltid bygga och fylla på ett söknings dto-objekt som det här och kalla på samma sökningsmetod som tar 
    * in ett objekt av typen ErrandSearchDto.
    */
    public class ErrandSearchQueryDto
    {
        public string StatusId { get; set; }

        public string DepartmentId { get; set; }

        public string EmployeeId { get; set; }

        public string RefNumber { get; set; }
    }
}
