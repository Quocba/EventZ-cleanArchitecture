using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Domain.Common;

namespace EventProduct.Domain.Entities
{
    [Table("event_order_product")]
    public class EventOrderProduct : BaseEntity<Guid>
    {
        [Column("price")]
        [Required]
        public double Price {  get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("update_at")]
        [Required]
        public DateTime UpdatdAt { get; set; }

        [Column("quantity")]
        [Required]
        public int Quantity {  get; set; }
        
        [Column("product_id")]
        [Required]
        public Guid ProductID { get; set; }

        [ForeignKey("ProductID")]
        public EventProduct Product { get; set; }

        [Column("event_order_id")]
        [Required]
        public Guid EventOrderID {  get; set; }

        [ForeignKey("EventOrderID")]
        public EventOrder EventOrder { get; set; }



    }
}
