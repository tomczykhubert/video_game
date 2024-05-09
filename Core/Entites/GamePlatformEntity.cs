using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GamePlatformEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("game_publisher_id")]
        public int GamePublisherId { get; set; }
        [Column("platform_id")]
        public int PlatformId { get; set; }
        [Column("release_year")]
        public int ReleaseYear { get; set; }
        public GamePublisherEntity GamePublisher { get; set; }
        public PlatformEntity Platform { get; set; }
        public ISet<RegionSalesEntity> RegionSales { get; set; } = new HashSet<RegionSalesEntity>();
    }
}
