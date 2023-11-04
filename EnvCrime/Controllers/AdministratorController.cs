using EnvCrime.Infrastructure.Services;
using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models.dto;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly AuthenticationHelperService authenticationService;
        private readonly ErrandStatusService errandStatusService;
        private readonly DepartmentService departmentService;
        private readonly EmployeeService employeeService;
        
        public AdministratorController(AuthenticationHelperService authService, ErrandStatusService errStatService, DepartmentService deptService, EmployeeService empService)
        {
            authenticationService = authService;
            errandStatusService = errStatService;
            departmentService = deptService;
            employeeService = empService;
        }

        public ViewResult AdministerStatuses()
        {
            return View(errandStatusService.GetAll());
        }

        public ViewResult EditStatus(String statusId)
        {
            ErrandStatus status = errandStatusService.GetById(statusId);
            if (status == null)
            {
                status = new ErrandStatus() { StatusId = "" };
            }
            ViewBag.NewStatus = string.IsNullOrWhiteSpace(status.StatusId);
            return View("EditStatus", status);
        }

        public IActionResult SaveChanges(ErrandStatus status)
        {
            // ett nyligen-konstruerat objekt inte ligger på db-kontexten från början och kommer inte att sparas även om man kallar .SaveChanges(). Antingen .Add() ska kallas före sparandet eller en existerande version av objektet får hämtas ut från dataabsen och sedan updateras
            var errandStatus = errandStatusService.GetById(status.StatusId);
            if (errandStatus != null)
            {
                errandStatus.StatusName = status.StatusName;
                errandStatusService.Save(errandStatus);
            }
            else
            {
                errandStatusService.Save(status);
            }
            return RedirectToAction("AdministerStatuses");
        }

        public IActionResult DeleteStatus(String statusId)
        {
            errandStatusService.DeleteById(statusId);
            return RedirectToAction("AdministerStatuses");
        }

        public ViewResult AdministerDepartments()
        {
            return View(departmentService.GetAll());
        }

        public ViewResult EditDepartment(String deptId)
        {
            Department department = departmentService.GetById(deptId);
            if (department == null)
            {
                department = new Department() { DepartmentId = "" };
            }
            ViewBag.NewDepartment = string.IsNullOrWhiteSpace(department.DepartmentId);
            return View("EditDepartment", department);
        }

        public IActionResult SaveDepartment(Department department)
        {
            var dept = departmentService.GetById(department.DepartmentId);
            if (dept != null)
            {
                dept.DepartmentName = department.DepartmentName;
                departmentService.Save(dept);
            }
            else
            {
                departmentService.Save(department);
            }
            return RedirectToAction("AdministerDepartments");
        }

        public IActionResult DeleteDepartment(String deptId)
        {
            departmentService.DeleteById(deptId);
            return RedirectToAction("AdministerDepartments");
        }

        public ViewResult AdministerEmployees(EmployeeSearchQuery query)
        {
            var employees = employeeService.Search(query);
            ViewBag.Employees = employees;
            ViewBag.ResultsExist = employees.Any();
            ViewBag.Departments = departmentService.GetAll();
            ViewBag.Roles = authenticationService.GetRoles();
            return View();
        }

        public ViewResult EditEmployee(String employeeId)
        {
            Employee emp = employeeService.GetById(employeeId);
            if (emp == null)
            {
                emp = new Employee() { EmployeeId = "" };
            }
            ViewBag.NewEmployee = string.IsNullOrWhiteSpace(emp.EmployeeId);
            ViewBag.Roles = authenticationService.GetRoles();
            ViewBag.Departments = departmentService.GetAll();
            return View("EditEmployee", emp);
        }

        public async Task<IActionResult> SaveEmployee(Employee employee)
        {
            var emp = employeeService.GetById(employee.EmployeeId);
            if (emp != null)
            {
                emp.EmployeeName = employee.EmployeeName;
                emp.RoleTitle = employee.RoleTitle;
                emp.DepartmentId = employee.DepartmentId;
                employeeService.Save(emp);
            }
            else
            {
                employeeService.Save(employee);
                await employeeService.AddUserIdentity(employee);
            }
            return RedirectToAction("AdministerEmployees");
        }

        public async Task<IActionResult> DeleteEmployee(String employeeId)
        {
            await employeeService.RemoveUserIdentity(employeeService.GetById(employeeId));
            employeeService.DeleteById(employeeId);
            return RedirectToAction("AdministerEmployees");
        }
    }
}
