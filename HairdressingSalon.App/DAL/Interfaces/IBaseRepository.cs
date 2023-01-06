using System.Linq.Expressions;

namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAllEntities(bool trackChanges);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task CreateEntity(T entity);
        Task CreateEntities(IEnumerable<T> entities);
        Task UpdateEntity(T entity);
        Task DeleteEntity(T entity);
    }
}
