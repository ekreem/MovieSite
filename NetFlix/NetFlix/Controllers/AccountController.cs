using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;
using System.Security.Claims;

namespace NetFlix.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(IAccountService accountService, RoleManager<IdentityRole> roleManager, IFavoriteService favoriteService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _roleManager = roleManager;
            _favoriteService = favoriteService;
            _userManager = userManager;
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
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        //profil
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _favoriteService.GetFavoritesByUserIdAsync(userId);

            var model = new ProfileVm
            {
                Username = User.Identity.Name,
                Email = User.FindFirstValue(ClaimTypes.Email),
                FavoriteMovies = favorites
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddFavorite(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            await _favoriteService.AddFavoriteAsync(userId, movieId);
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            await _favoriteService.RemoveFavoriteAsync(userId, movieId);
            return RedirectToAction("Profile");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ToggleFavorite(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            bool isFavorite = await _favoriteService.IsFavoriteAsync(userId, movieId);

            if (isFavorite)
            {
                await _favoriteService.RemoveFavoriteAsync(userId, movieId);
            }
            else
            {
                await _favoriteService.AddFavoriteAsync(userId, movieId);
            }
            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer);

            return RedirectToAction("Profile");  // Və ya gəldiyi səhifəyə qaytar
        }
        //ADMIN 
        public async Task<IActionResult> CreateAdmin()
        {
            // Admin rolu varsa, keç
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            // Admin varsa, keç
            var adminEmail = "ekremmelikov@gmail.com";
            var adminPassword = "Ekrem123.";


            var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Name = "Ekrem",
                    Surname = "Melikov",


                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    return Content("Admin uğurla yaradıldı ✅");
                }
                else
                {
                    return Content("Xəta baş verdi ❌: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            return Content("Admin artıq mövcuddur.");
        }
    }
}
