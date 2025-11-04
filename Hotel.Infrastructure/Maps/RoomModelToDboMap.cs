using Hotel.Core.Mapping.Interfaces;
using Hotel.Core.Models;
using Hotel.Infrastructure.Models;

namespace Hotel.Infrastructure.Maps;

/// <summary>
/// Маппинг между BL и DBO.
/// </summary>
public class RoomModelToDboMap : IMapperConfig
{
  /// <inheritdoc/>
  public void AddMaps(IMapStore store, IMapper mapper)
  {
    store.AddMap<Room, RoomDbo>(origin => new RoomDbo
    {
      roomId = origin.RoomId,
      roomCategoryId = origin.RoomCategoryId,
      stage = origin.Stage,
      status = origin.Status,
    });

    store.AddMap<RoomDbo, Room>(origin => new Room
    {
      RoomId = origin.roomId,
      RoomCategoryId = origin.roomCategoryId,
      Stage = origin.stage,
      Status = origin.status,
    });
  }
}
