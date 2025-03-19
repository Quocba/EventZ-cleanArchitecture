using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Command.EditEventDocument
{
    public class EditEventDocumentsCommand : IRequest
    {
        public Guid DocumentID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string LinkDocument { get; set; }
        public EventDocumentsTypeEnum DocumentType { get; set; }
    }
}
