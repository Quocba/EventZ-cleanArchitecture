using Event.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Events.Queries.CountEventCreated
{
    public class CountEventCreatedQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}
