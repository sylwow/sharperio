using dotnet_rpg.Mapper;
using dotnet_rpg.Services.CharacterService;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;
using dotnet_rpg.Repositories;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using dotnet_rpg.Services.UserContext;
using dotnet_rpg.Services.WeaponService;
using dotnet_rpg.Services.FightService;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<DataContext>(options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<DataContext>();


services.AddAutoMapper(typeof(Mappings));
services.AddScoped<ICharacterService, CharacterService>();
services.AddScoped<IAuthRepository, AuthRepository>();
services.AddScoped<IUserContext, UserContext>();
services.AddScoped<IWeaponService, WeaponService>();
services.AddScoped<IFightService, FightService>();
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.AddControllers();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Sdandard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
    {
        builder.WithOrigins("*");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
