using System.Reflection;
using Hotel.Core.Interfaces;
using Hotel.Core.Mapping;
using Hotel.Core.Mapping.Interfaces;
using Hotel.Core.Services;
using Hotel.Core.Services.Abstract;
using Hotel.Infrastructure.Context;
using Hotel.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Hotel.Api.Extensions;

/// <summary>
/// Класс для DI.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Добавление маппинга.
  /// </summary>
  /// <param name="services"><see cref="IServiceCollection"/>.</param>
  /// <returns><see cref="IServiceCollection"/>.</returns>
  public static IServiceCollection AddMappings(this IServiceCollection services)
  {
    services.AddSingleton<IMapStore, MapStore>();
    services.AddScoped<IMapper, Mapper>();

    return services;
  }

  /// <summary>
  /// Добавление сваггера.
  /// </summary>
  /// <param name="services"><see cref="IServiceCollection"/>.</param>
  /// <returns><see cref="IServiceCollection"/>.</returns>
  public static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Hotel API",
        Version = "v1",
        Description = "API для управления отелем",
        Contact = new OpenApiContact
        {
          Name = "Hotel Team",
          Email = "hotel@example.com",
        },
      });
    });

    return services;
  }

  /// <summary>
  /// Добавление внутренних сервисов.
  /// </summary>
  /// <param name="services"><see cref="IServiceCollection"/>.</param>
  /// <returns><see cref="IServiceCollection"/>.</returns>
  public static IServiceCollection AddHotelServices(this IServiceCollection services)
  {
    services.AddScoped<IRoomService, RoomService>();
    services.AddScoped<IRoomProvider, RoomProvider>();
    return services;
  }

  /// <summary>
  /// Добавление БД.
  /// </summary>
  /// <param name="services"><see cref="IServiceCollection"/>.</param>
  /// <param name="connectionString">Подключение.</param>
  /// <returns><see cref="IServiceCollection"/>.</returns>
  public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
  {
    services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
    return services;
  }

  /// <summary>
  /// Инициализация маппинга.
  /// </summary>
  /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
  /// <returns><see cref="IApplicationBuilder"/>.</returns>
  public static IApplicationBuilder InitializeMappings(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    var mapStore = scope.ServiceProvider.GetRequiredService<IMapStore>();
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("🔍 Поиск конфигураций маппинга...");

    try
    {
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      var totalConfigs = 0;

      foreach (var assembly in assemblies)
      {
        if (assembly.FullName?.StartsWith("System.") == true ||
            assembly.FullName?.StartsWith("Microsoft.") == true ||
            assembly.FullName?.StartsWith("netstandard") == true)
        {
          continue;
        }

        try
        {
          var configTypes = assembly.GetTypes()
              .Where(t => t.IsClass &&
                         !t.IsAbstract &&
                         typeof(IMapperConfig).IsAssignableFrom(t))
              .ToList();

          if (configTypes.Any())
          {
            logger.LogDebug("Сканируем сборку: {AssemblyName}", assembly.GetName().Name);

            foreach (var configType in configTypes)
            {
              try
              {
                var config = (IMapperConfig?)Activator.CreateInstance(configType);
                config?.AddMaps(mapStore, mapper);
                logger.LogInformation("✅ Загружен маппинг: {ConfigName}", configType.Name);
                totalConfigs++;
              }
              catch (Exception ex)
              {
                logger.LogError(ex, "❌ Ошибка загрузки {ConfigName}", configType.Name);
              }
            }
          }
        }
        catch (ReflectionTypeLoadException ex)
        {
          logger.LogWarning(ex, "Ошибка загрузки типов из {AssemblyName}", assembly.GetName().Name);
        }
        catch (Exception ex)
        {
          logger.LogWarning(ex, "Ошибка сканирования {AssemblyName}", assembly.GetName().Name);
        }
      }

      logger.LogInformation("🎯 Загружено конфигураций маппинга: {TotalConfigs}", totalConfigs);
    }
    catch (Exception ex)
    {
      logger.LogCritical(ex, "💥 Критическая ошибка инициализации маппингов");
      throw;
    }

    return app;
  }
}
