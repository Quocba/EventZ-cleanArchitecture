using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Command.DeleteEventDocument
{
    public class DeleteDocumentCommandHandle(IRepository<Event.Domain.Entities.EventDocuments> _eventDocumentRepository) : IRequestHandler<DeleteDocumentCommand>
    {
        public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var checkDelete = await _eventDocumentRepository.GetByIdAsync(request.DocumentID);
            if (checkDelete == null) {
                throw new NotImplementedException();
            }
            await _eventDocumentRepository.DeleteAsync(checkDelete);
            await _eventDocumentRepository.SaveAsync();

        }
    }
}
