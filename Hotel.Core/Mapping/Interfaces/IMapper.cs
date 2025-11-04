namespace Hotel.Core.Mapping.Interfaces;

/// <summary>
/// Интерфейс для работы с маппингом объектов между различными типами.
/// Предоставляет методы для преобразования одиночных объектов и коллекций.
/// </summary>
public interface IMapper
{
  /// <summary>
  /// Выполняет маппинг объекта типа <typeparamref name="TSource"/> в объект типа <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
  /// <typeparam name="TDestination">Тип целевого объекта.</typeparam>
  /// <param name="source">Исходный объект для преобразования.</param>
  /// <returns>Новый объект типа <typeparamref name="TDestination"/>.</returns>
  TDestination Map<TSource, TDestination>(TSource source);

  /// <summary>
  /// Выполняет маппинг объекта в указанный тип <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TDestination">Тип целевого объекта.</typeparam>
  /// <param name="source">Исходный объект для преобразования.</param>
  /// <returns>Новый объект типа <typeparamref name="TDestination"/>.</returns>
  TDestination Map<TDestination>(object source);

  /// <summary>
  /// Выполняет маппинг коллекции объектов типа <typeparamref name="TSource"/> в коллекцию объектов типа <typeparamref name="TDestination"/>.
  /// </summary>
  /// <typeparam name="TSource">Тип исходных объектов в коллекции.</typeparam>
  /// <typeparam name="TDestination">Тип целевых объектов в коллекции.</typeparam>
  /// <param name="source">Коллекция исходных объектов для преобразования.</param>
  /// <returns>Новая коллекция объектов типа <typeparamref name="TDestination"/>.</returns>
  List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);
}
