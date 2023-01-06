using AutoMapper;
using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.BLL.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ClientsService(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Client> Create(ClientCreated modelCreated)
        {
            var entity = _mapper.Map<Client>(modelCreated);

            await _repositoryManager.ClientsRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoryManager.ClientsRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoryManager.ClientsRepository.Delete(entity);

            return true;
        }

        public async Task<IEnumerable<Client>> GetAll() =>
            await _repositoryManager.ClientsRepository.GetAll(false);

        public async Task<Client> GetById(int id) =>
            await _repositoryManager.ClientsRepository.GetById(id, false);

        public async Task<bool> Update(ClientUpdated modelUpdated)
        {
            var entity = await _repositoryManager.ClientsRepository.GetById(modelUpdated.Id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(modelUpdated, entity);

            await _repositoryManager.ClientsRepository.Update(entity);

            return true;
        }

        public async Task<IEnumerable<Client>> Get(int rowsCount, string cacheKey) =>
            await _repositoryManager.ClientsRepository.Get(rowsCount, cacheKey);
    }
}
