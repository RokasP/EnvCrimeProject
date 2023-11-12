namespace EnvCrime.Models.dto
{
    /* Dto (Data Transfer Object) är ofta synonymt med "vy-modell". Just den här klassen skapats för ett annat ändamål.
    
    * Man vill samla ihop alla egenskaper som man kan redigera på ärenden i en klass. Då kan man använda samma backend metod
    * för uppdateringen av ärende objekt oavsett vilken vy eller sida användaren har gjort ändringar i. Det gäller att
    * alltid bygga och fylla på ett uppdaterings dto-objekt som det här och kalla på samma uppdateringsmetod (UpdateErrand) som tar 
    * in ett objekt av typen ErrandUpdateDto.
	* 
	*/
    public class ErrandUpdateDto
	{
		public int ErrandId { get; set; }

		public String DepartmentId { get; set; }

		public String EmployeeId { get; set; }

		public String StatusId { get; set; }

		public String Events { get; set; }

		public String Information { get; set; }

		public IFormFile SampleFile { get; set; }

		public IFormFile ImageFile { get; set; }

		public bool NoAction { get; set; }

		public String NoActionReason { get; set; }
	}
}
