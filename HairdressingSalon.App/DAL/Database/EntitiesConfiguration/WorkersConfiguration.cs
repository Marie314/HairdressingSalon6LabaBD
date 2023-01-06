using HairdressingSalon.App.DAL.Autocomplete;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairdressingSalon.App.DAL.Database.EntitiesConfiguration
{
    public class WorkersConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasData(DbAutocompleter.Workers);
        }
    }
}
