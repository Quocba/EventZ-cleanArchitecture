using Payment.Domain.Common;
using Payment.Domain.Enums;
using Payment.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Entities
{
    [Table("payment_event")]
    public class PaymentEvent : BaseEntity<Guid>
    {
        [Column("user_id")]
        [Required]
        public Guid UserId { get; set; }
        [Column("event_id")]
        [Required]
        public Guid EventId { get; set; }
        [Column("content")]
        [Required]
        [StringLength(255)]
        public string Content { get; set; }
        [Column("amount")]
        [Required]
        public decimal Amount { get; set; }
        [Column("status")]
        public PaymentStatus Status { get; set; } = PaymentStatus.PENDING;
        [Column("type")]
        public PaymentType Type { get; set; } = PaymentType.BANK_TRANSFER;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateUtility.GetCurrentDateTime();
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateUtility.GetCurrentDateTime();
        [Column("expire_at")]
        public DateTime ExpireAt { get; set; } = DateUtility.GetCurrentDateTime().AddMinutes(15);
    }
}
