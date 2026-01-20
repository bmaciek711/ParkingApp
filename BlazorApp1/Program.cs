using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkingApp.Application.PatternsSingleton;
using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Data;
using ParkingApp.Infrastructure.Repositories;
using ParkingApp.Web;
using ParkingApp.Web.Components;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ParkingDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
})
    .AddIdentityCookies(); 

builder.Services.AddIdentityCore<IdentityUser>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddRoles<IdentityRole>() 
.AddEntityFrameworkStores<ParkingDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

//poprawa przekirowania w przypadku niezalogowanego u¿ytkownika
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/login";  
});
builder.Services.AddAuthorization();


builder.Services.AddSingleton<SystemConfiguration>(); 
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication(); 
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ParkingDbContext>();
    db.Database.EnsureCreated(); 

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