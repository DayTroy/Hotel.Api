using Hotel.Core.Models;

namespace Hotel.Core.Services.Abstract;

/// <summary>
/// Интерфейс для работы с сервисом гостиничных номеров.
/// </summary>
public interface IRoomService
{
  /// <summary>
  /// Получение всех гостей.
  /// </summary>
  /// <returns>.</returns>
  public Task<List<Room>> GetAllRoomsAsync();
}
