using Microsoft.AspNetCore.Mvc.Rendering;
using MrLeastAcademy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLeastAcademy.ViewModel
{
    public class EmployeeCustomDataViewModel
    {
        public int Id { get; set; }
        [Display(Name="Full Name")]
        public string? Name { get; set; }

        public int Salary { get; set; }

        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }

        public string? Address { get; set; }

        [Display(Name ="Department")]
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; } = new List<SelectListItem>();


    }
}
