using Microsoft.AspNetCore.Mvc;
using MrLeastAcademy.Models;

namespace MrLeastAcademy.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext context = new AppDbContext();


        public IActionResult Index()
        {
            List<Department> departments = context.Departments.ToList();

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
                context.Departments.Add(department);
                context.SaveChanges();
                return Redirect("Index");
            }
            return View("Add" ,department);
        }
        public IActionResult Edit(int Id)
        {
            Department department = context.Departments.FirstOrDefault(x => x.Id == Id);
            if (department == null)
                return NotFound("No Department with this Id");
            return View("Edit", department);

        }

        // save Edits
        [HttpPost]
        public IActionResult saveEdit(int id , Department department)
        {
            if (ModelState.IsValid)
            {
                var oldDept = context.Departments.FirstOrDefault(x => x.Id == id);
                oldDept.Name = department.Name;
                oldDept.ManagerName = department.ManagerName;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", department);
        }

        public IActionResult IsValidDepartmentName(string Name)
        {
            var dept = context.Departments.FirstOrDefault(x => x.Name.ToLower() == Name.ToLower());
            if (dept == null)
                return Json(true);
            return Json("This department name is already exists. Please enter a unique name.");

        }
    }

}
