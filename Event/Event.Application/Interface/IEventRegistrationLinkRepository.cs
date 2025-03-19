using Event.Application.Interfaces;
using Event.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Interface
{
    public interface IEventRegistrationLinkRepository : IRepository<EventRegistrationLink>
    {
        Task<EventRegistrationLink> GetByCodeAndEventIdAsync(string code, Guid eventId);
        Task<EventRegistrationLink> GetByCodeAsync(string code);
    }
}
