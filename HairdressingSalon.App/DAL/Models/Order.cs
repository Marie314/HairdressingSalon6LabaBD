namespace HairdressingSalon.App.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ClientId { get; set; }
        public int WorkerId { get; set; }
        public Client Client { get; set; }
        public Worker Worker { get; set; }
    }
}
