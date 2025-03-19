using Identity.Domain.Shares;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Identity.Application.Features.Users.Commands.UpdateAccount
{
    public class UpdateUserProfileCommand : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [StringLength(50)]
        [Column("first_name")]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        [Column("last_name")]
        public string? LastName { get; set; }

        [MaxLength(255)]
        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("gender")]
        public int? Gender { get; set; }
    }
}
