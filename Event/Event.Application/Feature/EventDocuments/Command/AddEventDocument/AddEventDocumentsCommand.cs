using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Command.AddEventDocument
{
    public class AddEventDocumentsCommand : IRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string LinkDocument { get; set; }
        public EventDocumentsTypeEnum DocumentType { get; set; }
        public Guid EventID { get; set; }
    }
}
