using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Feature.EventPackage.AddEventPackage;
using Event.Application.Feature.EventPackage.Commands;
using Event.Application.Feature.EventPackage.Commands.UpdateEventPackage;
using Event.Domain.Entities;
using Event.Application.Interfaces;

namespace Event.Application.Interface
{
    public interface IEventPackageRepository : IRepository<EventPackages>
    {
       public Task<EventPackages> GetById(Guid id);
    }
}
