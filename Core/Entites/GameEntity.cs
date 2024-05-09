using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GameEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("genre_id")]
        public int GenreId { get; set; }
        [Column("game_name", TypeName = "varchar(200)")]
        public string Name { get; set; }
        public GenreEntity Genre { get; set; }
        public ISet<GamePublisherEntity> GamesPublishers { get; set; } = new HashSet<GamePublisherEntity>();
    }
}
