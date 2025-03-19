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

namespace Identity.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(IUserQuery _userQuery) : IRequestHandler<GetUsersQuery, PagedResponse<List<UserListResponse>>>
    {

        public async Task<PagedResponse<List<UserListResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userQuery.GetUsers(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Search = request.Search
            });
        }
    }
}
