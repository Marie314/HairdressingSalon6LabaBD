using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IFeedbacksRepository
    {
        Task<IEnumerable<Feedback>> GetAll(bool trackChanges);
        Task<Feedback> GetById(int id, bool trackChanges);
        Task<IEnumerable<Feedback>> Get(int rowsCount, string cacheKey);
        Task Create(Feedback model);
        Task Create(IEnumerable<Feedback> models);
        Task Delete(Feedback model);
        Task Update(Feedback model);
    }
}
