using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args); //creates Kestral server -> reads configuration from configuration files
//configuration files are appsettings.Development.json and appsettings.json

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicatioServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline. == Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope(); //when its finished with scope it will be destroyed before next code
var services = scope.ServiceProvider;

try
{//try to create database
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}
app.Run();
