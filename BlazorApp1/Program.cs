using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Core.Settings;
using ParkingApp.Infrastructure.Data;
using ParkingApp.Web;
using ParkingApp.Web.Components;
using ParkingApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<AccountRequirementService>();



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

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/login";  
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();
builder.Services.AddSingleton<SystemConfiguration>();

builder.Services.AddScoped<ReservationMediator>();

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

app.MapPost("/Account/Logout", async (
    SignInManager<IdentityUser> signInManager,
    [FromForm] string returnUrl) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect(returnUrl ?? "/");
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<ParkingDbContext>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    db.Database.EnsureCreated();

    // Inicjalizacja Ról (Wymagane do sprawdzania uprawnieñ)
    string[] roles = { "Admin", "VIP", "User" };
    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
        }
    }

    // Nadanie uprawnieñ Admina
    var adminEmail = "test@test.pl";
    // Szukamy u¿ytkownika
    var user = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

    if (user != null)
    {
        // Sprawdzamy czy rola Admin istnieje
        if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
        }

        // Nadajemy rolê, jeœli u¿ytkownik jej nie ma
        var isInRole = userManager.IsInRoleAsync(user, "Admin").GetAwaiter().GetResult();
        if (!isInRole)
        {
            userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }
    }

    // Inicjalizacja Miejsc Parkingowych 
    if (!db.ParkingSpots.Any())
    {
        db.ParkingSpots.AddRange(
            new ParkingSpot { Number = 1, IsVipOnly = false, SpotType = "Samochód", IsUnderMaintenance = true },
            new ParkingSpot { Number = 2, IsVipOnly = true, SpotType = "Samochód", IsUnderMaintenance = false },
            new ParkingSpot { Number = 3, IsVipOnly = false, SpotType = "Motocykl", IsUnderMaintenance = false },
            new ParkingSpot { Number = 4, IsVipOnly = false, SpotType = "Rower", IsUnderMaintenance = false }
        );
        db.SaveChanges();
    }
}

app.Run();