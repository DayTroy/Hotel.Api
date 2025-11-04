using Hotel.Core.Mapping.Abstract;

namespace Hotel.Core.Mapping;

/// <summary>
/// Реализация хранилища конфигураций маппинга между типами.
/// Использует словарь для хранения зарегистрированных функций маппинга.
/// </summary>
public class MapStore : IMapStore
{
  private readonly Dictionary<string, Delegate> _maps = new Dictionary<string, Delegate>();

  /// <summary>
  /// Регистрирует функцию маппинга для преобразования типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.
  /// </summary>
  /// <param name="mapFunc">Функция преобразования объекта типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.</param>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  public void AddMap<TSource, TDestination>(Func<TSource, TDestination> mapFunc)
  {
    var key = GetMapKey<TSource, TDestination>();
    _maps[key] = mapFunc;
  }

  /// <summary>
  /// Возвращает зарегистрированную функцию маппинга для преобразования типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  /// <returns>Функция маппинга, если она была зарегистрирована, иначе null.</returns>
  public Func<TSource, TDestination> GetMap<TSource, TDestination>()
  {
    var key = GetMapKey<TSource, TDestination>();
    return _maps.TryGetValue(key, out var mapFunc)
        ? (Func<TSource, TDestination>)mapFunc
        : null;
  }

  /// <summary>
  /// Проверяет наличие зарегистрированной функции маппинга для преобразования типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  /// <returns>true, если функция маппинга зарегистрирована, иначе false.</returns>
  public bool HasMap<TSource, TDestination>()
  {
    var key = GetMapKey<TSource, TDestination>();
    return _maps.ContainsKey(key);
  }

  /// <summary>
  /// Генерирует уникальный ключ для хранения функции маппинга в словаре.
  /// Формат ключа: "ПолноеИмяИсходногоТипа->ПолноеИмяЦелевогоТипа".
  /// </summary>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  /// <returns>Уникальный строковый ключ для идентификации маппинга.</returns>
  private static string GetMapKey<TSource, TDestination>()
  {
    return $"{typeof(TSource).FullName}->{typeof(TDestination).FullName}";
  }
}
