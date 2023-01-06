using HairdressingSalon.App.DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HairdressingSalon.App.ViewModels
{
    public class ServiceUpdatedModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Invalid code")]
        [Range(10000, 100000, ErrorMessage = "Value should be in range from 10000 till 99999")]
        public int Code { get; set; }


        [Required(ErrorMessage = "Invalid price")]
        [Range(25, 200, ErrorMessage = "Value should be in range from 25 till 200")]
        public decimal Price { get; set; }



        public string ServiceKind { get; set; }
        public string Order { get; set; }


        [ValidateNever]
        public List<ServiceKind> ServiceKinds { get; set; }

        [ValidateNever]
        public List<Order> Orders { get; set; }
    }
}
