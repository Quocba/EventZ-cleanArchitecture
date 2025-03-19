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
    [Table("layout")]
    public class Layout : BaseEntity<Guid>
    {
        [Column("layout_name")]
        [MaxLength(255)]
        [Required]
        public string LayoutName {  get; set; }

        [Column("layout_floor_number")]
        [Required]
        public int LayoutFloorNumber {  get; set; }

        [Column("layout_type")]
        [Required]
        public LayoutTypeEnum LayoutType { get; set; }

        [Column("rows")]
        [Required]
        public int Rows {  get; set; }

        [Column("cols")]
        [Required]
        public int Cols {  get; set; }
        public bool IsDeleted { get; set; } = false;

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateUtility.GetCurrentDateTime();

        [Column("created_by")]
        [Required]
        [MaxLength(255)]
        public string CreatedBy {  get; set; }
    }
}
