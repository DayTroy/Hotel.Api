namespace Hotel.Core.Abstract;

/// <summary>
/// Интерфейс для работы с сервисом гостей.
/// </summary>
public interface IGuestService
{
  /// <summary>
  /// Получение всех гостей.
  /// </summary>
  public void GetAllGuestsAsync();
}
