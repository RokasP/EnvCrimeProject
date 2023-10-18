using EnvCrime.Models;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class ManagerController : Controller
    {

        private readonly IEnvCrimeRepository repository;

        public ManagerController(IEnvCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult StartManager()
        {
            return View(repository);
        }

        public ViewResult CrimeManager(int errandId)
        {
            ViewBag.ErrandId = errandId;
            return View(repository);
        }

        [HttpPost]
        public IActionResult UpdateCrime(int errandId, string selectedEmployeeId, bool noAction, string reason)
        {
            Errand errand = repository.GetErrand(errandId);
            if (noAction)
            {
                errand.StatusId = "S_B";
                errand.InvestigatorInfo = reason;
            }
            if (selectedEmployeeId != null)
            {
                errand.EmployeeId = selectedEmployeeId;
            }
            repository.SaveErrand(errand);
            return RedirectToAction("CrimeManager", new { errandId });
        }
    }
}
