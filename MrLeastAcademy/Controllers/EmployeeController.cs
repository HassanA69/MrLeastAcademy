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
            Employee? employee = context.Employees.FirstOrDefault(x => x.Id == Id);
            if (employee == null)
                return NotFound("No Employee with this Id");
            List<Department> departments = context.Departments.ToList();
            EmployeeCustomDataViewModel empViewModel = new EmployeeCustomDataViewModel();
            empViewModel.Id = employee.Id;
            empViewModel.Name = employee.Name;
            empViewModel.Salary = employee.Salary;
            empViewModel.Address = employee.Address;
            empViewModel.JobTitle = employee.JobTitle;
            empViewModel.ImageUrl = employee.ImageUrl;
            empViewModel.DepartmentId = employee.DepartmentId;
            empViewModel.Departments = departments;
           
            return View("Edit", empViewModel);
        }
        // Save edits
        [HttpPost]
        public IActionResult SaveEdit(int id , EmployeeCustomDataViewModel newEmp)
        {
            var depExists = context.Departments.Any(x => x.Id == newEmp.DepartmentId);

            if (newEmp.Name == null || newEmp.Salary < 0 || newEmp.Address == null || newEmp.JobTitle == null || newEmp.ImageUrl == null || !depExists)
            {
                newEmp.Departments = context.Departments.ToList();
                return View("edit", newEmp);
            }

            var oldEmp = context.Employees.FirstOrDefault(x => x.Id == newEmp.Id);
            if (oldEmp == null)
            {
                return NotFound("Employee not found");
            }

            oldEmp.Name = newEmp.Name;
            oldEmp.Salary = newEmp.Salary;
            oldEmp.Address = newEmp.Address;
            oldEmp.JobTitle = newEmp.JobTitle;
            oldEmp.ImageUrl = newEmp.ImageUrl;
            oldEmp.DepartmentId = newEmp.DepartmentId;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

       
    }
}
