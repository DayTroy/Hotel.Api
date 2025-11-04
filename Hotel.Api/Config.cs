using Hotel.Core.Interfaces;
using Hotel.Core.Services;
using Hotel.Core.Services.Abstract;
using Hotel.Infrastructure.Context;
using Hotel.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Hotel.Api;

/// <summary>
/// Config.
/// </summary>
public class Config
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Config"/> class.
  /// </summary>
  /// <param name="configuration">The application configuration.</param>
  public Config(IConfiguration configuration)
  {
    this.Configuration = configuration;
  }

  /// <summary>
  /// Gets the application configuration.
  /// </summary>
  public IConfiguration Configuration { get; }

  /// <summary>
  /// Configures the services for the application.
  /// </summary>
  /// <param name="services">The application configuration.</param>
  public void ConfigureServices(IServiceCollection services)
  {
    ConfigureExternal(services);
    ConfigureInternal(services);
    services.AddControllers();
  }

  private void ConfigureExternal(IServiceCollection services)
  {
    services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));

    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Hotel API",
        Version = "v1",
        Description = "A simple hotel management API",
      });
    });
  }

  private void ConfigureInternal(IServiceCollection services)
  {
    services.AddScoped<IRoomService, RoomService>();
    services.AddScoped<IRoomProvider, RoomProvider>();
  }

  /// <summary>
  /// Configures the application pipeline.
  /// </summary>
  /// <param name="app">The application configuration.</param>
  /// <param name="env">The application environment.</param>
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    // Other configurations
  }
}
