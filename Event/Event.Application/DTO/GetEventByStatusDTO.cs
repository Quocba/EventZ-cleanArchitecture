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
    public class GetEventByStatusDTO
    {
        [JsonConverter(typeof(JsonEnumConverter<EventStatusEnum>))]
        public EventStatusEnum Status {  get; set; }
        public int Quantity { get; set; }
    }
}
