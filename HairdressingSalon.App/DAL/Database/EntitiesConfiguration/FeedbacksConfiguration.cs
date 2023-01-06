using HairdressingSalon.App.DAL.Autocomplete;
using HairdressingSalon.App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairdressingSalon.App.DAL.Database.EntitiesConfiguration
{
    public class FeedbacksConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasData(DbAutocompleter.Feedbacks);
        }
    }
}
