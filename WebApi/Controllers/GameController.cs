using Core.DTOs;
using Core.Models;
using Infrastructure;
using Infrastructure.ServiceFiles.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        public IGameService _gameService { get; set; }
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<GameDTO> FindGameById(int id)
        {
            var gameDTO = _gameService.FindGameById(id);
            return gameDTO == null ? NotFound() : Ok(gameDTO);
        }
        [HttpGet]
        [Route("/genre/{genreId}/paged/{size}/{page}")]
        public ActionResult<PagingList<GameDTO>> GetGameByGenreIdPaged(int page, int size, int genreId)
        {
            if (size > 50 || size < 1)
                return BadRequest("Zły rozmiar strony!");
            var gameDTO = _gameService.FindGamesByGenreIdPaged(page, size, genreId);
            return gameDTO == null ? NotFound() : Ok(gameDTO);
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddGame(LinkGenerator link, NewGameDTO gameDTO)
        {
            var  game = _gameService.AddGame(gameDTO);
            return Created(link.GetUriByAction(HttpContext, nameof(FindGameById), null, new { id = game.Id}),
            game);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteGameById(int id)
        {
            var deleted = _gameService.DeleteGameById(id);
            return deleted == false ? NotFound() : Ok($"Game with id {id} deleted succesfully!");
        }
        [HttpUpdate]
    }
}
