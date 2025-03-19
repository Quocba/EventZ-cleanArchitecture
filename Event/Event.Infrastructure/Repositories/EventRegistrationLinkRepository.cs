using Event.Application.Interface;
using Event.Domain.Entities;
using Event.Infrastructure.Context;
using Event.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Infrastructure.Repositories
{
    public class EventRegistrationLinkRepository(ApplicationDbContext _context) : Repository<EventRegistrationLink>(_context), IEventRegistrationLinkRepository
    {
        public async Task<EventRegistrationLink> GetByCodeAndEventIdAsync(string code, Guid eventId)
        {
            var eventLink = await _context.EventRegistrationLink.FirstOrDefaultAsync(x => x.Code == code && x.EventID == eventId);

            return eventLink;
        }
        public async Task<EventRegistrationLink> GetByCodeAsync(string code)
        {
            var eventLink = await _context.EventRegistrationLink.FirstOrDefaultAsync(x => x.Code == code);

            return eventLink;
        }
    }
}
