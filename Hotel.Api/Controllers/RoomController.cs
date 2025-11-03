using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

/// <summary>
/// Initializes a new instance of the <see cref="RoomController"/> class.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
  /// <summary>
  /// Initializes a new instance of the <see cref="RoomController"/> class.
  /// </summary>
  public RoomController()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="RoomController"/> class.
  /// </summary>
  /// <returns>IActionResult.</returns>
  [HttpGet("types")]
  public IActionResult GetRoomTypes()
  {
    var roomTypes = new[] { "Single", "Double", "Suite", "Deluxe" };
    return this.Ok(roomTypes);
  }
}
