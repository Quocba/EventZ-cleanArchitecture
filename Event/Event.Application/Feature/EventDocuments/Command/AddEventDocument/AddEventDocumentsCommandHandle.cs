using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Command.AddEventDocument
{
    public class AddEventDocumentsCommandHandle(IRepository<Domain.Entities.EventDocuments> _eventDocumentsRepository) : IRequestHandler<AddEventDocumentsCommand>
    {
        public async Task Handle(AddEventDocumentsCommand request, CancellationToken cancellationToken)
        {


            var addDocument = new Domain.Entities.EventDocuments
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                LinkDocument = request.LinkDocument,
                DocumentsType = request.DocumentType,
                EventID = request.EventID
            };

            await _eventDocumentsRepository.AddAsync(addDocument);
            await _eventDocumentsRepository.SaveAsync();

        }
    }
}
