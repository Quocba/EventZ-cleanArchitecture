using Identity.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Common
{
    public abstract class AuditableBaseEntity<T>
    {
        [Key]
        [Column("id")]
        public virtual T Id { get; set; }
        [Column("create_by")]
        public Guid CreatedBy { get; set; }
        [Column("create_at")]
        public DateTime CreatedAt { get; set; } = DateUtility.GetCurrentDateTime();
        [Column("last_modified_by")]
        public Guid LastModifiedBy { get; set; }
        [Column("last_modified_at")]
        public DateTime? LastModifiedAt { get; set; } = DateUtility.GetCurrentDateTime();
    }
}
