using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
