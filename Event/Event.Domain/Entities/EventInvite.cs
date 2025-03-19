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
    [Table("event_invite")]
    public class EventInvite : BaseEntity<Guid>
    {
        [Column("email")]
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }

        [Column("phone")]
        [MaxLength(11)]
        [Required]
        public string Phone {  get; set; }

        [Column("is_checked")]
        [Required]
        public bool IsChecked {  get; set; }

        [Column("title")]
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("content")]
        [Required]
        [MaxLength]
        public string Content { get; set; }

        [Column("is_confirm")]
        [Required]
        public bool IsConfirm { get; set; }

        [Column("create_at")]
        [Required]
        public DateTime CreateAt { get; set; }

        [Column("update_at")]
        [Required]
        public DateTime UpdateAt { get; set; }

        [Column("event_id")]
        [Required]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        public Events Events { get; set; }
    }
}
