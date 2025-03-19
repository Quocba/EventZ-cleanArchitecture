using Dapper;
using Identity.Application.DTO;
using Identity.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.EventUser.Queries.GetEventUser
{
    public class GetEventUserQueryHandler(IUserQuery _userQuery) : IRequestHandler<GetEventUserQuery, EventUserResponse>
    {
        public async Task<EventUserResponse> Handle(GetEventUserQuery request, CancellationToken cancellationToken)
        {
            return await _userQuery.GetEventUser(request.Id);
        }
    }
}
