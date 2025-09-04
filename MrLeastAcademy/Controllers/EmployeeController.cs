using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrLeastAcademy.Models;
using MrLeastAcademy.ViewModel;

namespace MrLeastAcademy.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index()
        {
            return View("Index", context.Employees.ToList());
        }

        // Handle link
        public IActionResult Edit(int Id)
        {
            Employee employee = context.Employees.FirstOrDefault(x => x.Id == Id);

            if (employee == null)
                return NotFound("No Employee with this Id");
            return View("Edit", employee);
        }
        // Save edits
        [HttpPost]
        public IActionResult SaveEdit(Employee newEmp)
        {
            var depExists = context.Departments.Any(x => x.Id == newEmp.DepartmentId);

            if (newEmp.Name ==null || newEmp.Salary<0 || newEmp.Address==null || newEmp.JobTitle == null || newEmp.ImageUrl == null || !depExists)
            {
                return View("edit", newEmp);
            }
            
            var oldEmp = context.Employees.FirstOrDefault(x => x.Id == newEmp.Id);
            oldEmp.Name = newEmp.Name;
            oldEmp.Salary = newEmp.Salary;
            oldEmp.Address = newEmp.Address;
            oldEmp.JobTitle = newEmp.JobTitle;
            oldEmp.ImageUrl = newEmp.ImageUrl;
            oldEmp.DepartmentId = newEmp.DepartmentId;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            var employee = context.Employees
                 .Include(x => x.Department)
                 .FirstOrDefault(x => x.Id == Id);

            EmployeeCustomDataViewModel employeeCustomData = new EmployeeCustomDataViewModel();
            List<string> branches = new List<string>() { "Cairo", "Alex", "Assiut" };

            employeeCustomData.EmployeeName = employee.Name;
            employeeCustomData.DepartmentName = employee.Department.Name;
            employeeCustomData.Branches = branches;
            employeeCustomData.Temp = 38;
            employeeCustomData.Message = "gigit gigit gooo";
            employeeCustomData.Color = "red";
            return View("Details", employeeCustomData);

        }
    }
}
