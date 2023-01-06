using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IWorkersRepository
    {
        Task<IEnumerable<Worker>> GetAll(bool trackChanges);
        Task<Worker> GetById(int id, bool trackChanges);
        Task<IEnumerable<Worker>> Get(int rowsCount, string cacheKey);
        Task Create(Worker model);
        Task Create(IEnumerable<Worker> models);
        Task Delete(Worker model);
        Task Update(Worker model);
    }
}
