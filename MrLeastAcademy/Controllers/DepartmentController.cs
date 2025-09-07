using Microsoft.AspNetCore.Mvc;
using MrLeastAcademy.Models;
using MrLeastAcademy.Repository;

namespace MrLeastAcademy.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository departmentRepository;
        IEmployeeRepository employeeRepository;
        public DepartmentController(IDepartmentRepository _departmentRepository, IEmployeeRepository _employeeRepository)
        {
            departmentRepository = _departmentRepository;
            employeeRepository = _employeeRepository;
        }
        public IActionResult Index()
        {
             var departments = departmentRepository.GetAll();

            return View("Index", departments);
        }

        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        public IActionResult SaveAdd(Department department)
        {
            if(ModelState.IsValid)
            {
                departmentRepository.Add(department);
                departmentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        
        public IActionResult Edit(int Id)
        {
            if (Id <= 0)
                return BadRequest("Invalid Department ID");

            var department = departmentRepository.GetById(Id);
            if (department == null)
                return NotFound("No Department with this Id");
            return View("edit", department);

        }

        // save Edits
        [HttpPost]
        public IActionResult saveEdit(int id , Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepository.Update(department);
                departmentRepository.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", department);
        }

        [HttpGet]
        public JsonResult IsValidDepartmentName(string name, int Id)
        {
            var isValid = departmentRepository.IsValidDepartmentName(name, Id);
            return Json(isValid);
        }
        public IActionResult Delete(int Id)
        {
            var department = departmentRepository.GetByIdWithEmployees(Id);
            if (department == null)
                return NotFound("No Department with this Id");
            if (department.Employees.Any())
            {
                ModelState.AddModelError("", "Cannot delete department with assigned employees.");
                return View("Index", departmentRepository.GetAll());
            }
            departmentRepository.Delete(Id);
            departmentRepository.Save();
            return RedirectToAction("Index");
        }
    }

}
