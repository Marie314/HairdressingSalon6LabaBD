namespace HairdressingSalon.App.DAL.DTO
{
    public class OrderUpdated
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ClientId { get; set; }
        public int WorkerId { get; set; }
    }
}
