using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace IntegrationTest
{
    public class AppTestFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<VideoGamesDbContext>));
                services.Remove(dbContextDescriptor);
                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<VideoGamesDbContext>((provider, options) =>
                    {
                        options.UseInMemoryDatabase("QuizTest").UseInternalServiceProvider(provider);
                    });
            });
            builder.UseEnvironment("Development");
        }
    }
}
