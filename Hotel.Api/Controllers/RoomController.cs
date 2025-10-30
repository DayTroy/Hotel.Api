using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        public RoomController()
        {
        }

        [HttpGet("types")]
        public IActionResult GetRoomTypes()
        {
            var roomTypes = new[] { "Single", "Double", "Suite", "Deluxe" };
            return Ok(roomTypes);
        }
    }
}