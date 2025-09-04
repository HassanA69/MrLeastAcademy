using Microsoft.AspNetCore.Mvc.Rendering;
using MrLeastAcademy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLeastAcademy.ViewModel
{
    public class EmployeeCustomDataViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(30, ErrorMessage = "Full name must not exceed 30 characters.")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters.")]
        public string? Name { get; set; }

        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Salary is required.")]
        [Range(6000, 100000, ErrorMessage = "Salary must be between 6,000 and 100,000.")]
        public int Salary { get; set; }

        [Display(Name = "Job Title")]
        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Job title must be between 2 and 50 characters.")]
        public string JobTitle { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Image URL is required.")]
        [RegularExpression(@"^.*\.(jpg|png)$", ErrorMessage = "Image URL must end with .jpg or .png.")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Address must contain letters only.")]
        public string? Address { get; set; }

        [Display(Name ="Department")]
        [Required(ErrorMessage = "Department selection is required.")]

        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; } = new List<SelectListItem>();


    }
}
