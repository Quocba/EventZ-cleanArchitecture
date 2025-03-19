using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Features.Users.Queries.GetUser;
using Identity.Domain.Entities;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DTO;

namespace Identity.Application.MessageBus
{
    public class GetUserConsumer(ISender _sender) : IConsumer<GetUserRequestDTO>
    {
        public async Task Consume(ConsumeContext<GetUserRequestDTO> context)
        {
            var query = new GetUserQuery { Id = context.Message.ID };
            var user = await _sender.Send(query);
            if (user == null)
            {
                await context.RespondAsync<GetUserResponseDTO>(null);
                return;
            }

            var response = new GetUserResponseDTO
            { 
                Id = context.Message.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Email = user.Email,
                Phone = user.Phone
            };

            await context.RespondAsync(response);

        }
    }
}
