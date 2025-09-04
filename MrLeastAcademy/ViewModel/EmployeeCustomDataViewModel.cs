using MrLeastAcademy.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLeastAcademy.ViewModel
{
    public class EmployeeCustomDataViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Salary { get; set; }

        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }

        public string? Address { get; set; }

        public int DepartmentId { get; set; }
        public List<Department>? Departments { get; set; }

      
    }
}
