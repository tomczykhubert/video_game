using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GenreEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("genre_name", TypeName = "varchar(50)")]
        public string Name { get; set; }
        public ISet<GameEntity> Games { get; set; } = new HashSet<GameEntity>();
    }
}
