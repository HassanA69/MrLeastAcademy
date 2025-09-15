using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MrLeastAcademy.ViewModel;
using System.Threading.Tasks;

namespace MrLeastAcademy.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                // save role
                IdentityRole role = new IdentityRole();
                role.Name = roleViewModel.RoleName;
                var res = await RoleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    ViewBag.Success = true;
                    ViewBag.RoleName = role.Name;
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View("AddRole", roleViewModel);
        }
    }
}
