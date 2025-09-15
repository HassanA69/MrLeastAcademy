using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MrLeastAcademy.Models;
using MrLeastAcademy.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MrLeastAcademy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]

        public async Task<IActionResult> Register()
        {
            var roles = await roleManager.Roles.ToListAsync();
            var viewModel = new RegisterViewModel
            {
                Roles = new SelectList(roles, "Id", "Name")
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                ApplicationUser user = new ApplicationUser();
                user.Address = registerViewModel.Address;
                user.UserName = registerViewModel.UserName;

                IdentityResult result = await userManager.CreateAsync(user, registerViewModel.Password);
                // Save user data


                // create Cookies
                if (result.Succeeded)
                {

                    // assign to role
                    var selectedRole = await roleManager.FindByIdAsync(registerViewModel.SelectedRole);
                    if (selectedRole != null)
                    {
                        await userManager.AddToRoleAsync(user, selectedRole.Name);
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            var roles = await roleManager.Roles.ToListAsync();
            registerViewModel.Roles = new SelectList(roles, "Id", "Name");

            return View("Register", registerViewModel);
        }
        public IActionResult Login()
        {

            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginUserViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                // check is Found
                var applicationUser = await userManager.FindByNameAsync(loginUser.UserName);
                if (applicationUser != null)
                {
                    var IsCorrect = await userManager.CheckPasswordAsync(applicationUser, loginUser.Password);

                    if (IsCorrect)
                    {

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("UserAddress", applicationUser.Address));
                        await signInManager.SignInWithClaimsAsync(applicationUser, loginUser.RememberMe, claims);
                        return RedirectToAction("Index", "Department");
                    }


                }
                ModelState.AddModelError("", "Invalid UserName or Password");

            }
            return View("Login", loginUser);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
