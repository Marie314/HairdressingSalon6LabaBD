using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.BLL.Interfaces
{
    public interface IFeedbacksService
    {
        Task<IEnumerable<Feedback>> GetAll();
        Task<Feedback> GetById(int id);
        Task<IEnumerable<Feedback>> Get(int rowsCount, string cacheKey);
        Task<Feedback> Create(FeedbackCreated modelCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(FeedbackUpdated modelUpdatede);
    }
}
