using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;

namespace Event.Application.Interface
{
    public interface IEventRegistrationLinkQuery
    {
        Task<List<GetEventRegistrationLinkDTO>> GetEventRegistrationLinks(Guid eventID);
    }
}
