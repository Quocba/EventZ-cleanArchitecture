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
    [Table("event_registration_link")]
    public class EventRegistrationLink : BaseEntity<Guid>
    {
        [Column("code")]
        [MaxLength(255)]
        [Required]
        public string Code { get; set; }

        [Column("start_date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column("event_id")]
        [Required]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        public Events Events { get; set; }

    }
}
