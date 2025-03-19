using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    [Table("user_roles")]
    public class UserRole
    {
        [Column("role_id")]
        public Guid RoleID {  get; set; }
        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; }
        [Column("user_id")]
        public Guid UserID {  get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
    }
}
