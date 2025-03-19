using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using MediatR;

namespace Event.Application.Feature.EventRegistrationLink.Commands.DeleteRegistrationLink
{
    public class DeleteRegistrationLinkCommandHandle(IRepository<Domain.Entities.EventRegistrationLink> _eventRegistrationLinkRepository) : IRequestHandler<DeleteRegistrationLinkCommand>
    {
        public async Task Handle(DeleteRegistrationLinkCommand request, CancellationToken cancellationToken)
        {
            var checkDelete = await _eventRegistrationLinkRepository.GetByIdAsync(request.RegistrationLinkID);
            if(checkDelete == null)
            {
                throw new NotImplementedException();
            };
            await _eventRegistrationLinkRepository.DeleteAsync(checkDelete);
            await _eventRegistrationLinkRepository.SaveAsync();
        }

        
    }
}
