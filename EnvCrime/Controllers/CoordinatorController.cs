using EnvCrime.Infrastructure;
using EnvCrime.Models;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class CoordinatorController : Controller
    {

        private static string EXCLUDED_DEPARTMENT_CHOICE = "D00";

        private readonly IEnvCrimeRepository repository;

        public CoordinatorController(IEnvCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult StartCoordinator()
        {
            return View(repository);
        }

        public ViewResult CrimeCoordinator(int errandId)
        {
            ViewBag.ErrandId = errandId;
            return View(repository.Departments);
        }

        public ViewResult ReportCrime()
        {
            var errand = HttpContext.Session.Get<Errand>("NewErrandCoordinator");
            if (errand != null)
            {
                return View(errand);
            }
            return View();
        }

        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.Set<Errand>("NewErrandCoordinator", errand);
            return View(errand);
        }

        public ViewResult Thanks()
        {
            var errand = HttpContext.Session.Get<Errand>("NewErrandCoordinator"); // borde normalt finnas
            ViewBag.RefNumber = repository.SaveErrand(errand);
            HttpContext.Session.Remove("NewErrandCoordinator");
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCrime(int errandId, string selectedDepartmentId)
        {
            if (shouldUpdate(selectedDepartmentId))
            {
                repository.UpdateErrandDepartment(errandId, selectedDepartmentId);
            }
            return RedirectToAction("CrimeCoordinator", new { errandId });
        }

        private bool shouldUpdate(string selectedDepartmentId)
        {
            return !string.IsNullOrWhiteSpace(selectedDepartmentId) && 
                !selectedDepartmentId.Equals(EXCLUDED_DEPARTMENT_CHOICE);
        }
    }
}
