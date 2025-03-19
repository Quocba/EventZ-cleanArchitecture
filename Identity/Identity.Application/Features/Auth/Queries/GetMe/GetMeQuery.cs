using Identity.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Queries.GetMe
{
    public class GetMeQuery : IRequest<UserResponse>
    {
        public Guid Id { get; set; }
    }
}
