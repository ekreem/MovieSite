using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;

namespace NetFlix.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(IAccountService accountService, RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            if (!ModelState.IsValid)
                return View(registerVm);

                var isSuccess = await _accountService.RegisterAsync(registerVm);

           
            if (!isSuccess)
            {



                ModelState.AddModelError("", "Qeydiyyat zamanı xəta baş verdi.");
                return View(registerVm);
            }

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm, [FromQuery] string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            var isSuccess = await _accountService.LoginAsync(loginVm);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Email və ya şifrə yanlışdır.");
                return View(loginVm);
            }

            var user = await _accountService.GetUserByEmailAsync(loginVm.Email); // əgər istifadə etmək istəsən
            var roles = await _accountService.GetRolesAsync(user);

            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            return Content("Roles created!");
        }
    }
}
