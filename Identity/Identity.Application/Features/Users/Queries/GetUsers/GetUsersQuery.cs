using Identity.Application.DTO;
using Identity.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<PagedResponse<List<UserListResponse>>>
    {
        public string Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
