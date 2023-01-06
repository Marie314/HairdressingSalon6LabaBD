using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HairdressingSalon.App.DAL.Database.Repositories
{
    public class WorkersRepository : BaseRepository<Worker>, IWorkersRepository
    {
        private readonly IMemoryCache _memoryCache;

        public WorkersRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
            : base(dbContext)
        {
            _memoryCache = memoryCache;
        }

        public async Task Create(Worker model) =>
            await CreateEntity(model);

        public async Task Create(IEnumerable<Worker> models) =>
            await CreateEntities(models);

        public async Task Delete(Worker model) =>
            await DeleteEntity(model);

        public async Task<IEnumerable<Worker>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).ToListAsync();

        public async Task<Worker> GetById(int id, bool trackChanges) =>
            await GetByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task Update(Worker model) =>
            await UpdateEntity(model);

        public async Task<IEnumerable<Worker>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Worker> models))
            {
                models = await dbContext.Workers.Take(rowsCount).ToListAsync();
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
