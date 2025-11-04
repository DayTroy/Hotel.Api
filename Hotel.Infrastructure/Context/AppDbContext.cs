using Hotel.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Context;

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
  /// <see cref="Room"/>Gets or sets .
  /// </summary>
  public DbSet<Room> Room { get; set; }
}
