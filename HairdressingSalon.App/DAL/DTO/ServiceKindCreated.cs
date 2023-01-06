using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.DAL.DTO
{
    public class ServiceKindCreated
    {
        [Required(ErrorMessage = "Invalid name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Invalid description")]
        public string Description { get; set; }


        public string ImageUrl { get; set; }
    }
}
