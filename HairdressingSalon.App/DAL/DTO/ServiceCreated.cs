namespace HairdressingSalon.App.DAL.DTO
{
    public class ServiceCreated
    {
        public int Code { get; set; }
        public decimal Price { get; set; }
        public int ServiceKindId { get; set; }
        public int OrderId { get; set; }
    }
}
