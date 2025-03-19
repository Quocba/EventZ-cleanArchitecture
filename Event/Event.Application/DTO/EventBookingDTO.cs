using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Shares;

namespace Event.Application.DTO
{
    public class EventBookingDTO
    {
        public Guid EventBookingID { get; set; }
        public int NumSeat { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsActive { get; set; }
        [JsonConverter(typeof(JsonEnumConverter<EventSeatsBookingStatusEnum>))]
        public EventSeatsBookingStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid EventSeatID { get; set; }
        public Guid ID { get; set; }
        public Guid PaymentID { get; set; }
    }
}
