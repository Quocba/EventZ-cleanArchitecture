using Identity.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    [Table("user_event")]
    public class UserEvent
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Column("event_id")]
        public Guid EventId { get; set; }
        [Column("event_role_id")]
        public Guid EventRoleId { get; set; }
        public EventRole EventRole { get; set; }
    }
}
