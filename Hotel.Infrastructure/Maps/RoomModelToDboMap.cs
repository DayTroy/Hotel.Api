using Hotel.Core.Mapping.Abstract;
using Hotel.Core.Models;
using Hotel.Infrastructure.Models;
using Mapster;

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

  /// <inheritdoc/>
  // public void Register(TypeAdapterConfig config)
  // {
  //   config.NewConfig<RoomDbo, Room>()
  //       .Map(dest => dest.RoomId, src => src.roomId)
  //       .Map(dest => dest.RoomCategoryId, src => src.roomCategoryId)
  //       .Map(dest => dest.Stage, src => src.stage)
  //       .Map(dest => dest.Status, src => src.status);
  // }
}
