using PP_PI_Backend.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(",");

var connectionStrings = builder.Configuration.GetConnectionString("PostgreSQLConnection"); // Setting the connection string for Postgre

builder.Services.AddDbContext<LibraryDb>(options => options.UseNpgsql(connectionStrings)); // Setting the db context
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
