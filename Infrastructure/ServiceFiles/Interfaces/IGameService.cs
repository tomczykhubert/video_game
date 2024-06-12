using Core.DTOs;
using Core.Entities;
using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceFiles.Interfaces
{
    public interface IGameService
    {
        GameDTO? FindGameById(int id);
        PagingList<GameDTO>? FindGamesByGenreIdPaged(int page, int size, int genreId);
        GameEntity AddGame(NewGameDTO gameDTO);
        bool DeleteGameById(int id);
        RegionSalesEntity AddRegionSales(NewRegionSalesDTO regionSales);
        decimal GetRegionSales(int id, int platformId, int regionId);
    }
}
