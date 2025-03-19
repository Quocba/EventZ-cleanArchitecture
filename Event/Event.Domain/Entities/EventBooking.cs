using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Common;
using Event.Domain.Shares;

namespace Event.Domain.Entities
{
    [Table("event_booking")]
    public class EventBooking : BaseEntity<Guid>
    {
        [Column("num_seats")]
        [Required]
        public int NumSeat {  get; set; }
 
        [Column("additional_info")]
        
        [Required]
        public string Additional_Info {  get; set; }

        [Column("is_active")]
        [Required]
        public bool IsActive {  get; set; } = false;

        [Column("status")]
        [Required]
        public EventSeatsBookingStatusEnum Status { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }  = DateUtility.GetCurrentDateTime();

        [Column("updated_at")]
        [Required]
        public DateTime UpdatedAt { get; set;} = DateUtility.GetCurrentDateTime();

        [Column("event_seat_id")]
        [Required]
        public Guid EventSeatID {  get; set; }
        [ForeignKey("EventSeatID")]
        public EventSeats EventSeats { get; set; }

        [Column("user_id")]
        [Required]
        public Guid UserID { get; set; }

        [Column("payment_id")]
        [Required]
        public Guid PaymentID { get; set; }
    }
}
