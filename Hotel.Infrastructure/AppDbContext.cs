using Hotel.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Guest> Products { get; set; }
}
