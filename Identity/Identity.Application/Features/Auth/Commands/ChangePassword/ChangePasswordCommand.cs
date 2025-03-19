using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [Required]
        [MinLength(6)]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
