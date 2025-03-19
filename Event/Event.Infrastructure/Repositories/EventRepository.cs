using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Feature.Events;
using Event.Application.Interface;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Infrastructure.Context;
using Event.Application.Interfaces;

namespace Event.Infrastructure.Repositories
{
    public class EventRepository(ApplicationDbContext db) : Repository<Events>(db), IEventRepository
    {
    }
}
