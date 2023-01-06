using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HairdressingSalon.App.DAL.Database.Repositories
{
    public class OrdersRepository : BaseRepository<Order>, IOrdersRepository
    {
        private readonly IMemoryCache _memoryCache;

        public OrdersRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
            : base(dbContext)
        {
            _memoryCache = memoryCache;
        }

        public async Task Create(Order model) =>
            await CreateEntity(model);

        public async Task Create(IEnumerable<Order> models) =>
            await CreateEntities(models);

        public async Task Delete(Order model) =>
            await DeleteEntity(model);

        public async Task<IEnumerable<Order>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).Include(o => o.Client).Include(o => o.Worker).ToListAsync();

        public async Task<Order> GetById(int id, bool trackChanges) =>
            await GetByCondition(x => x.Id.Equals(id), trackChanges).Include(o => o.Client).Include(o => o.Worker).SingleOrDefaultAsync();

        public async Task Update(Order model) =>
            await UpdateEntity(model);

        public async Task<IEnumerable<Order>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Order> models))
            {
                models = await dbContext.Orders.Take(rowsCount).Include(o => o.Client).Include(o => o.Worker).ToListAsync();
                if (models != null)
                {
                    _memoryCache.Set(cacheKey, models,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachingTime)));
                }
            }
            return models;
        }
    }
}
