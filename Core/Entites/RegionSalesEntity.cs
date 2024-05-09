using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities
{
    [PrimaryKey(nameof(RegionId), nameof(GamePlatformId))]
    public class RegionSalesEntity
    {
        [Column("region_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionId { get; set; }
        [Column("game_platform_id")]
        public int GamePlatformId { get; set; }
        [Column("num_sales", TypeName = "numeric(5,2)")]
        public decimal NumberOfSales { get; set; }
        public RegionEntity Region { get; set; }
        public GamePlatformEntity GamePlatform { get; set; }
    }
}
