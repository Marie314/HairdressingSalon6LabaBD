using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.BLL.Interfaces
{
    public interface IClientsService
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(int id);
        Task<IEnumerable<Client>> Get(int rowsCount, string cacheKey);
        Task<Client> Create(ClientCreated modelCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(ClientUpdated modelUpdatede);
    }
}
