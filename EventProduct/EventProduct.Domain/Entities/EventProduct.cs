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
    [Table("event_product")]
    public class EventProduct : BaseEntity<Guid>
    {
        [Column("name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }
        
        [Column("sell_price")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = ("Sell Price can not be less than 1"))]
        public double SellPrice { get; set; }

        [Column("sale_price")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = ("Sale Price can not be less than 1"))]
        public double SalePrice { get; set; }

        [Column("stock")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage =("Stock can be not less than 1"))]
        public int Stock {  get; set; }

        [Column("thumbnai_url")]
        [Required]
        public string ThumbnaiURL {  get; set; }

        [Column("image_url")]
        [Required]
        public string ImageURL {  get; set; }

        [Column("category_id")]
        [Required]
        public Guid CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public EventCategory Category { get; set; }



    }
}
