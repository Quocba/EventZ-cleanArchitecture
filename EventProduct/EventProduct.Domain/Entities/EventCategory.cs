using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Domain.Common;

namespace EventProduct.Domain.Entities
{
    [Table("event_category")]
    public class EventCategory : BaseEntity<Guid>
    {
        [Column("name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        [Column("event_id")]
        public Guid EventID {  get; set; }

     
    }
}
