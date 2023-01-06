using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.DAL.Database.Repositories;
using HairdressingSalon.App.DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HairdressingSalon.App.DAL.Database
{
    public class RepositoryManager : IRepositoryManager
    {
        private ApplicationDbContext _dbContext;
        private IMemoryCache _memoryCache;

        private IClientsRepository _clientsRepository;
        private IFeedbacksRepository _feedbacksRepository;
        private IOrdersRepository _ordersRepository;
        private IServiceKindsRepository _serviceKindsRepository;
        private IServicesRepository _servicesRepository;
        private IWorkersRepository _workersRepository;

        public RepositoryManager(ApplicationDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public IClientsRepository ClientsRepository
        {
            get
            {
                if (_clientsRepository == null)
                {
                    _clientsRepository = new ClientsRepository(_dbContext, _memoryCache);
                }
                return _clientsRepository;
            }
        }

        public IFeedbacksRepository FeedbacksRepository
        {
            get
            {
                if (_feedbacksRepository == null)
                {
                    _feedbacksRepository = new FeedbacksRepository(_dbContext, _memoryCache);
                }
                return _feedbacksRepository;
            }
        }

        public IOrdersRepository OrdersRepository
        {
            get
            {
                if (_ordersRepository == null)
                {
                    _ordersRepository = new OrdersRepository(_dbContext, _memoryCache);
                }
                return _ordersRepository;
            }
        }

        public IServicesRepository ServicesRepository
        {
            get
            {
                if (_servicesRepository == null)
                {
                    _servicesRepository = new ServicesRepository(_dbContext, _memoryCache);
                }
                return _servicesRepository;
            }
        }

        public IWorkersRepository WorkersRepository
        {
            get
            {
                if (_workersRepository == null)
                {
                    _workersRepository = new WorkersRepository(_dbContext, _memoryCache);
                }
                return _workersRepository;
            }
        }

        public IServiceKindsRepository ServiceKindsRepository
        {
            get
            {
                if (_serviceKindsRepository == null)
                {
                    _serviceKindsRepository = new ServiceKindsRepository(_dbContext, _memoryCache);
                }
                return _serviceKindsRepository;
            }
        }
    }
}
