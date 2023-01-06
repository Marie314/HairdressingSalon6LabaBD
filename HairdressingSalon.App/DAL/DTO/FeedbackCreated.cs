using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.DAL.DTO
{
    public class FeedbackCreated
    {
        public string Text { get; set; }
        public int Mark { get; set; }
        public DateTime DateTime { get; set; }
        public int OrderId { get; set; }
    }
}
