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
            return View(repository.Employees);
        }

        [HttpPost]
        public IActionResult UpdateCrime(int errandId, string selectedEmployeeId, bool noAction, string reason)
        {
            //lägg inte in något värde än så länge eftersom vi vet inte när vi ska behöva det och vi vill inte hämta ut data från databasen flera gånger i onödan. Det är helt möjligt att man kör igenom metoden utan att behöva uppdatera något och således hämta data alls
            Errand errand = null; 
            if (noAction)
            {
                if (!string.IsNullOrWhiteSpace(reason))
                {
                    SetNoAction(errand, errandId, reason);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(selectedEmployeeId))
                {
                    SetEmployee(errand, errandId, selectedEmployeeId);
                }
            }
            return RedirectToAction("CrimeManager", new { errandId });
        }

        private void SetNoAction(Errand errand, int errandId, string reason)
        {             
            if (errand == null)
            {
                errand = repository.GetErrand(errandId);
            }
            errand.EmployeeId = null;
            errand.StatusId = "S_B";
            errand.InvestigatorInfo = reason;
            repository.SaveErrand(errand);
        }

        private void SetEmployee(Errand errand, int errandId, string employeeId)
        {
            if (errand == null) 
            {
                errand = repository.GetErrand(errandId);
            }
            errand.EmployeeId = employeeId;
            repository.SaveErrand(errand);
        }
    }
}
