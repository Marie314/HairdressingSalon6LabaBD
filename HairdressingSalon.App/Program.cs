using AutoMapper;
using Flurl.Http.Configuration;
using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL;
using HairdressingSalon.App.DAL.Database;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"), b =>
        b.MigrationsAssembly("HairdressingSalon.App")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IWorkersService, WorkersService>();
builder.Services.AddScoped<IFeedbacksService, FeedbacksService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IServiceKindsService, ServiceKindsService>();
builder.Services.AddScoped<IServicesService, ServicesService>();

builder.Services.AddAutoMapper(config => config.AddProfile(new MappingConfig()));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
