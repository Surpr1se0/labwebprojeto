using labwebprojeto.Data;
using labwebprojeto.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Services;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add IdentityUsers and IdentityRoles to the Database                                            //Enable email confirm
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add Settings to the Identity Auth
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Add Cloudinary API Service
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IPhotoService, PhotoService>();

// Add API Google Services
var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

// Add Microsoft API Services
services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
    microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
});

// Add API Twitter Services
services.AddAuthentication().AddTwitter(twitterOptions =>
{
    twitterOptions.ConsumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];
    twitterOptions.ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
});

// Add Email Sender
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

// Add Email Notifications
builder.Services.AddTransient<IEmailService, SendGridMailService>();

// Add Role Manager
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Add Scoped RoleManager
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    SeedRoles.Seed(roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
