using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.Cookie;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.JWTToken;
using Practice_Store.Application.ServiceCollection;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;
using Practice_Store.Infrastructure.Cookie;
using Practice_Store.Infrastructure.JWTToken;
using Practice_Store.Persistence.Contexts;
using Practice_Store.Persistence.RepositoryManager.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5215")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.OrderManagement_Admin, policy => policy.RequireRole(UserRoles.OrderManagement_Admin));
    options.AddPolicy(UserRoles.UserManagement_Admin, policy => policy.RequireRole(UserRoles.UserManagement_Admin));
    options.AddPolicy(UserRoles.ProductManagement_Admin, policy => policy.RequireRole(UserRoles.ProductManagement_Admin));
    options.AddPolicy(UserRoles.SiteManagement_Admin, policy => policy.RequireRole(UserRoles.SiteManagement_Admin));
});

builder.Services.AddIdentity<IdtUser, IdtRole>(options =>
{
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
}).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Register/LogIn");
    options.AccessDeniedPath = new PathString("/Error/AccessDenied");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});


builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IGenerateToken, Generatetoken>();
builder.Services.AddScoped<IReadToken, ReadToken>();
builder.Services.AddScoped<IManageCookie, ManageCookie>();
builder.Services.UserManagementServices().ProductManagementServices().LandingPageManagementServices().CartManagementServices().OrderManagementServices();
builder.Services.RepositoriesServices().ProductRepositiryServices();

string _ConnectionString = @"Data Source=LENOVO-THINKBOO\SQLEXPRESS; Initial Catalog=Practice_Store_DB; Integrated Security=True; TrustServerCertificate=True;";
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(option => option.UseSqlServer(_ConnectionString));

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 100_000_000;
});

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

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("/Error/Notfound");
    }
});

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
