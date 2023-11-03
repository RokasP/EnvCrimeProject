using EnvCrime.Infrastructure.Services;
using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    [Authorize(Roles = "Investigator")]
	public class InvestigatorController : Controller
	{

		private readonly AuthenticationHelperService authenticationService;
		private readonly ErrandService errandService;
		private readonly ErrandStatusService errandStatusService;

		public InvestigatorController(AuthenticationHelperService authService, ErrandService errService, ErrandStatusService errStatService)
		{
			authenticationService = authService;
			errandService = errService;
			errandStatusService = errStatService;
		}

		public ViewResult StartInvestigator(SearchQueryDto searchQuery)
		{
			ViewBag.Statuses = errandStatusService.GetAll();
			LimitSearchToRole(searchQuery);
			return View(searchQuery);
		}

        public ViewResult CrimeInvestigator(int errandId)
		{
			ViewBag.ErrandId = errandId;
			ViewBag.Statuses = errandStatusService.GetAll();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCrime(ErrandUpdateDto dto)
		{
			await errandService.UpdateErrand(dto);
			return RedirectToAction("CrimeInvestigator", new { dto.ErrandId });
		}

        private void LimitSearchToRole(SearchQueryDto searchQuery)
        {
            searchQuery.EmployeeId = authenticationService.GetLoggedInEmployeeId();
        }
    }
}
