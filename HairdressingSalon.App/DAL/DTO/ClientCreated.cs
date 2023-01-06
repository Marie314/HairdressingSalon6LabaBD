using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.DAL.DTO
{
    public class ClientCreated
    {
        [Required(ErrorMessage = "Invalid surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Invalid name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Invalid middle name")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "Invalid address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Invalid telephone")]
        public string Telephone { get; set; }


        [Required(ErrorMessage = "Invalid discount")]
        [Range(0, 50, ErrorMessage = "Value should be in range from 0 till 50")]
        public int Discount { get; set; }
    }
}
