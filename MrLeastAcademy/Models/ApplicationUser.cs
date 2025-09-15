using Microsoft.AspNetCore.Identity;

namespace MrLeastAcademy.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? Address { get; set; }
    }
}
