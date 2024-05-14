using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Models;
using Infrastructure.ServiceFiles.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceFiles.Services
{
    public class GameService : IGameService
    {
        private readonly VideoGamesDbContext _context;
        private IMapper _mapper;
        public GameService(VideoGamesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GameDTO? FindGameById(int id)
        {
            var entity = _context.game.AsNoTracking()
                .Include(game => game.Genre)
                .Include(game => game.GamesPublishers)
                .ThenInclude(gp => gp.Publisher)
                .Include(game => game.GamesPublishers)
                .ThenInclude(gp => gp.GamesPlatforms)
                .ThenInclude(gp => gp.Platform)
                .FirstOrDefault(e => e.Id == id);
            return entity is null ? null : _mapper.Map<GameDTO>(entity);
        }

        public PagingList<GameDTO>? FindGamesByGenreIdPaged(int page, int size, int genreId)
        {
            var find = _context.game.AsNoTracking()
                .Include(game => game.Genre)
                .Include(game => game.GamesPublishers)
                .ThenInclude(gp => gp.Publisher)
                .Include(game => game.GamesPublishers)
                .ThenInclude(gp => gp.GamesPlatforms)
                .ThenInclude(gp => gp.Platform)
                .Where(x => x.GenreId == genreId);
            return find.Count() == 0 ? null : PagingList<GameDTO>.Create(
                (p, s) => find
                    .Skip((p - 1) * s)
                    .Take(s)
                    .Select(_mapper.Map<GameDTO>)
                    .ToList(), page, size, find.Count());
        }

        public GameEntity AddGame(NewGameDTO gameDTO)
        {
            var entity = _mapper.Map<GameEntity>(gameDTO);
            try
            {
                var saved = _context.game.Add(entity).Entity;
                _context.SaveChanges();
                return saved;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException.Message);
                if (e.InnerException.Message.StartsWith("23503"))
                {
                    throw new GameNotFoundException("Genre not found. Can't save!");
                }
                throw new Exception(e.Message);
            }
        }

        public bool DeleteGameById(int id)
        {
            var find = _context.game.Where(x => x.Id == id).FirstOrDefault();
            if (find is null)
                return false;
            _context.game.Remove(find);
            _context.SaveChanges();
            return true;
        }
    }
}