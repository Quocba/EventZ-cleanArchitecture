using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Wrappers;
using Event.Domain.Entities;

namespace Event.Application.Interface
{
    public interface IEventPackageQuery
    {
        Task<PagedResponse<List<EventPackages>>> GetAllEventPackage(PagedRequest request);

    }
}
