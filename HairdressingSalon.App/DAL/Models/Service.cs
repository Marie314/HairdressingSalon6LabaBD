namespace HairdressingSalon.App.DAL.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public decimal Price { get; set; }
        public int ServiceKindId { get; set; }
        public int OrderId { get; set; }
        public ServiceKind ServiceKind { get; set; }
        public Order Order { get; set; }
    }
}
