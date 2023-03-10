using HairdressingSalon.App.DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HairdressingSalon.App.ViewModels
{
    public class OrderUpdatedModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Client { get; set; }
        public string Worker { get; set; }


        [ValidateNever]
        public List<Client> Clients { get; set; }


        [ValidateNever]
        public List<Worker> Workers { get; set; }

        [ValidateNever]
        public Order OldOrder { get; set; }
    }
}
