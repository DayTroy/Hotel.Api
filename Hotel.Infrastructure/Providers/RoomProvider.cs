using Hotel.Core.Interfaces;
using Hotel.Core.Models;
using Hotel.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Providers;

/// <summary>
/// <see cref="IRoomProvider"/>.
/// </summary>
public class RoomProvider : IRoomProvider
{
  private readonly AppDbContext _pgDb;

  /// <summary>
  /// Initializes a new instance of the <see cref="RoomProvider"/> class.
  /// </summary>
  /// <param name="context"><see cref="AppDbContext"/>.</param>
  public RoomProvider(AppDbContext context)
  {
    _pgDb = context ?? throw new ArgumentNullException(nameof(context));
  }

  /// <summary>
  /// <see cref="IRoomProvider"/>.
  /// </summary>
  /// <returns>.</returns>
  public async Task<List<Room>> GetAllRoomsAsync()
  {
    var rooms = await _pgDb.Room.ToListAsync();
    return rooms;
  }
}
