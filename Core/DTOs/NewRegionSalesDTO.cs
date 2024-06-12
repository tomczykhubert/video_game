using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class NewRegionSalesDTO
    {
        public int GameId { get; set; }
        public int RegionId { get; set; }
        public decimal Sales { get; set; }
        public int PlatformId { get; set; }
    }
}
