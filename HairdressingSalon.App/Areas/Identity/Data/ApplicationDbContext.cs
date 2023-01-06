using HairdressingSalon.App.DAL.Autocomplete;
using HairdressingSalon.App.DAL.Database.EntitiesConfiguration;
using HairdressingSalon.App.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HairdressingSalon.App.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceKind> ServiceKinds { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        DbAutocompleter.GenerateRandomValues();

        modelBuilder.ApplyConfiguration(new ClientsConfiguration());
        modelBuilder.ApplyConfiguration(new FeedbacksConfiguration());
        modelBuilder.ApplyConfiguration(new OrdersConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceKindsConfiguration());
        modelBuilder.ApplyConfiguration(new ServicesConfiguration());
        modelBuilder.ApplyConfiguration(new WorkersConfiguration());
    }
}
