using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class InvestigatorController : Controller
    {

        private readonly IEnvCrimeRepository repository;
        private IWebHostEnvironment environment;

        public InvestigatorController(IEnvCrimeRepository repo, IWebHostEnvironment env)
        {
            repository = repo;
            environment = env;
        }

        public ViewResult StartInvestigator()
        {
            return View(repository);
        }

        public ViewResult CrimeInvestigator(int errandId)
        {
            ViewBag.ErrandId = errandId;
            return View(repository.ErrandStatuses);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCrime(int errandId, string selectedStatusId, string events, string information, IFormFile loadSample, IFormFile loadImage)
        {
            await repository.UpdateErrandData(errandId, selectedStatusId, events, information, loadSample, loadImage);
            return RedirectToAction("CrimeInvestigator", new { errandId });
        }
    }
}
