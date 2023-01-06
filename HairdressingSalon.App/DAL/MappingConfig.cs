using AutoMapper;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ClientCreated, Client>();
            CreateMap<ClientUpdated, Client>();

            CreateMap<FeedbackCreated, Feedback>();
            CreateMap<FeedbackUpdated, Feedback>();

            CreateMap<OrderCreated, Order>();
            CreateMap<OrderUpdated, Order>();

            CreateMap<ServiceKindCreated, ServiceKind>();
            CreateMap<ServiceKindUpdated, ServiceKind>();

            CreateMap<ServiceCreated, Service>();
            CreateMap<ServiceUpdated, Service>();

            CreateMap<WorkerCreated, Worker>();
            CreateMap<WorkerUpdated, Worker>();
        }
    }
}
