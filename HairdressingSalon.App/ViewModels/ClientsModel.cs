using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.ViewModels
{
    public class ClientsModel
    {
        public List<Client> Clients { get; set; }
        public List<ServiceKind> ServiceKinds { get; set; }
    }
}
