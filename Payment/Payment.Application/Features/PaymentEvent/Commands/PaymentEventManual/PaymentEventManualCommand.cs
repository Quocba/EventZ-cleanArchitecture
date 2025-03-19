using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Commands.PaymentManual
{
    public class PaymentEventManualCommand : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [Required]
        public Guid EventId { get; set; }
    }
}
