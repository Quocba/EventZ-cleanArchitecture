using Identity.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.EventUser.Queries.GetEventUser
{
    public class GetEventUserQuery : IRequest<EventUserResponse>
    {
        public Guid Id { get; set; }
    }
}
