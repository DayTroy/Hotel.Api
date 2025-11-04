using System.Reflection;
using Hotel.Core.Mapping.Abstract;
using Mapster;

namespace Hotel.Core.Mapping;

/// <summary>
/// Маппер.
/// </summary>
public class Mapper : IMapper
{
  private readonly IMapStore _mapStore;

  /// <summary>
  /// Initializes a new instance of the <see cref="Mapper"/> class.
  /// </summary>
  /// <param name="mapStore"><see cref="IMapStore"/>.</param>
  public Mapper(IMapStore mapStore)
  {
    _mapStore = mapStore;
  }

  /// <summary>
  /// .
  /// </summary>
  /// <param name="source">.</param>
  /// <typeparam name="TSource"> .</typeparam>
  /// <typeparam name="TDestination">.</typeparam>
  /// <returns>.</returns>
  public TDestination Map<TSource, TDestination>(TSource source)
  {
    if (source == null)
    {
      return default;
    }

    // 1. Пытаемся найти кастомный маппинг
    if (_mapStore.HasMap<TSource, TDestination>())
    {
      Console.WriteLine($"Custom");
      var mapFunc = _mapStore.GetMap<TSource, TDestination>();
      return mapFunc(source);
    }

    // 2. Пытаемся использовать Mapster
    try
    {
      Console.WriteLine($"Mapster default");
      return source.Adapt<TSource, TDestination>();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Mapster failed: {ex.Message}");

      // 3. Fallback на reflection
      return MapViaReflection<TSource, TDestination>(source);
    }
  }

  public TDestination Map<TDestination>(object source)
  {
    if (source == null) return default;

    var sourceType = source.GetType();
    var method = typeof(Mapper).GetMethod(nameof(Map), 1, BindingFlags.Public | BindingFlags.Instance, null, new[] { sourceType }, null)
        ?.MakeGenericMethod(sourceType, typeof(TDestination));

    if (method != null)
    {
      return (TDestination)method.Invoke(this, new[] { source });
    }

    throw new InvalidOperationException($"Cannot map {sourceType} to {typeof(TDestination)}");
  }

  public List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
  {
    return source?.Select(item => Map<TSource, TDestination>(item)).ToList()
           ?? new List<TDestination>();
  }

  private TDestination MapViaReflection<TSource, TDestination>(TSource source)
  {
    try
    {
      var destination = Activator.CreateInstance<TDestination>();
      var sourceType = typeof(TSource);
      var destinationType = typeof(TDestination);

      var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
      var destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (var sourceProp in sourceProperties)
      {
        if (!sourceProp.CanRead) continue;

        // Игнорируем регистр при сравнении
        var destProp = destinationProperties.FirstOrDefault(p =>
            string.Equals(p.Name, sourceProp.Name, StringComparison.OrdinalIgnoreCase) &&
            p.PropertyType == sourceProp.PropertyType &&
            p.CanWrite);

        if (destProp != null)
        {
          var value = sourceProp.GetValue(source);
          destProp.SetValue(destination, value);
        }
      }

      return destination;
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Error mapping {typeof(TSource)} to {typeof(TDestination)}", ex);
    }
  }
}
