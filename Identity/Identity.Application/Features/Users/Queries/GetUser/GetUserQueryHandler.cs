using Dapper;
using Identity.Application.DTO;
using Identity.Application.Interfaces;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler(IUserQuery _userQuery, IUserEventQuery _userEventQuery
        , IRequestClient<CountEventCreatedEvent> _requestCountEventCreated) : IRequestHandler<GetUserQuery, UserResponse>
    {
        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userQuery.GetUser(request.Id);

            var response = await _requestCountEventCreated.GetResponse<CountEventCreatedResponse>(new { UserId = request.Id });

            user.NumberOfEventsCreated = response.Message.NumberOfEventsCreated;

            user.NumberOfEventsAttended = await _userEventQuery.CountEventAttended(request.Id);

            return user;
        }
    }
}
