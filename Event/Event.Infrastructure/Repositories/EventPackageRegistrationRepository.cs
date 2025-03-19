using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Feature.EventPackageRegistration.RegistrationEventPackge;
using Event.Application.Interface;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure.Repositories
{
    public class EventPackageRegistrationRepository(ApplicationDbContext db) : Repository<EventPackageRegistrations>(db), IEventPackageRegistrationRepository
    {
        private readonly ApplicationDbContext _db = db;
        public async Task<EventPackageRegistrations> GetByID(Guid Id)
        {
           var get = await _db.EventPackageRegistrations.FirstOrDefaultAsync(x => x.Id == Id);
            if (get == null)
            {
                return null;
            }
            return get;
        }
    }
}
