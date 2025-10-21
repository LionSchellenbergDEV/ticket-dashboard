using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ticket_dashboard;
using ticket_dashboard.Models;
using ticket_dashboard.Repositories.Interfaces;
using ticket_dashboard.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString)
);

builder.Services.Configure<MailSettings>(
    builder.Configuration.GetSection("MailSettings"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();

// Fügen Sie diese Zeilen in Program.cs hinzu:

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Nutzer werden nach 30min aus Sicherheitsgründen automatisch abgemeldet. 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;

        // Definiert den Pfad, zu dem unauthentifizierte Benutzer umgeleitet werden
        options.LoginPath = "/Auth/Login";

        // Definiert den Pfad, zu dem Benutzer ohne Berechtigung umgeleitet werden
        options.AccessDeniedPath = "/Auth/AccessDenied";

        // Definiert die Dauer des Cookies
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Stellen Sie sicher, dass dies registriert ist, da Sie Controller verwenden
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
