namespace Hotel.Core.Models;

/// <summary>
/// Модель гостиничного номера.
/// </summary>
public sealed record Room
{
  /// <summary>
  /// Номер комнаты.
  /// </summary>
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
