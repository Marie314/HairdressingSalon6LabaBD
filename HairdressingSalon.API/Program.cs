using AutoMapper;
using Flurl.Http.Configuration;
using HairdressingSalon.App.Areas.Identity.Data;
using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.Database;
using HairdressingSalon.App.DAL.Interfaces;
using HairdressingSalon.App.DAL;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();