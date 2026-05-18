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
    public DbSet<EditRequest> EditRequests => Set<EditRequest>();

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
       .HasMaxLength(1999);

        modelBuilder.Entity<General>()
        .HasOne(g => g.Tank)
        .WithMany(t => t.Generals)
        .HasForeignKey(g => g.TankId)
        .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<TankBattleVideo>()
     .HasOne(x => x.Tank1)
     .WithMany()
     .HasForeignKey(x => x.Tank1Id)
     .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TankBattleVideo>()
            .HasOne(x => x.Tank2)
            .WithMany()
            .HasForeignKey(x => x.Tank2Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EditRequest>()
    .Property(r => r.EntityType)
    .HasConversion<string>();

        modelBuilder.Entity<EditRequest>()
            .Property(r => r.OldDescription)
            .HasMaxLength(3000);

        modelBuilder.Entity<EditRequest>()
            .Property(r => r.NewDescription)
            .HasMaxLength(3000);

        modelBuilder.Entity<EditRequest>()
            .Property(r => r.Status)
            .HasDefaultValue("Pending");

        modelBuilder.Entity<EditRequest>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
