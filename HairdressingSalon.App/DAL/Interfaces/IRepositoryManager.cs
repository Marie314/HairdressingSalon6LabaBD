namespace HairdressingSalon.App.DAL.Interfaces
{
    public interface IRepositoryManager
    {
        public IClientsRepository ClientsRepository { get; }
        public IFeedbacksRepository FeedbacksRepository { get; }
        public IOrdersRepository OrdersRepository { get; }
        public IServiceKindsRepository ServiceKindsRepository { get; }
        public IServicesRepository ServicesRepository { get; }
        public IWorkersRepository WorkersRepository { get; }
    }
}
