using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Data;
using ParkingApp.Infrastructure.Repositories;
using ParkingApp.Web;
using ParkingApp.Web.Components;
using ParkingApp.Application.PatternsSingleton;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// dodanie identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ParkingDbContext>()
.AddDefaultTokenProviders();

// 2. Dodaj autoryzacjê i uwierzytelnianie
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// dodanie singletona
builder.Services.AddSingleton<SystemConfiguration>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ParkingDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ParkingDbContext>();

    if (!db.ParkingSpots.Any())
    {
        db.ParkingSpots.AddRange(
            new ParkingSpot { Number = 1, IsVipOnly = false, IsUnderMaintenance = false },
            new ParkingSpot { Number = 2, IsVipOnly = true, IsUnderMaintenance = false },
            new ParkingSpot { Number = 3, IsVipOnly = false, IsUnderMaintenance = true }
        );

        db.SaveChanges();
    }
}

// przyk³adowa baza (chcêzobaczyæ efekt)

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ParkingDbContext>();

    if (!db.ParkingSpots.Any())
    {
        db.ParkingSpots.AddRange(
            new ParkingSpot { Number = 1, IsVipOnly = false, IsUnderMaintenance = false },
            new ParkingSpot { Number = 2, IsVipOnly = true, IsUnderMaintenance = false },
            new ParkingSpot { Number = 3, IsVipOnly = false, IsUnderMaintenance = true }
        );

        db.SaveChanges();
    }
}


app.Run();
