using EventManagementExercise.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementExercise.Data
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Allergy> Allergies { get; set; }
    public DbSet<Event> Events { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Guest
      modelBuilder.Entity<Guest>().Property(x => x.NormalizedName).IsRequired();
      modelBuilder.Entity<Guest>().HasIndex(x => x.NormalizedEmail).IsUnique();

      // Allergy
      modelBuilder.Entity<Allergy>().HasIndex(x => x.NormalizedName).IsUnique();
    }
  }
}
