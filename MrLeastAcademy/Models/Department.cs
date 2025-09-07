using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;



namespace MrLeastAcademy.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(50, ErrorMessage = "Department name must be between 2 and 50 characters.", MinimumLength = 2)]
        [Display(Name = "Department Name")]
        [Remote(action: "IsValidDepartmentName", controller: "Department", AdditionalFields = "Id", ErrorMessage = "Department name already exists.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manager name is required.")]
        [StringLength(50, ErrorMessage = "Manager name must be between 2 and 50 characters.", MinimumLength = 2)]
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }

        [Display(Name = "Employees")]
        public List<Employee>? Employees { get; set; }
    }
}
