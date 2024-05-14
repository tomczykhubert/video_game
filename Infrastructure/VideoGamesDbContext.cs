using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class VideoGamesDbContext : DbContext
    {
        public DbSet<GameEntity> game { get; set; }
        public DbSet<GenreEntity> genre { get; set; }
        public DbSet<PlatformEntity> platform { get; set; }
        public DbSet<PublisherEntity> publisher { get; set; }
        public DbSet<RegionEntity> region { get; set; }
        public DbSet<RegionSalesEntity> region_sales { get; set; }
        public DbSet<GamePlatformEntity> game_platform { get; set; }
        public DbSet<GamePublisherEntity> game_publisher { get; set; }

        public VideoGamesDbContext(DbContextOptions<VideoGamesDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=test;Username=postgres;Password=1234;Include Error Detail=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("video_games");
            modelBuilder.Entity<GameEntity>().HasOne<GenreEntity>(game => game.Genre).WithMany(genre => genre.Games).HasForeignKey(game => game.GenreId);
            modelBuilder.Entity<GamePublisherEntity>().HasOne<GameEntity>(gamePublisher => gamePublisher.Game).WithMany(game => game.GamesPublishers).HasForeignKey(gamePublisher => gamePublisher.GameId);
            modelBuilder.Entity<GamePublisherEntity>().HasOne<PublisherEntity>(gamePublisher => gamePublisher.Publisher).WithMany(publisher => publisher.GamesPublishers).HasForeignKey(gamePublisher => gamePublisher.PublisherId);
            modelBuilder.Entity<GamePlatformEntity>().HasOne<GamePublisherEntity>(gamePlatform => gamePlatform.GamePublisher).WithMany(gamePublisher => gamePublisher.GamesPlatforms).HasForeignKey(gamePlatform => gamePlatform.GamePublisherId);
            modelBuilder.Entity<GamePlatformEntity>().HasOne<PlatformEntity>(gamePlatform => gamePlatform.Platform).WithMany(platform => platform.GamesPlatforms).HasForeignKey(gamePlatform => gamePlatform.PlatformId);
            modelBuilder.Entity<RegionSalesEntity>().HasOne<RegionEntity>(regionSales => regionSales.Region).WithMany(region => region.RegionSales).HasForeignKey(regionSales => regionSales.RegionId);
            modelBuilder.Entity<RegionSalesEntity>().HasOne<GamePlatformEntity>(regionSales => regionSales.GamePlatform).WithMany(gamePlatform => gamePlatform.RegionSales).HasForeignKey(regionSales => regionSales.GamePlatformId);
        }
    }
}
