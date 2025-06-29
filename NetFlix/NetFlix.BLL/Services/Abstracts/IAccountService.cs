using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Abstracts
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterViewModel registerVm);
        Task<bool> LoginAsync(LoginViewModel loginVm);
        Task LogoutAsync();
        Task<AppUser> GetUserByEmailAsync(string email);

        Task<IList<string>> GetRolesAsync(AppUser user);
    }
}