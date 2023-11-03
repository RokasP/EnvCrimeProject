using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models;
using EnvCrime.Models.dto;
using EnvCrime.Models.poco;
using LinqKit;
using System.Data.Entity;

namespace EnvCrime.Infrastructure.Services
{
	public class ErrandService : GenericRepository<Errand, int>
    {
		private static string EXCLUDED_DEPARTMENT_CHOICE = "D00";

		private readonly Utilities utilities;
        private readonly AuthenticationHelperService authenticationService;
        private readonly SequenceService sequenceService;
        private readonly DepartmentService departmentService;
        private readonly EmployeeService employeeService;
        private readonly ErrandStatusService errandStatusService;
        private readonly SampleService sampleService;
		private readonly PictureService pictureService;
		public ErrandService(ApplicationDbContext dbCtx, Utilities utils, AuthenticationHelperService authHelper, SequenceService seqService, DepartmentService deptService,
            EmployeeService empService,
            ErrandStatusService errStatService, SampleService sampService, PictureService pictService) : base(dbCtx)
        {
            utilities = utils;
            authenticationService = authHelper;
            sequenceService = seqService;
            departmentService = deptService;
            employeeService = empService;
            errandStatusService = errStatService;
            sampleService = sampService;
            pictureService = pictService;
        }

        protected override void BeforeInitialSave(Errand entity)
        {
            entity.RefNumber = DateTime.Now.Year + "-45-" + sequenceService.GetNextSequence();
            entity.StatusId = "S_A";
        }

        public IQueryable<ErrandDto> Search(SearchQueryDto query)
        {
            var predicate = PredicateBuilder.New<Errand>(true);

            if (query != null) {
                if (query.StatusId != null)
                {
                    predicate.And(errand => errand.StatusId == query.StatusId);
                }
                if (query.DepartmentId != null)
                {
                    predicate.And(errand => errand.DepartmentId == query.DepartmentId);
                }
                if (query.EmployeeId != null)
                {
                    predicate.And(errand => errand.EmployeeId == query.EmployeeId);
                }
                if (query.RefNumber != null)
                {
                    predicate.And(errand => errand.RefNumber.StartsWith(query.RefNumber));
                }
            }

            return MapToDtos(Search(predicate));
        }

        public Errand GetByIdWithMedia(int id)
        {
            return GetAll().Where(errand => errand.ErrandId == id)
                .Include(errand => errand.Samples)
                .Include(errand => errand.Pictures)
                .First();
        }

        public void SetNoAction(ErrandUpdateDto dto)
        {
            Errand errand = GetById(dto.ErrandId);

            errand.EmployeeId = null;
            errand.StatusId = "S_B";
            errand.InvestigatorInfo = dto.NoActionReason;
            
            Save(errand);
        }

        public async Task UpdateErrand(ErrandUpdateDto dto)
        {
            Errand errand = GetById(dto.ErrandId);

            if (!string.IsNullOrWhiteSpace(dto.EmployeeId))
            {
				errand.EmployeeId = dto.EmployeeId;
			}
            if (!string.IsNullOrWhiteSpace(dto.DepartmentId) && !dto.DepartmentId.Equals(EXCLUDED_DEPARTMENT_CHOICE))
            {
                errand.DepartmentId = dto.DepartmentId;
            }
            if (!string.IsNullOrWhiteSpace(dto.StatusId))
            {
                errand.StatusId = dto.StatusId;
            }
            if (!string.IsNullOrWhiteSpace(dto.Events))
            {
                errand.InvestigatorAction += " " + dto.Events;
            }
            if (!string.IsNullOrWhiteSpace(dto.Information))
            {
                errand.InvestigatorInfo += " " + dto.Information; 
            }
            if (dto.SampleFile != null && dto.SampleFile.Length > 0) 
            {
                String uniqueSampleName = await utilities.UploadFile(dto.SampleFile, "samples");
                sampleService.Save(new Sample() { SampleName = uniqueSampleName, ErrandId = dto.ErrandId });
            }
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                String uniquePictureName = await utilities.UploadFile(dto.ImageFile, "pictures");
                pictureService.Save(new Picture() {  PictureName = uniquePictureName, ErrandId = dto.ErrandId });
            }

            Save(errand);
        }

        private IQueryable<ErrandDto> MapToDtos(IQueryable<Errand> errands)
        {
            return from e in errands
                   join s in errandStatusService.GetAll() on e.StatusId equals s.StatusId
                   join d in departmentService.GetAll() on e.DepartmentId equals d.DepartmentId into tempDepts
                   from allDepts in tempDepts.DefaultIfEmpty()
                   join emp in employeeService.GetAll() on e.EmployeeId equals emp.EmployeeId into tempEmps
                   from allEmps in tempEmps.DefaultIfEmpty()
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
        }
    }
}
