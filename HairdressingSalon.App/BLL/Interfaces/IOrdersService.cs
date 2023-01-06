using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.BLL.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task<IEnumerable<Order>> Get(int rowsCount, string cacheKey);
        Task<Order> Create(OrderCreated modelCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(OrderUpdated modelUpdatede);
    }
}
