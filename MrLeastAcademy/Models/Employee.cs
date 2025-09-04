using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLeastAcademy.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int Salary { get; set; }

        public string JobTitle { get; set; }
        public string ImageUrl { get; set; }

        public string? Address { get; set; }

        [ForeignKey("Department")]
        [Display(Name= "Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
