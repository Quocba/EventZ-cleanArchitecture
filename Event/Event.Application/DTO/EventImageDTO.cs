using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Shares;
using Newtonsoft.Json;

namespace Event.Application.DTO
{
    public class EventImageDTO
    {
        public string ImageUrl { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<ImageTypeEnum>))]
        public ImageTypeEnum ImageType { get; set; }

    }

}
