using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    [Table("events")]
    public class Events : BaseEntity<Guid>
    {
        [Column("title")]
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [Column("description")]
        [MaxLength(255)]
        [Required] 
        public string Description { get; set; }

        [Column("start_time")]
        [Required]
        public DateTime StartTime { get; set; }

        [Column("end_time")]
        [Required]
        public DateTime EndTime { get; set; }

        [Column("province")]
        [MaxLength(255)]
        [Required]
        public string Province {  get; set; }

        [Column("address")]
        [MaxLength(255)]
        [Required]
        public string Address { get; set; }

        [Column("additional_info")]
        [MaxLength(255)]
        [Required]

        public string AdditionalInfo {  get; set; }

        [Column("number_of_guest")]
        [Required]
        public int NumberOfGuest {  get; set; }
        [Column("price")]
        [Required]
        public double Price { get; set; } = 0;

        [Column("is_active")]
        [Required]
        public bool IsActive { get; set; } = false;


        [Column("is_open_layout")]
        [Required]
        public bool IsOpenLayout { get; set; } = false;

        [Column("status")]
        [Required]
        public EventStatusEnum Status { get; set; }
        [Required]
        [Column("is_free")]
        public bool IsFree { get; set; } = false;

        [Column("user_id")]
        [Required]
        public Guid UserID { get; set; }

        [Column("event_type_id")]
        [Required]
        public Guid EventTypeID {  get; set; }

        [ForeignKey("EventTypeID")]
        public EventType EventType { get; set; }

        public ICollection<EventImages> EventImages { get; set; }
        public ICollection<EventRegistrationLink> EventRegistrationLinks { get; set; }
    }
}
