using PP_PI_Backend.Data;
using Microsoft.EntityFrameworkCore;
using System;




var builder = WebApplication.CreateBuilder(args);

//var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(",");
var allowedOrigins = builder.Configuration
    .GetValue<string>("AllowedOrigins")?
    .Split(",")
    ?? new[] { "*" };


//var connectionStrings = builder.Configuration.GetConnectionString("PostgreSQLConnection"); // Setting the connection string for Postgre

//builder.Services.AddDbContext<LibraryDb>(options => 
// options.UseNpgsql(connectionStrings)); // Setting the db context

builder.Services.AddDbContext<LibraryDb>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDb>();
    db.Database.Migrate();
}


app.UseCors();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
