using AutoMapper;
using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.BLL.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServicesService(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Service> Create(ServiceCreated modelCreated)
        {
            var entity = _mapper.Map<Service>(modelCreated);

            await _repositoryManager.ServicesRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoryManager.ServicesRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoryManager.ServicesRepository.Delete(entity);

            return true;
        }

        public async Task<IEnumerable<Service>> GetAll() =>
            await _repositoryManager.ServicesRepository.GetAll(false);

        public async Task<Service> GetById(int id) =>
            await _repositoryManager.ServicesRepository.GetById(id, false);

        public async Task<bool> Update(ServiceUpdated modelUpdated)
        {
            var entity = await _repositoryManager.ServicesRepository.GetById(modelUpdated.Id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(modelUpdated, entity);

            await _repositoryManager.ServicesRepository.Update(entity);

            return true;
        }

        public async Task<IEnumerable<Service>> Get(int rowsCount, string cacheKey) =>
            await _repositoryManager.ServicesRepository.Get(rowsCount, cacheKey);
    }
}
