using Core.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class GameApiTest
    {
        [Theory]
        [InlineData(1)]
        public async void GetOneGame(int id)
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetFromJsonAsync<GameDTO>($"/api/game/{id}");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}
