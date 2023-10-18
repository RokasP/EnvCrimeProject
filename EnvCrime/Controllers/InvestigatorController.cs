using EnvCrime.Models;
using EnvCrime.Models.poco;
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
            return View(repository);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCrime(int errandId, string selectedStatusId, string events, string information, IFormFile loadSample, IFormFile loadImage)
        {
            Errand errand = repository.GetErrand(errandId);
            errand.StatusId = selectedStatusId;
            errand.InvestigatorAction += " " + events;
            errand.InvestigatorInfo += " " + information;
            repository.SaveErrand(errand);

            if (loadSample != null && loadSample.Length > 0) 
            {
                await UploadFileToSystem(loadSample, "samples");
                Sample sample = new Sample()
                {
                    SampleName = loadSample.FileName,
                    ErrandId = errandId
                };
                repository.SaveSample(sample);
            }
            if (loadImage != null && loadImage.Length > 0)
            {
                await UploadFileToSystem(loadImage, "pictures");

                Picture picture = new Picture()
                {
                    PictureName = loadImage.FileName,
                    ErrandId = errandId
                };
                repository.SavePicture(picture);
            }
            return RedirectToAction("CrimeInvestigator", new { errandId });
        }

        private async Task UploadFileToSystem(IFormFile file, string subfolderName)
        {
            var tempFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var finalFilePath = Path.Combine(environment.WebRootPath, "uploads", subfolderName, file.FileName);
            System.IO.File.Move(tempFilePath, finalFilePath);
        }
    }
}
