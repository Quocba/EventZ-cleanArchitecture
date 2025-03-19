using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Command.DeleteEventDocument
{
    public class DeleteDocumentCommand : IRequest
    {
        public Guid DocumentID { get; set; }
        public DeleteDocumentCommand() { }
        public DeleteDocumentCommand(Guid documentID)
        {
            DocumentID = documentID;
        }

    }
}
