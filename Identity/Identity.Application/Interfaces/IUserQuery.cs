using Identity.Application.DTO;
using Identity.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces
{
    public interface IUserQuery
    {
        Task<UserResponse> GetUser(Guid id);
        Task<PagedResponse<List<UserListResponse>>> GetUsers(PagedRequestWithSearch request);
        Task<PagedResponse<List<EventUserListResponse>>> GetEventUsers(PagedRequestEventUser request);
        Task<EventUserResponse> GetEventUser(Guid id);
    }
}
