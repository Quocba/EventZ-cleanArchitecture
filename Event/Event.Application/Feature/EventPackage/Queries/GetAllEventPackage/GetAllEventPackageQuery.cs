using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using MediatR;

namespace Event.Application.Feature.EventPackage.Queries.GetAllEventPackage
{
    public class GetAllEventPackageQuery : IRequest<PagedResponse<List<EventPackages>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
