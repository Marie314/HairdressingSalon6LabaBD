namespace HairdressingSalon.App.DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public int Discount { get; set; }
    }
}
