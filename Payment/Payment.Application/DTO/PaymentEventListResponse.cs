using Payment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Payment.Application.DTO
{
    public class PaymentEventListResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Content { get; set; }
        public decimal Amount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
