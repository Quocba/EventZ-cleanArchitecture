using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Domain.Shares;

namespace Event.Application.DTO
{
    public class GetEventPackageRegistrationDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonConverter(typeof(JsonEnumConverter<EventPackageRegistrationStatusEnum>))]
        public EventPackageRegistrationStatusEnum Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public double Price { get; set; }

        [JsonIgnore( Condition =JsonIgnoreCondition.WhenWritingNull)]
        public string EventPackageName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double EventPackageSellPrice { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double EventPackageSalePrice { get; set; }


    }
}
