using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Feature.EventPackage.AddEventPackage;
using Event.Application.Feature.EventPackage.Commands.UpdateEventPackage;
using Event.Application.Interface;
using Event.Domain.Entities;
using Event.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure.Repositories
{
    public class EventPackageRepository(ApplicationDbContext db) : Repository<EventPackages>(db), IEventPackageRepository
    {
        private readonly ApplicationDbContext _db = db;
        public async Task<EventPackages> GetById(Guid id)
        {
            var get = await _db.EventPackages.FirstOrDefaultAsync(x => x.Id == id);
            if (get == null)
            {
                return null;
            }
            return get;
        }
    }
}
