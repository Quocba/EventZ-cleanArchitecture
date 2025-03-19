using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Feature.EventPackageRegistration.RegistrationEventPackge;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Application.Interfaces;

namespace Event.Application.Interface
{
    public interface IEventPackageRegistrationRepository : IRepository<EventPackageRegistrations>
    {
        Task<EventPackageRegistrations> GetByID(Guid Id);
    }
}
