using HairdressingSalon.App.DAL.Autocomplete;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairdressingSalon.App.DAL.Database.EntitiesConfiguration
{
    public class ServiceKindsConfiguration : IEntityTypeConfiguration<ServiceKind>
    {
        public void Configure(EntityTypeBuilder<ServiceKind> builder)
        {
            builder.HasData(DbAutocompleter.ServiceKinds);
        }
    }
}
