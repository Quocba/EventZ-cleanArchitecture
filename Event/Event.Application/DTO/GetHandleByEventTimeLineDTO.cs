using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using Event.Domain.Shares;

namespace Event.Application.DTO
{
    public class GetHandleByEventTimeLineDTO
    {
        
        public HandleBy HandleBy { get; set; }
        public Guid TimeLineID { get; set; }
        public string TimeLineTitle { get; set; }
        public string Content { get; set; }
        public DateTime TimeLineStartDate { get; set; }
        public DateTime TimeLineEndDate { get; set; }
        [JsonConverter(typeof(JsonEnumConverter<TimeLineTypeEnum>))]
        public TimeLineTypeEnum TimeLineType { get; set; }

    }
}
