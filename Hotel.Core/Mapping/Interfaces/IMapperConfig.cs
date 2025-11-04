namespace Hotel.Core.Mapping.Interfaces;

/// <summary>
/// Интерфейс для создания мапперов.
/// </summary>
public interface IMapperConfig
{
  /// <summary>
  /// Добавление всех мапперов в хранилище.
  /// </summary>
  /// <param name="store"><see cref="IMapStore"/>.</param>
  /// <param name="mapper"><see cref="IMapper"/>.</param>
  void AddMaps(IMapStore store, IMapper mapper);
}
