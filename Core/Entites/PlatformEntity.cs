using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PlatformEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("platform_name", TypeName = "varchar(50)")]
        public string Name { get; set; }
        public ISet<GamePlatformEntity> GamesPlatforms { get; set; } = new HashSet<GamePlatformEntity>();
    }
}
