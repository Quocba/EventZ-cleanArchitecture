//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;
//using EventProduct.Application.DTO;
//using MassTransit;
//using MediatR;
//using Microsoft.Extensions.Configuration;
//using RabbitMQ.Contract.DTO;
//using RabbitMQ.Contract.EventType;

//namespace EventProduct.Application.EventTypeBus
//{
//    public class CheckEventTypeConsumer(ISender _sender, IConfiguration _configuration) : IConsumer<CheckEventType>
//    {
//        public async Task Consume(ConsumeContext<CheckEventType> context)
//        {
//            HttpClient httpClient = new HttpClient();
//            var jsonObject = await httpClient.GetFromJsonAsync<APIResponseDTO<Guid>>($"{_configuration["BaseURL:EventService"]}/event-type/get-by-id?EventTypeID={context.Message.EventTypeID}");

//            if (jsonObject?.Data == null)
//            {
//                throw new InvalidOperationException("Event type not found");
//            }
//            var response = await context.RespondAsync(new CheckEventTypeResponse { EventTypeID = jsonObject.Data });
//        }
//    }
//}
