using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrLeastAcademy.Models;
using MrLeastAcademy.Repository;

namespace MrLeastAcademy.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        IDepartmentRepository _departmentRepository;
        IEmployeeRepository _employeeRepository;
        public DepartmentController(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult DeptEmps()
        {
            return View("DeptEmps", _departmentRepository.GetAll());
        }

        public IActionResult GetEmployees(int id)
        {
            List<Employee> employees = _employeeRepository.GetEmployeesByDepartment(id);
            return Json( employees );
        }
        
        public IActionResult Index()
        {
             var departments = _departmentRepository.GetAll();

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
                _departmentRepository.Add(department);
                _departmentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        
        public IActionResult Edit(int Id)
        {
            if (Id <= 0)
                return BadRequest("Invalid Department ID");

            var department = _departmentRepository.GetById(Id);
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
                _departmentRepository.Update(department);
                _departmentRepository.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", department);
        }

        [HttpGet]
        public JsonResult IsValidDepartmentName(string name, int Id)
        {
            var isValid = _departmentRepository.IsValidDepartmentName(name, Id);
            return Json(isValid);
        }
        public IActionResult Delete(int Id)
        {
            var department = _departmentRepository.GetByIdWithEmployees(Id);
            if (department == null)
                return NotFound("No Department with this Id");
            if (department.Employees.Any())
            {
                ModelState.AddModelError("", "Cannot delete department with assigned employees.");
                return View("Index", _departmentRepository.GetAll());
            }
            _departmentRepository.Delete(Id);
            _departmentRepository.Save();
            return RedirectToAction("Index");
        }
    }

}
