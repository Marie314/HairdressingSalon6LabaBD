using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.DAL.DTO
{
    public class FeedbackUpdated
    {
        public int Id { get; set; }
        public string Text { get; set; }


        [Required(ErrorMessage = "Invalid mark")]
        [Range(1, 5, ErrorMessage = "Value should be in range from 1 till 5")]
        public int Mark { get; set; }
    }
}
