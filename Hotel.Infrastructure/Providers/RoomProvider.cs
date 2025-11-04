using Hotel.Core.Interfaces;
using Hotel.Core.Mapping.Abstract;
using Hotel.Core.Models;
using Hotel.Infrastructure.Context;
using Hotel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Providers;

/// <summary>
/// <see cref="IRoomProvider"/>.
/// </summary>
public class RoomProvider : IRoomProvider
{
  private readonly AppDbContext _pgDb;
  private readonly IMapper _mapper;

  /// <summary>
  /// Initializes a new instance of the <see cref="RoomProvider"/> class.
  /// </summary>
  /// <param name="context"><see cref="AppDbContext"/>.</param>
  /// <param name="mapper"><see cref="IMapperService"/>.</param>
  public RoomProvider(AppDbContext context, IMapper mapper)
  {
    _pgDb = context ?? throw new ArgumentNullException(nameof(context));
    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
  }

  /// <summary>
  /// <see cref="IRoomProvider"/>.
  /// </summary>
  /// <returns>.</returns>
  public async Task<List<Room>> GetAllRoomsAsync()
  {
    var roomsDbo = await _pgDb.Room.ToListAsync();
    var rooms = _mapper.MapList<RoomDbo, Room>(roomsDbo);
    return rooms;
  }
}
