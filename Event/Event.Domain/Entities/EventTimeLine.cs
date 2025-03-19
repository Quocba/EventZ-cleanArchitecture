using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    [Table("event_timeline")]
    public class EventTimeLine : BaseEntity<Guid>
    {
        [Column("title")]
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [Column("content")]
        [MaxLength(255)]
        [Required]
        public string Content { get; set; }

        [Column("start_date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column("handle_by")]
        [Required]
        public HandleBy HandleBy { get; set; }

        [Column("parent_id")]
        [Required]
        public Guid ParentID {  get; set; }

        [Column("event_id")]
        [Required]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        public Events Events { get; set; }

        [Column("timeline_type")]
        [Required]
        public TimeLineTypeEnum TimeLineType { get; set; }


        [Column("status")]
        [Required]
        public EventTimeLineStatusEnum Status { get; set; }

      
    }   
}
