using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    [Table("event_type")]
    public class EventType : BaseEntity<Guid>
    {
        [Column("name")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
