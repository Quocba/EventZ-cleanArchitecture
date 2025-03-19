using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Common;

namespace Identity.Domain.Entities
{
    [Table("event_roles")]
    public class EventRole : BaseEntity<Guid>
    {
        [Column("name")]
        [MaxLength(255)]
        public string Name {  get; set; }

        [Column("description")]
        [MaxLength(255)]
        public string Description { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
