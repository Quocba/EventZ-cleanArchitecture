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
    [Table("refesh_tokens")]
    public class RefreshToken : BaseEntity<Guid>
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [StringLength(250)]
        [Column("token")]
        public string Token { get; set; }
        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }
    }

}
