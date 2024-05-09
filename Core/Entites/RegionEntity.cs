using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RegionEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("region_name", TypeName = "varchar(50)")]
        public string Name { get; set; }
        public ISet<RegionSalesEntity> RegionSales { get; set; } = new HashSet<RegionSalesEntity>();
    }
}
