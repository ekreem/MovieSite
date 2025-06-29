using NetFlix.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Abstracts
{
    public interface IActorService
    {
        Task<List<GetActorVm>> GetAllActorsAsync();
        Task<GetActorVm> GetActorByIdAsync(int id);
        Task CreateActorAsync(CreateActorVm actorVm, string webRootPath); // webRootPath əlavə edildi
        Task UpdateActorAsync(UpdateActorVm actorVm, string webRootPath); // webRootPath əlavə edildi
        Task DeleteActorAsync(int id, string webRootPath); // webRootPath əlavə edildi
    }
}