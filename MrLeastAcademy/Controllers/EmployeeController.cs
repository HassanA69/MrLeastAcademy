using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrLeastAcademy.Models;
using MrLeastAcademy.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var employee = context.Employees.FirstOrDefault(x => x.Id == Id);
            if (employee == null)
                return NotFound("No Employee with this Id");


            var empViewModel = new EmployeeCustomDataViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                JobTitle = employee.JobTitle,
                ImageUrl = employee.ImageUrl,
                DepartmentId = employee.DepartmentId,
                Departments = context.Departments.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList()
            };

            return View("Edit", empViewModel);
        }
        // Save edits
        [HttpPost]

        public IActionResult SaveEdit(int id, EmployeeCustomDataViewModel newEmp)
        {
            

            
            //if (newEmp.Name == null || newEmp.Salary < 0 || newEmp.Address == null || newEmp.JobTitle == null || newEmp.ImageUrl == null || !depExists)
            if(!ModelState.IsValid )
            {
                newEmp.Departments = context.Departments.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList();
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

        // new Employee
        [HttpPost]
        public IActionResult New()
        {
            ViewBag.Departments = context
                .Departments.ToList();
            return View("New");
        }

        // Save New Employee
        [HttpPost]
        public IActionResult SaveNew(Employee employee)
        {
           // if(employee.Name !=null && employee.Salary >= 6000 && employee.Address != null && employee.JobTitle != null && employee.ImageUrl != null && context.Departments.Any(x => x.Id == employee.DepartmentId))
            if (ModelState.IsValid && context.Departments.Any(x => x.Id == employee.DepartmentId))
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = context
                .Departments.ToList();
            return View("New", employee);
        }
    }
}
