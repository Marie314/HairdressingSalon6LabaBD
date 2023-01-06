using HairdressingSalon.App.DAL.Autocomplete;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairdressingSalon.App.DAL.Database.EntitiesConfiguration
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(DbAutocompleter.Services);
        }
    }
}
