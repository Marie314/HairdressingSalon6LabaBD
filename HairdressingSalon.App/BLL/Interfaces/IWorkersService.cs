using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.BLL.Interfaces
{
    public interface IWorkersService
    {
        Task<IEnumerable<Worker>> GetAll();
        Task<Worker> GetById(int id);
        Task<IEnumerable<Worker>> Get(int rowsCount, string cacheKey);
        Task<Worker> Create(WorkerCreated modelCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(WorkerUpdated modelUpdatede);
    }
}
