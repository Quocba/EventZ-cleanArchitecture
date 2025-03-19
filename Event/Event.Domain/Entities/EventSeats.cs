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
    [Table("event_seats")]
    public class EventSeats : BaseEntity<Guid>
    {
        [Column("seat_label")]
        [MaxLength(20)]
        [Required]
        public string SeatLabel {  get; set; }

        [Column("row_number")]
        [Required]
        public int RowNumber {  get; set; }

        [Column("col_number")]
        [Required]
        public int ColNumber {  get; set; }

        [Column("seat_type")]
        [Required]
        public SeatTypeEnum SeatType { get; set; }

        [Column("price")]
        [Required]
        public double Price {  get; set; }

        [Column("total_seats")]
        [Required]
        public int TotalSeats {  get; set; }    


        [Column("available_seats")]
        [Required]
        public int AvailableSeats {  get; set; }

        [Column("status")]
        [Required]
        public string Status { get; set; }

        [Column("is_free")]
        [Required]
        public bool IsFree {  get; set; } = true;

        [Column("need_accept")]
        [Required]
        public bool NeedAccept { get; set; } = true;

        [Column("event_layout_id")]
        [Required]
        public Guid EventLayoutID {  get; set; }

        [ForeignKey("EventLayoutID")]
        public EventLayout EventLayout { get; set; }

    }
}
