namespace Hotel.Core.Mapping.Interfaces;

/// <summary>
/// Хранилище конфигураций маппинга между типами.
/// </summary>
public interface IMapStore
{
  /// <summary>
  /// Регистрирует функцию маппинга для преобразования типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.
  /// </summary>
  /// <param name="mapFunc">Функция преобразования объекта типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.</param>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  void AddMap<TSource, TDestination>(Func<TSource, TDestination> mapFunc);

  /// <summary>
  /// Возвращает зарегистрированную функцию маппинга для преобразования типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  /// <returns>Функция маппинга, если она была зарегистрирована, иначе null.</returns>
  Func<TSource, TDestination> GetMap<TSource, TDestination>();

  /// <summary>
  /// Проверяет наличие зарегистрированной функции маппинга для преобразования типа <typeparamref name="TSource"/> в <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Исходный тип для маппинга.</typeparam>
  /// <typeparam name="TDestination">Целевой тип для маппинга.</typeparam>
  /// <returns>true, если функция маппинга зарегистрирована, иначе false.</returns>
  bool HasMap<TSource, TDestination>();
}
