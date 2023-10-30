using EnvCrime.Models.dto;
using EnvCrime.Models.poco;
using Microsoft.EntityFrameworkCore;

namespace EnvCrime.Models
{
    public class EFRepository : IEnvCrimeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContext;
        private IWebHostEnvironment environment;

        public EFRepository(ApplicationDbContext ctx, IWebHostEnvironment env, IHttpContextAccessor httpCtx)
        {
            context = ctx;
            environment = env;
            httpContext = httpCtx;
        }

        public Employee GetLoggedInUser()
        {
            var currentUserName = httpContext.HttpContext.User.Identity.Name;
            var currentUser = Employees.Where(employee => employee.EmployeeId == currentUserName).First();
            return currentUser;
        }

        public IQueryable<ErrandDto> ErrandDtos
        {
            get
            {
                var currentUser = GetLoggedInUser();
                return MapToErrandDtos(GetErrandsForUser(currentUser));
            }
        }

        public Errand GetErrand(int errandId)
		{
			return context.Errands.Where(errand => errand.ErrandId == errandId)
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

        public async Task UpdateErrandData(int errandId, String statusId, String events, String information, IFormFile sampleFile, IFormFile imageFile)
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
                String uniqueSampleName = await UploadFile(sampleFile, "samples");
                Sample sample = new Sample() { SampleName = uniqueSampleName, ErrandId = errandId };
                SaveSample(sample);
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                String uniquePictureName = await UploadFile(imageFile, "pictures");
                Picture picture = new Picture() { PictureName = uniquePictureName, ErrandId = errandId };
                SavePicture(picture);
            }
        }

        private IQueryable<Employee> Employees => context.Employees;

        public IQueryable<Employee> ManagerEmployees
        {
            get
            {
                var currentUserName = httpContext.HttpContext.User.Identity.Name;
                var currentUser = Employees.Where(employee => employee.EmployeeId == currentUserName).First();

                return Employees.Where(employee => employee.DepartmentId == currentUser.DepartmentId);
            }
        }

        public Employee GetEmployee(String employeeId)
        {
            return Employees.Where(employee => employee.EmployeeId == employeeId).FirstOrDefault();
        }

        public IQueryable<Department> Departments => context.Departments;

        public Department GetDepartment(String departmentId)
        {
            return Departments.Where(dept => dept.DepartmentId == departmentId).FirstOrDefault();
        }

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatus1;

        public ErrandStatus GetErrandStatus(String errandStatusId)
        {
            return ErrandStatuses.Where(errandStatus => errandStatus.StatusId == errandStatusId).First();
        }

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

        private async Task<String> UploadFile(IFormFile file, string subfolderName)
        {
            var tempFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            String uniqueFileName = MakeUniqueName(file.FileName);

            var finalFilePath = Path.Combine(environment.WebRootPath, "uploads", subfolderName, uniqueFileName);
            File.Move(tempFilePath, finalFilePath);

            return uniqueFileName;
        }

        private static String MakeUniqueName(String fileName)
        {
            return Guid.NewGuid().ToString() + "_" + fileName;
        }

        private IQueryable<Errand> GetErrandsForUser(Employee currentUser)
        {
            switch (currentUser.RoleTitle)
            {
                case "Coordinator":
                    return context.Errands;
                case "Manager":
                    return context.Errands.Where(errand => errand.DepartmentId == currentUser.DepartmentId);
                case "Investigator":
                    return context.Errands.Where(errand => errand.EmployeeId == currentUser.EmployeeId);
                default:
                    return Enumerable.Empty<Errand>().AsQueryable();
            }
        }

        private IQueryable<ErrandDto> MapToErrandDtos(IQueryable<Errand> errands)
        {
            var errandsListItems = from e in errands
                                   join s in ErrandStatuses on e.StatusId equals s.StatusId
                                   join d in Departments on e.DepartmentId equals d.DepartmentId into tempDepts from allDepts in tempDepts.DefaultIfEmpty()
                                   join emp in Employees on e.EmployeeId equals emp.EmployeeId into tempEmps from allEmps in tempEmps.DefaultIfEmpty()
                                   orderby e.RefNumber descending
                                   select new ErrandDto
                                   {
                                       DateOfObservation = e.DateOfObservation,
                                       ErrandId = e.ErrandId,
                                       RefNumber = e.RefNumber,
                                       TypeOfCrime = e.TypeOfCrime,
                                       StatusName = s.StatusName,
                                       DepartmentName = e.DepartmentId == null ? "Ej tillsatt" : allDepts.DepartmentName,
                                       EmployeeName = e.EmployeeId == null ? "Ej tillsatt" : allEmps.EmployeeName
                                   };
            return errandsListItems;
        }
    }
}
