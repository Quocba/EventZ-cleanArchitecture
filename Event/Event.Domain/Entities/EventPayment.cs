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
    [Table("event_payment")]
    public class EventPayment : BaseEntity<Guid>
    {
        [Column("event_id")]
        [Required]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        public Events Events { get; set; }

        [Column("payment_history_id")]
        public Guid PaymentHistoryID { get; set; }
    }

}
