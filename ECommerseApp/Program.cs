using Core.Interface;
using ECommerseApp.Middleware;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Security.Claims;
using System.Text;

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
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    string connectionUri = configuration.GetSection("ConnectionStrings:connectionUri").Value; // Adjust if needed
    string databaseName = configuration.GetSection("ConnectionStrings:databaseName").Value;

    return new MongoDbContext(connectionUri, databaseName);
});

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



// Register CartService
builder.Services.AddSingleton<ICartService, CartService>();


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