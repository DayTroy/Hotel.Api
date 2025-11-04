using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Models;

/// <summary>
/// Модель гостиничного номера в БД.
/// </summary>
public sealed record RoomDbo
{
  /// <summary>
  /// Номер комнаты.
  /// </summary>
  [Key]
  public string roomId { get; set; }

  /// <summary>
  /// Статус гостиничного номера.
  /// </summary>
  public string status { get; set; }

  /// <summary>
  /// Этаж.
  /// </summary>
  public int stage { get; set; }

  /// <summary>
  /// Номер категории гостиничного номера.
  /// </summary>
  public string roomCategoryId { get; set; }
}
