using HairdressingSalon.App.DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.ViewModels
{
    public class FeedbackCreatedModel
    {
        public string Text { get; set; }


        [Required(ErrorMessage = "Invalid mark")]
        [Range(1, 5, ErrorMessage = "Value should be in range from 1 till 5")]
        public int Mark { get; set; }


        public string Order { get; set; }


        [ValidateNever]
        public List<Order> Orders { get; set; }
    }
}
