using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.DAL.DTO
{
    public class WorkerUpdated
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Invalid surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Invalid name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Invalid middle name")]
        public string MiddleName { get; set; }
    }
}
