using System.ComponentModel.DataAnnotations;

namespace MrLeastAcademy.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        [MaxLength(20, ErrorMessage = "Role name cannot exceed 20 characters.")]
        [MinLength(3, ErrorMessage = "Role name must be at least 3 characters long.")]
        public String RoleName { get; set; }
    }
}
