using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Application.Interfaces;
using Event.Infrastructure.Shares;
using MediatR;

namespace Event.Application.Feature.EventRegistrationLink.Commands.CreateRegistrationLink
{
    public class CreateRegistrationLinkCommandHandle(IEventRegistrationLinkRepository _eventRegistrationLinkRepository)
        : IRequestHandler<CreateRegistrationLinkComand>
    {
        public async Task Handle(CreateRegistrationLinkComand request, CancellationToken cancellationToken)
        {
            var registerLink = await _eventRegistrationLinkRepository.GetByCodeAsync(request.Code);

            if (registerLink != null) throw new ArgumentException("Code already exists");   

            Domain.Entities.EventRegistrationLink create = new Domain.Entities.EventRegistrationLink
            {
                Id = Guid.NewGuid(),
                Code = Util.GenerateUniqueString(6),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EventID = request.EventID
            };
            await _eventRegistrationLinkRepository.AddAsync(create);
            await _eventRegistrationLinkRepository.SaveAsync();
        }
    }
}
