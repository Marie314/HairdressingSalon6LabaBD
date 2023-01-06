using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IServicesRepository
    {
        Task<IEnumerable<Service>> GetAll(bool trackChanges);
        Task<Service> GetById(int id, bool trackChanges);
        Task<IEnumerable<Service>> Get(int rowsCount, string cacheKey);
        Task Create(Service model);
        Task Create(IEnumerable<Service> models);
        Task Delete(Service model);
        Task Update(Service model);
    }
}
