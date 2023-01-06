using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HairdressingSalon.App.DAL.Database.Repositories
{
    public class ServiceKindsRepository : BaseRepository<ServiceKind>, IServiceKindsRepository
    {
        private readonly IMemoryCache _memoryCache;

        public ServiceKindsRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
            : base(dbContext)
        {
            _memoryCache = memoryCache;
        }

        public async Task Create(ServiceKind model) =>
            await CreateEntity(model);

        public async Task Create(IEnumerable<ServiceKind> models) =>
            await CreateEntities(models);

        public async Task Delete(ServiceKind model) =>
            await DeleteEntity(model);

        public async Task<IEnumerable<ServiceKind>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).ToListAsync();

        public async Task<ServiceKind> GetById(int id, bool trackChanges) =>
            await GetByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task Update(ServiceKind model) =>
            await UpdateEntity(model);

        public async Task<IEnumerable<ServiceKind>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ServiceKind> models))
            {
                models = await dbContext.ServiceKinds.Take(rowsCount).ToListAsync();
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
