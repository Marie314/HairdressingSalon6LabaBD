using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.BLL.Interfaces
{
    public interface IServicesService
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service> GetById(int id);
        Task<IEnumerable<Service>> Get(int rowsCount, string cacheKey);
        Task<Service> Create(ServiceCreated modelCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(ServiceUpdated modelUpdatede);
    }
}
