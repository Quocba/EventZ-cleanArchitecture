using Identity.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    [Table("roles")]
    public class Role : BaseEntity<Guid>
    {
        [Required]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        [MaxLength(255)]
        public string? Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
