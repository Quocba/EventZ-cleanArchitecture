using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.ChangeDeleteUser
{
    public class ChangeDeletedUserCommand : IRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
