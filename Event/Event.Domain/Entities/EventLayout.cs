using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    [Table("event_layout")]
    public class EventLayout : BaseEntity<Guid>
    {
        [Column("event_id")]
        public Guid EventID { get; set; }
        [ForeignKey("EventID")]
        public Events Events { get; set; }

        [Column("layout_id")]
        public Guid LayoutID { get; set; }
        public Layout Layout { get; set; }
    }
}
