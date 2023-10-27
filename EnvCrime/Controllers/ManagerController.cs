using EnvCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    [Authorize(Roles = "Manager")]
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
            return View(repository.Employees);
        }

        [HttpPost]
        public IActionResult UpdateCrime(int errandId, string selectedEmployeeId, bool noAction, string reason)
        {
            if (noAction)
            {
                if (!string.IsNullOrWhiteSpace(reason))
                {
                    repository.SetErrandNoAction(errandId, reason);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(selectedEmployeeId))
                {
                    repository.UpdateErrandEmployee(errandId, selectedEmployeeId);
                }
            }
            return RedirectToAction("CrimeManager", new { errandId });
        }
    }
}
