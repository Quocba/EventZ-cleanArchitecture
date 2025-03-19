using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Json;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    [Table("event_packages")]
    public class EventPackages : BaseEntity<Guid>
    {
        [Column("name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("sell_price")]
        [Required]
        public double SellPrice { get; set; }

        [Column("sale_price")]
        [Required]
        public double SalePrice { get; set; }

        [Column("benifit")]
        [Required]
        public Benefit Benefit { get; set; }
    }
}
