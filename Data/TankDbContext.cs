using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki.Models;

namespace Tank_Wiki.Data;

public class TankDbContext : IdentityDbContext<AppUser>
{
    public TankDbContext(DbContextOptions<TankDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tank> Tanks => Set<Tank>();
    public DbSet<TankOfficer> TankOfficers => Set<TankOfficer>();
    public DbSet<General> Generals => Set<General>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<TankBattleVideo> TankBattleVideos => Set<TankBattleVideo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tank → Officers relation
        modelBuilder.Entity<Tank>()
            .HasMany(t => t.Officers)
            .WithOne(o => o.Tank)
            .HasForeignKey(o => o.TankId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tank → Generals relation
        modelBuilder.Entity<Tank>()
            .HasMany(t => t.Generals)
            .WithOne(g => g.Tank)
            .HasForeignKey(g => g.TankId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<General>()
       .Property(g => g.Description)
       .HasMaxLength(2000);

        modelBuilder.Entity<General>()
        .HasOne(g => g.Tank)
        .WithMany(t => t.Generals)
        .HasForeignKey(g => g.TankId)
        .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<TankBattleVideo>()
      .HasOne(x => x.Tank1)
      .WithMany()
      .HasForeignKey(x => x.Tank1Id)
      .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TankBattleVideo>()
            .HasOne(x => x.Tank2)
            .WithMany()
            .HasForeignKey(x => x.Tank2Id)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
