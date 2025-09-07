using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrLeastAcademy.Models;
using MrLeastAcademy.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrLeastAcademy.Repository;

namespace MrLeastAcademy.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository employeeRepository;
        IDepartmentRepository departmentRepository;
        public EmployeeController(IEmployeeRepository _employeeRepository, IDepartmentRepository _departmentRepository)
        {
            employeeRepository = _employeeRepository;
            departmentRepository = _departmentRepository;
        }

        public IActionResult Index()
        {
            return View("Index", employeeRepository.GetAll() );
        }

        // Handle link
        public IActionResult Edit(int Id)
        {
            if(Id <= 0)
            {
                return BadRequest("Invalid Employee ID");
            }
           Employee employee= employeeRepository.GetById(Id);
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
                Departments = departmentRepository.GetAll()
                        .Select(d => new SelectListItem
                        {
                            Value = d.Id.ToString(),
                            Text = d.Name
                        }).ToList()

            };

            return View("Edit", empViewModel);
        }
        // Save edits
        [HttpPost]

        public IActionResult SaveEdit(int id, EmployeeCustomDataViewModel newEmp)
        {
            if (!ModelState.IsValid)
            {
                newEmp.Departments =employeeRepository.GetAll()
                    .Select(e => new SelectListItem{
                        Value = e.Id.ToString(),
                        Text = e.Name
                    }).ToList();
                return View("edit", newEmp);
            }

            var oldEmp = employeeRepository.GetById(id);
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

            employeeRepository.Update(oldEmp);
            employeeRepository.Save();

            return RedirectToAction("Index");
        }

        // new Employee

        public IActionResult New()
        {
            ViewBag.Departments = departmentRepository.GetAll();
            return View("New");
        }

        // Save New Employee
        [HttpPost]
        public IActionResult SaveNew(Employee employee)
        {
            var depExists = departmentRepository.Exists(employee.DepartmentId);
            if (ModelState.IsValid && !depExists)
            {
                employeeRepository.Add(employee);
                employeeRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = departmentRepository.GetAll();
            return View("New", employee);
        }
        // Delete Employee
        public IActionResult Delete(int id)
        {
            var emp = employeeRepository.GetById(id);
            if (emp == null)
                return NotFound();

            employeeRepository.Delete(id);
            employeeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}
