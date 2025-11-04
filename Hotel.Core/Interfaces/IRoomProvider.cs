using Hotel.Core.Models;

namespace Hotel.Core.Interfaces;

/// <summary>
/// Провайдер для работы с сервисом гостиничных номеров.
/// </summary>
public interface IRoomProvider
{
  /// <summary>
  /// Получение всех гостей.
  /// </summary>
  /// <returns>.</returns>
  public Task<List<Room>> GetAllRoomsAsync();
}
