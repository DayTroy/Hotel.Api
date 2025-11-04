using System.Reflection;
using Hotel.Core.Mapping.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Hotel.Core.Mapping;

// TODO:  Добавить логгер.

/// <summary>
/// Гибридный маппер, объединяющий кастомные маппинги и Mapster.
/// Приоритет отдается кастомным маппингам, затем используется Mapster.
/// </summary>
public class Mapper : IMapper
{
  private readonly IMapStore _mapStore;

  /// <summary>
  /// Initializes a new instance of the <see cref="Mapper"/> class.
  /// </summary>
  /// <param name="mapStore">Хранилище конфигураций маппинга.</param>
  public Mapper(IMapStore mapStore)
  {
    _mapStore = mapStore;
  }

  /// <summary>
  /// Выполняет маппинг объекта типа <typeparamref name="TSource"/> в объект типа <typeparamref name="TDestination"/>.
  /// Сначала проверяет наличие кастомного маппинга, затем использует Mapster.
  /// </summary>
  /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
  /// <typeparam name="TDestination">Тип целевого объекта.</typeparam>
  /// <param name="source">Исходный объект для преобразования.</param>
  /// <returns>Новый объект типа <typeparamref name="TDestination"/>.</returns>
  /// <exception cref="InvalidOperationException">Когда маппинг невозможен.</exception>
  public TDestination Map<TSource, TDestination>(TSource source)
  {
    if (source == null)
    {
      return default;
    }

    // 1. Пытаемся найти кастомный маппинг
    if (_mapStore.HasMap<TSource, TDestination>())
    {
      var mapFunc = _mapStore.GetMap<TSource, TDestination>();
      return mapFunc(source);
    }

    // 2. Используем Mapster для автоматического маппинга
    try
    {
      return source.Adapt<TSource, TDestination>();
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException(
          $"Не удалось выполнить маппинг {typeof(TSource)} -> {typeof(TDestination)}. " +
          $"Кастомный маппинг не зарегистрирован и Mapster не справился. " +
          $"Ошибка: {ex.Message}", ex);
    }
  }

  /// <summary>
  /// Выполняет маппинг объекта в указанный тип <typeparamref name="TDestination"/>.
  /// Использует reflection для определения типа исходного объекта.
  /// </summary>
  /// <typeparam name="TDestination">Тип целевого объекта.</typeparam>
  /// <param name="source">Исходный объект для преобразования.</param>
  /// <returns>Новый объект типа <typeparamref name="TDestination"/>.</returns>
  /// <exception cref="InvalidOperationException">Когда маппинг невозможен.</exception>
  public TDestination Map<TDestination>(object source)
  {
    if (source == null)
    {
      return default;
    }

    var sourceType = source.GetType();

    var method = typeof(Mapper).GetMethod(
        nameof(Map),
        1,
        BindingFlags.Public | BindingFlags.Instance,
        null,
        new[] { sourceType },
        null)?.MakeGenericMethod(sourceType, typeof(TDestination));

    if (method != null)
    {
      var result = method.Invoke(this, new[] { source });
      return result != null ? (TDestination)result : default;
    }

    throw new InvalidOperationException($"Не удалось найти метод маппинга для преобразования {sourceType} в {typeof(TDestination)}");
  }

  /// <summary>
  /// Выполняет маппинг коллекции объектов типа <typeparamref name="TSource"/> в коллекцию объектов типа <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Тип исходных объектов в коллекции.</typeparam>
  /// <typeparam name="TDestination">Тип целевых объектов в коллекции.</typeparam>
  /// <param name="source">Коллекция исходных объектов для преобразования.</param>
  /// <returns>Новая коллекция объектов типа <typeparamref name="TDestination"/>.</returns>
  public List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
  {
    return source?.Select(item => Map<TSource, TDestination>(item)).ToList()
           ?? new List<TDestination>();
  }
}
