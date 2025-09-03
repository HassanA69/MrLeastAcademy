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
            if (department.Name != null && department.ManagerName != null)
            {
                context.Departments.Add(department);
                context.SaveChanges();
                return Redirect("Index");
            }
            return View("Add" ,department);
        }
    }

}
