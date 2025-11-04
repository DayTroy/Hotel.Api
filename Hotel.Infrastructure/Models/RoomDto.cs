namespace Hotel.Infrastructure.Models;

/// <summary>
/// Модель гостиничного номера.
/// </summary>
public sealed record RoomDto
{
  /// <summary>
  /// Номер комнаты.
  /// </summary>
  public string Id;

  /// <summary>
  /// Этаж.
  /// </summary>
  public int Stage;
}
