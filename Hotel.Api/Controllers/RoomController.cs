using Hotel.Core.Models;
using Hotel.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

/// <summary>
/// Initializes a new instance of the <see cref="RoomController"/> class.
/// </summary>
[ApiController]
[Route("api")]
public class RoomController : ControllerBase
{
  private readonly IRoomService _roomService;

  /// <summary>
  /// Initializes a new instance of the <see cref="RoomController"/> class.
  /// </summary>
  /// <param name="roomService"><see cref="IRoomService"/>.</param>
  public RoomController(IRoomService roomService)
  {
    _roomService = roomService;
  }

  /// <summary>
  /// Получение гостиничных номеров.
  /// </summary>
  /// <returns>IActionResult.</returns>
  [HttpGet("rooms")]
  public async Task<List<Room>> GetRooms()
  {
    var rooms = await _roomService.GetAllRoomsAsync();
    return rooms;
  }
}
