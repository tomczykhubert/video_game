using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Core.DTOs;
using Core.Entities;

namespace IntegrationTest
{
    public class IntegrationGameApiTest : IClassFixture<AppTestFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppTestFactory<Program> _app;
        private readonly VideoGamesDbContext _context;

        public IntegrationGameApiTest(AppTestFactory<Program> app)
        {
            _app = app;
            _client = _app.CreateClient();
            using (var scope = app.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetService<VideoGamesDbContext>();
                var genre = new GenreEntity()
                {
                    Id = 1,
                    Name = "Test Genre",
                };

                var publisher = new PublisherEntity()
                {
                    Id = 1,
                    Name = "Test Publisher"
                };

                var game = new GameEntity()
                {
                    Id = 1,
                    Name = "Test Game",
                    GenreId = 1,
                };

                var platform = new PlatformEntity()
                {
                    Id = 1,
                    Name = "Test Platform"
                };

                var region = new RegionEntity()
                {
                    Id = 1,
                    Name = "Test Region"
                };

                var gamePublisher = new GamePublisherEntity()
                {
                    Id = 1,
                    GameId = 1,
                    PublisherId = 1
                };

                var gamePlatform = new GamePlatformEntity()
                {
                    Id = 1,
                    GamePublisherId = 1,
                    PlatformId = 1,
                    ReleaseYear = 2024
                };

                var regionSales = new RegionSalesEntity()
                {
                    GamePlatformId = 1,
                    RegionId = 1,
                    NumberOfSales = 10
                };

                if (!_context.genre.Any())
                    _context.genre.Add(genre);

                if (!_context.publisher.Any())
                    _context.publisher.Add(publisher);

                if (!_context.platform.Any())
                    _context.platform.Add(platform);

                if (!_context.game.Any())
                    _context.game.Add(game);

                if (!_context.region.Any())
                    _context.region.Add(region);

                if (!_context.game_publisher.Any())
                    _context.game_publisher.Add(gamePublisher);

                if (!_context.game_platform.Any())
                    _context.game_platform.Add(gamePlatform);

                if (!_context.region_sales.Any())
                    _context.region_sales.Add(regionSales);

                _context.SaveChanges();
            }
        }

        [Fact]
        public async void GetShouldReturnOneQuiz()
        {
            var result = await _client.GetFromJsonAsync<GameDTO>("/api/game/1");
            var publisher = "Test Publisher";
            var platform = "Test Platform";
            Assert.NotNull(result);
            Assert.IsType<GameDTO>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Game", result.Name);
            Assert.Equal("Test Genre", result.Genre);
            Assert.Single(result.Platforms);
            Assert.Equal(platform, result.Platforms.First().Name);
            Assert.Single(result.Publishers);
            Assert.Equal(publisher, result.Publishers.First().Name);
            Assert.Single(result.PublishersPlatformsYears);
            Assert.Equal(platform, result.PublishersPlatformsYears.First().Platform);
            Assert.Equal(publisher, result.PublishersPlatformsYears.First().Publisher);
            Assert.Equal(2024, result.PublishersPlatformsYears.First().ReleaseYear);
        }

        [Fact]
        public async void GetShouldReturnNotFound()
        {
            var result = await _client.GetAsync("/api/game/100");
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
