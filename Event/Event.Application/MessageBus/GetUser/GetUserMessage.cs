using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ.Contract.DTO;

namespace Event.Application.MessageBus.GetUser
{
    public class GetUserMessage(IRequestClient<GetUserRequestDTO> _requestClient)
    {
        public async Task<GetUserResponseDTO> GetUser(Guid userID)
        {
            var request = new GetUserRequestDTO { ID = userID };
            var response = await _requestClient.GetResponse<GetUserResponseDTO>(request);
            return response.Message;
        }
    }
}
