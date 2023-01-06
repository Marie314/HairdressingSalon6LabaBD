using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.BLL.Interfaces
{
    public interface IServiceKindsService
    {
        Task<IEnumerable<ServiceKind>> GetAll();
        Task<ServiceKind> GetById(int id);
        Task<IEnumerable<ServiceKind>> Get(int rowsCount, string cacheKey);
        Task<ServiceKind> Create(ServiceKindCreated modelCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(ServiceKindUpdated modelUpdatede);
    }
}
