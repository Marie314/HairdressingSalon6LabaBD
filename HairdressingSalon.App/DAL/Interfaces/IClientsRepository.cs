using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IClientsRepository
    {
        Task<IEnumerable<Client>> GetAll(bool trackChanges);
        Task<Client> GetById(int id, bool trackChanges);
        Task<IEnumerable<Client>> Get(int rowsCount, string cacheKey);
        Task Create(Client model);
        Task Create(IEnumerable<Client> models);
        Task Delete(Client model);
        Task Update(Client model);
    }
}
