using Identity.Domain.Common;
using Identity.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    [Table("users")]
    public class User : BaseEntity<Guid>
    {

        [StringLength(50)]
        [Column("first_name")]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        [Column("last_name")]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }
        [Column("is_email_confirmed")]
        public bool IsEmailConfirmed { get; set; } = false;

        [MaxLength(255)]
        [Column("avatar")]
        public string? Avatar {  get; set; }

        [MaxLength(10)]
        [Column("phone")]
        public string Phone {  get; set; }

        [Column("is_phone_confirmed")]
        public bool IsPhoneConfirmed { get; set; } = false;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("create_at")]
        [Required]
        public DateTime CreateAt { get; set; } = DateUtility.GetCurrentDateTime();

        [Column("gender")]
        public int Gender {  get; set; }
        [Column("create_by")]
        public Guid? CreateById { get; set; }
        [ForeignKey(nameof(CreateById))]
        public User? CreateBy { get; set; }

        [Required]
        [Column("last_modified_at")]
        public DateTime? LastModifiedAt { get; set; } = DateUtility.GetCurrentDateTime();

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        
    }

}
