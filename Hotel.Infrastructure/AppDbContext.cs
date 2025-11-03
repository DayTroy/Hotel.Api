using Hotel.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure;

/// <summary>
/// Контекст БД.
/// </summary>
public class AppDbContext : DbContext
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AppDbContext"/> class.
  /// </summary>
  /// <param name="options"><see cref="DbContextOptions<AppDbContext>"></param>.
  public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
  {
  }

  /// <summary>
  /// <see cref="Guest"/>Gets or sets .
  /// </summary>
  public DbSet<Guest> Guests { get; set; }
}
