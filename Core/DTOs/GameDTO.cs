using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<PublisherDTO> Publishers { get; set; } = new List<PublisherDTO>();
        public List<PlatformDTO> Platforms { get; set; } = new List<PlatformDTO>();
        public List<PublisherPlatformYearDTO> PublishersPlatformsYears { get; set; } = new List<PublisherPlatformYearDTO>();
    }
}
