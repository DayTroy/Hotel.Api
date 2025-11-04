namespace Hotel.Core.Models;

/// <summary>
/// Модель гостя.
/// </summary>
public sealed record Guest
{
  /// <summary>
  /// Имя.
  /// </summary>
  public string FirstName;

  /// <summary>
  /// Фамилия.
  /// </summary>
  public string LastName;

  /// <summary>
  /// Отчество.
  /// </summary>
  public string? MiddleName;

  /// <summary>
  /// Контактный телефон.
  /// </summary>
  public string PhoneNumber;

  /// <summary>
  /// Дата рождения.
  /// </summary>
  public DateTime BirthDate;
}
