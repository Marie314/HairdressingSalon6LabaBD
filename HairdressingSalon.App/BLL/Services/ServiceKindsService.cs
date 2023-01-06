using AutoMapper;
using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.BLL.Services
{
    public class ServiceKindsService : IServiceKindsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServiceKindsService(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ServiceKind> Create(ServiceKindCreated modelCreated)
        {
            var entity = _mapper.Map<ServiceKind>(modelCreated);

            await _repositoryManager.ServiceKindsRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoryManager.ServiceKindsRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoryManager.ServiceKindsRepository.Delete(entity);

            return true;
        }

        public async Task<IEnumerable<ServiceKind>> GetAll() =>
            await _repositoryManager.ServiceKindsRepository.GetAll(false);

        public async Task<ServiceKind> GetById(int id) =>
            await _repositoryManager.ServiceKindsRepository.GetById(id, false);

        public async Task<bool> Update(ServiceKindUpdated modelUpdated)
        {
            var entity = await _repositoryManager.ServiceKindsRepository.GetById(modelUpdated.Id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(modelUpdated, entity);

            await _repositoryManager.ServiceKindsRepository.Update(entity);

            return true;
        }

        public async Task<IEnumerable<ServiceKind>> Get(int rowsCount, string cacheKey) =>
            await _repositoryManager.ServiceKindsRepository.Get(rowsCount, cacheKey);
    }
}
