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
    public class GetDocumentByEventDTO
    {
       public Guid DocumentID {  get; set; }
       public string Title {  get; set; }
       public string Content {  get; set; }
       public string LinkDocument {  get; set; }
        [JsonConverter(typeof(JsonEnumConverter<EventDocumentsTypeEnum>))]
       public EventDocumentsTypeEnum DocumentType {  get; set; }

    }
}
