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
    [Table("event_images")]
    public class EventImages : BaseEntity<Guid>
    {
        [Column("image_url")]
        [MaxLength(255)]
        [Required]
        public string ImageUrl {  get; set; }

        [Column("image_type")]
        [MaxLength(255)]
        [Required]
        public ImageTypeEnum ImageType { get; set; }

        [Column("event_id")]
        [Required]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        public Events Events { get; set; }
    }
}
