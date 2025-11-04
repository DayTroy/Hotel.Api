using Hotel.Core.Interfaces;
using Hotel.Core.Models;
using Hotel.Core.Services.Abstract;

namespace Hotel.Core.Services;

/// <summary>
/// <see cref="IRoomService"/>.
/// </summary>
public class RoomService : IRoomService
{
  private readonly IRoomProvider _roomProvider;

  /// <summary>
  /// Initializes a new instance of the <see cref="RoomService"/> class.
  /// </summary>
  /// <param name="roomProvider"><see cref="IRoomProvider"/>.</param>
  public RoomService(IRoomProvider roomProvider)
  {
    _roomProvider = roomProvider ?? throw new ArgumentNullException(nameof(roomProvider));
  }

  /// <summary>
  /// <see cref="IRoomService"/>.
  /// </summary>
  /// <returns>.</returns>
  public async Task<List<Room>> GetAllRoomsAsync()
  {
    // TODO: Добавить мапперы.
    var rooms = await _roomProvider.GetAllRoomsAsync();
    return rooms;
  }
}
