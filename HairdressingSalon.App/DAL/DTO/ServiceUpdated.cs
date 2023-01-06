namespace HairdressingSalon.App.DAL.DTO
{
    public class ServiceUpdated
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Code { get; set; }
        public int ServiceKindId { get; set; }
        public int OrderId { get; set; }
    }
}
