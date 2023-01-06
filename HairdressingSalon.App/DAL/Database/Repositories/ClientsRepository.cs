using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HairdressingSalon.App.DAL.Database.Repositories
{
    public class ClientsRepository : BaseRepository<Client>, IClientsRepository
    {
        private readonly IMemoryCache _memoryCache;

        public ClientsRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
            : base(dbContext)
        {
            _memoryCache = memoryCache;
        }

        public async Task Create(Client model) =>
            await CreateEntity(model);

        public async Task Delete(Client model) =>
            await DeleteEntity(model);

        public async Task<IEnumerable<Client>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).ToListAsync();

        public async Task<Client> GetById(int id, bool trackChanges) =>
            await GetByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task Update(Client model) =>
            await UpdateEntity(model);

        public async Task<IEnumerable<Client>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Client> models))
            {
                models = await dbContext.Clients.Take(rowsCount).ToListAsync();
                if (models != null)
                {
                    _memoryCache.Set(cacheKey, models,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachingTime)));
                }
            }
            return models;
        }

        public async Task Create(IEnumerable<Client> models) =>
            await CreateEntities(models);
    }
}
