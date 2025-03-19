using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Command.EditEventDocument
{
    public class EditEventDocumentCommandHandle(IRepository<Domain.Entities.EventDocuments> _eventDocumentRepository) : IRequestHandler<EditEventDocumentsCommand>
    {
        public async Task Handle(EditEventDocumentsCommand request, CancellationToken cancellationToken)
        {

            var checkDocument = await _eventDocumentRepository.GetByIdAsync(request.DocumentID);
            if (checkDocument == null)
            {
                throw new KeyNotFoundException("Event Document Not Found");
            }
            checkDocument.Title = request.Title ?? checkDocument.Title;
            checkDocument.Content = request.Content ?? checkDocument.Content;
            checkDocument.LinkDocument = request.LinkDocument ?? checkDocument.LinkDocument;
            checkDocument.DocumentsType = request.DocumentType != null ? request.DocumentType : checkDocument.DocumentsType;

            await _eventDocumentRepository.UpdateAsync(checkDocument);
            await _eventDocumentRepository.SaveAsync();
        }
    }
}
