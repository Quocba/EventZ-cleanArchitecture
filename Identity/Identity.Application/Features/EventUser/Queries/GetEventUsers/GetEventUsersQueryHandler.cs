using Dapper;
using Identity.Application.DTO;
using Identity.Application.Interfaces;
using Identity.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.EventUser.Queries.GetEventUsers
{
    public class GetEventUsersQueryHandler(IUserQuery _userQuery) : IRequestHandler<GetEventUsersQuery, PagedResponse<List<EventUserListResponse>>>
    {

        public async Task<PagedResponse<List<EventUserListResponse>>> Handle(GetEventUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userQuery.GetEventUsers(new PagedRequestEventUser
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    Search = request.Search,
                    EventRoleIds = request.EventRoleId,
                    EventId = request.EventId
                }
            );
        }
    }
}
