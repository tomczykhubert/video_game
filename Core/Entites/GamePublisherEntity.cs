using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GamePublisherEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("game_id")]
        public int GameId {  get; set; }
        [Column("publisher_id")]
        public int PublisherId { get; set; }
        public GameEntity Game { get; set; }
        public PublisherEntity Publisher { get; set; }
        public ISet<GamePlatformEntity> GamesPlatforms { get; set; } = new HashSet<GamePlatformEntity>();
    }
}
