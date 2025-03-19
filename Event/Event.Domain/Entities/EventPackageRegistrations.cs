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
    [Table("event_package_registrations")]
    public class EventPackageRegistrations : BaseEntity<Guid>
    {
        [Column("name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column("email")]
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }

        [Column("phone")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Column("status")]
        [Required]
        public EventPackageRegistrationStatusEnum Status { get; set; }

        [Column("create_at")]
        [Required]
        public DateTime CreateAt { get; set; } = DateUtility.GetCurrentDateTime();

        [Column("update_at")]
        [Required]
        public DateTime UpdateAt { get; set; } = DateUtility.GetCurrentDateTime();

        [Column("price")]
        [Required]
        public double Price { get; set; }

        [Column("event_package_id")]
        public Guid EventPackageID { get; set; }

        [ForeignKey("EventPackageID")]
        public EventPackages EventPackage { get; set; }
    }
}