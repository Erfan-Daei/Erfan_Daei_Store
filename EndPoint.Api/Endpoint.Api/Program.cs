using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:7007")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin_Only", policy =>
    {
        policy.RequireAssertion(context => !context.User.IsInRole(UserRoles.Customer));
    });
    options.AddPolicy("UserMangementAdmins", policy => policy.RequireRole(UserRoles.Admin, UserRoles.UserManagement_Admin));
    options.AddPolicy("ProductManagementAdmins", policy => policy.RequireRole(UserRoles.Admin, UserRoles.ProductManagement_Admin));
    options.AddPolicy("OrderManagementAdmins", policy => policy.RequireRole(UserRoles.Admin, UserRoles.OrderManagement_Admin));
    options.AddPolicy("SiteManagementAdmins", policy => policy.RequireRole(UserRoles.Admin, UserRoles.SiteManagement_Admin));
    options.AddPolicy("Customer&Admin", policy => policy.RequireRole(UserRoles.Admin, UserRoles.Customer));
});

builder.Services.AddIdentity<IdtUser, IdtRole>(options =>
{
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
}).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(configureOptions =>
    {
        configureOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JWTConfig:issuer"],
            ValidAudience = builder.Configuration["JWTConfig:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:key"])),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RoleClaimType = ClaimTypes.Role,
        };
        configureOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Token failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };

        configureOptions.SaveToken = true;
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var security = new OpenApiSecurityScheme
    {
        Name = "Jwt Authentication",
        Description = "Insert Your JwtToken",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    sw.AddSecurityDefinition(security.Reference.Id, security);
    sw.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {security, new string[]{ } }
    });
});

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IGenerateToken, Generatetoken>();
builder.Services.AddScoped<IReadToken, ReadToken>();
builder.Services.AddScoped<IManageCookie, ManageCookie>();
builder.Services.UserManagementServices().ProductManagementServices().LandingPageManagementServices().CartManagementServices().OrderManagementServices();

string _ConnectionString = @"Data Source=LENOVO-THINKBOO\SQLEXPRESS; Initial Catalog=Practice_Store_DB; Integrated Security=True; TrustServerCertificate=True;";
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(option => option.UseSqlServer(_ConnectionString));

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 100_000_000;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "Admin",
            pattern: "api/Admin/{controller=UserManager}/{action=GET}",
            defaults: new { area = "Admin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeManager}/{action=Get}/{id?}");

app.Run();
