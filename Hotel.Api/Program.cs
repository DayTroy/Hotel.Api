using Hotel.Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddHotelServices()
    .AddCustomMappings()
    .AddCustomSwagger()
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
