using Core.Interface;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Security.Claims;
using System.Text;
using Core.Services;
using Infrastructure.Data.Context;
using Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(); // Add this to enable console logging.


builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// for appsettings.json file access...
var configuration = builder.Configuration;

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddCors();

// add logger for debug easy...
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
            // RoleClaimType = "role" // Specify custom claim type for roles
        };
    });

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var connString = builder.Configuration.GetConnectionString("Redis");

    if (string.IsNullOrEmpty(connString))
    {
        throw new Exception("Cannot get Redis connection string.");
    }

    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable authentication and authorization middleware

app.UseHttpsRedirection();
// app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4201", "http://localhost:4200"));

// Middleware to handle authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();


// swagger run url
// http://localhost:4201/swagger
// http://localhost:4201/swagger/index.html