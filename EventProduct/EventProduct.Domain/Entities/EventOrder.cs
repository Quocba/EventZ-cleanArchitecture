using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Domain.Common;
using EventProduct.Domain.Shares;

namespace EventProduct.Domain.Entities
{
    [Table("event_order")]
    public class EventOrder : BaseEntity<Guid>
    {
        [Column("total_price")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Total Price can be not less than 1")]
        public double TotalPrice { get; set; } = 0;

        [Column("created_at")]
        [Required]
        public DateTime CreateAt { get; set; } = DateUtility.GetCurrentDateTime();


        [Column("update_at")]
        [Required]
        public DateTime UpdateAt { get; set; } = DateUtility.GetCurrentDateTime();

        [Column("event_id")]
        [Required]  
        public Guid EventID { get; set; }

        [Column("user_id")]
        [Required]
        public Guid UserID { get; set; }
        [Column("fullname")]
        [Required]
        [MaxLength(100)]
        public string FullName {  get; set; }

        [Column("phone")]
        [Required]
        public string Phone {  get; set; }

        [Column("email")]
        [Required]
        public string Email { get; set; }

        [Column("address")]
        [Required]
        public string Address { get; set; }
        [Column("payment_history_id")]
        [Required]
        public Guid PaymentHistoryID {  get; set; }
    }
}
