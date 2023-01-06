using HairdressingSalon.App.DAL.Autocomplete;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairdressingSalon.App.DAL.Database.EntitiesConfiguration
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(DbAutocompleter.Orders);
        }
    }
}
