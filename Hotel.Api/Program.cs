using Hotel.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/hotel.txt")
    .CreateLogger();

var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

builder.Services
    .AddDatabase(connectionString)
    .AddHotelServices()
    .AddMappings()
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app.InitializeMappings();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API v1");
    options.RoutePrefix = string.Empty;
  });
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
