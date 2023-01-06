using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IServiceKindsRepository
    {
        Task<IEnumerable<ServiceKind>> GetAll(bool trackChanges);
        Task<ServiceKind> GetById(int id, bool trackChanges);
        Task<IEnumerable<ServiceKind>> Get(int rowsCount, string cacheKey);
        Task Create(ServiceKind model);
        Task Create(IEnumerable<ServiceKind> models);
        Task Delete(ServiceKind model);
        Task Update(ServiceKind model);
    }
}
