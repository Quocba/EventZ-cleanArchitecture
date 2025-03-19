using Identity.Domain.Shares;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string? Avatar { get; set; }

        [MaxLength(10)]
        [Phone]
        public string Phone { get; set; }

        public int Gender { get; set; }
    }
}
