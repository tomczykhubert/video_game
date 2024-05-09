using AutoMapper;
using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class AutoEntityMapper : Profile
    {
        public AutoEntityMapper()
        {
            //from db
            CreateMap<GameEntity, GameDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.GamesPublishers))
                .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.GamesPublishers))
                .ForMember(dest => dest.PublishersPlatformsYears, opt => opt.MapFrom(src => src.GamesPublishers));
            CreateMap<ISet<GamePublisherEntity>, List<PublisherPlatformYearDTO>>().AfterMap(
                (src, dest) =>
                    {
                        foreach (var gp in src)
                        {
                            foreach (var gp2 in gp.GamesPlatforms)
                            {
                                dest.Add(new PublisherPlatformYearDTO()
                                {
                                    Platform = gp2.Platform.Name,
                                    Publisher = gp.Publisher.Name,
                                    ReleaseYear = gp2.ReleaseYear,
                                });
                            }
                        }
                    });
            CreateMap<ISet<GamePublisherEntity>, List<PublisherDTO>>()
                .AfterMap((src, dest) =>
                {
                    foreach (var gp in src)
                    {
                        dest.Add(new PublisherDTO()
                        {
                            Id = gp.Publisher.Id,
                            Name = gp.Publisher.Name
                        });
                    }
                });
            CreateMap<ISet<GamePublisherEntity>, List<PlatformDTO>>()
                .AfterMap((src, dest) =>
                {
                    foreach (var gp in src)
                    {
                        foreach (var gp2 in gp.GamesPlatforms)
                        {
                            dest.Add(new PlatformDTO()
                            {
                                Id = gp2.Platform.Id,
                                Name = gp2.Platform.Name
                            });
                        }
                    }
                });
            //to db
            CreateMap<NewGameDTO, GameEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));
        }
    }
}
