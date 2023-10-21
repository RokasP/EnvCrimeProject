using EnvCrime.Models.poco;
using Microsoft.EntityFrameworkCore;

namespace EnvCrime.Models
{
    public class EFRepository : IEnvCrimeRepository
    {
        private readonly ApplicationDbContext context;
        private IWebHostEnvironment environment;

        public EFRepository(ApplicationDbContext ctx, IWebHostEnvironment env)
        {
            context = ctx;
            environment = env;
        }

        public IQueryable<Errand> Errands => context.Errands;

		public Errand GetErrand(int errandId)
		{
			return Errands.Where(errand => errand.ErrandId == errandId)
                .Include(errand => errand.Samples)
                .Include(errand => errand.Pictures)
                .First();
		}

		public String SaveErrand(Errand errand)
        {
            if (errand.ErrandId == 0)
            {
                Sequence lastSequence = GetLastSequence();

				errand.RefNumber = DateTime.Now.Year + "-45-" + lastSequence.CurrentValue++;
                errand.StatusId = "S_A";
                
                context.Errands.Add(errand);
            }
            context.SaveChanges();
            return errand.RefNumber;
        }

        public void UpdateErrandDepartment(int errandId, String departmentId)
        {
            Errand errand = GetErrand(errandId);
            errand.DepartmentId = departmentId;
            SaveErrand(errand);
        }

        public void UpdateErrandEmployee(int errandId, String employeeId)
        {
            Errand errand = GetErrand(errandId);
            errand.EmployeeId = employeeId;
            SaveErrand(errand);
        }

        public void SetErrandNoAction(int errandId, String noActionReason)
        {
            Errand errand = GetErrand(errandId);
            errand.EmployeeId = null;
            errand.StatusId = "S_B";
            errand.InvestigatorInfo = noActionReason;
            SaveErrand(errand);
        }

        public async void UpdateErrandData(int errandId, String statusId, String events, String information, IFormFile sampleFile, IFormFile imageFile)
        {
            Errand errand = GetErrand(errandId);
            if (!string.IsNullOrWhiteSpace(statusId))
            {
                errand.StatusId = statusId;
            }
            if (!string.IsNullOrWhiteSpace(events)) 
            {
                errand.InvestigatorAction += " " + events;
            }
            if (!string.IsNullOrWhiteSpace(information))
            {
                errand.InvestigatorInfo += " " + information;
            }
            SaveErrand(errand);

            if (sampleFile != null && sampleFile.Length > 0)
            {
                await UploadFile(sampleFile, "samples");
                Sample sample = new Sample() { SampleName = sampleFile.FileName, ErrandId = errandId };
                SaveSample(sample);
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                await UploadFile(imageFile, "pictures");
                Picture picture = new Picture() { PictureName = imageFile.FileName, ErrandId = errandId };
                SavePicture(picture);
            }
        }

        public IQueryable<Employee> Employees => context.Employees;

        public IQueryable<Department> Departments => context.Departments;

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatus1;

        public IQueryable<Sample> Samples => context.Samples;

        public int SaveSample(Sample sample)
        {
            if (sample.SampleId == 0)
            {
                context.Samples.Add(sample);
            }
            context.SaveChanges();
            return sample.SampleId;
        }

        public IQueryable<Picture> Pictures => context.Pictures;

        public int SavePicture(Picture picture)
        {
            if (picture.PictureId == 0)
            {
                context.Pictures.Add(picture);
            }
            context.SaveChanges();
            return picture.PictureId;
        }

        public IQueryable<Sequence> Sequences => context.Sequences;

		public Sequence GetLastSequence()
		{
			return Sequences.Where(seq => seq.Id == 1).First();
		}

        private async Task UploadFile(IFormFile file, string subfolderName)
        {
            var tempFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var finalFilePath = Path.Combine(environment.WebRootPath, "uploads", subfolderName, file.FileName);
            File.Move(tempFilePath, finalFilePath);
        }
    }
}
