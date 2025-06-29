using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Concretes
{public class AccountService : IAccountService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> LoginAsync(LoginViewModel loginVm)
        {
            var user = await _userManager.FindByEmailAsync(loginVm.Email);
            if (user == null) return false;

            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.IsRememberMe, false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterAsync(RegisterViewModel registerVm)
        {
            AppUser appUser = new AppUser
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname, 
                Email = registerVm.Email,
                UserName = registerVm.Username
            };

            
            var result = await _userManager.CreateAsync(appUser, registerVm.Password);
            if (!result.Succeeded) return false;

            var userCount = await _userManager.Users.CountAsync();

            if (userCount == 1)
                await _userManager.AddToRoleAsync(appUser, "Admin");
            else
                await _userManager.AddToRoleAsync(appUser, "User");

            return true;
        }
    }

    
}
