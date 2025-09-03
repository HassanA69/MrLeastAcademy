using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrLeastAcademy.Models;
using MrLeastAcademy.ViewModel;

namespace MrLeastAcademy.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext context = new AppDbContext();
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
