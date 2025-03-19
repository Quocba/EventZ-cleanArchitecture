using MassTransit;
using MassTransit.Transports;
using MediatR;
using Payment.Application.Exceptions;
using Payment.Application.Features.PaymentEvent.Commands.PaymentManual;
using Payment.Application.Interfaces;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Commands.PaymentEventManual
{
    public class PaymentEventManualCommandHandler(ISendEndpointProvider _sendEndpointProvider
                , IRequestClient<GetEventEvent> _requestGetEvent
                , IRepository<Domain.Entities.PaymentEvent> _paymentEventRepository) : IRequestHandler<PaymentEventManualCommand>
    {
        public async Task Handle(PaymentEventManualCommand request, CancellationToken cancellationToken)
        {
            var reponse = await _requestGetEvent.GetResponse<GetEventResponse>(
                new GetEventEvent { EventId = request.EventId },
                cancellationToken
            ) ?? throw new NotFoundException("Event not found");

            Guid paymentId = Guid.NewGuid();
            var eventDetail = reponse.Message;

            if (!eventDetail.IsApproved) throw new ValidationException("Event is not approved");

            await _paymentEventRepository.AddAsync(new Domain.Entities.PaymentEvent
            {
                Id = paymentId,
                UserId = request.UserId,
                EventId = request.EventId,
                Content = "Thanh toán phí tạo sự kiện " + eventDetail.Title,
                Amount = (decimal)eventDetail.Price,
                Status = Domain.Enums.PaymentStatus.PAID,
                Type = Domain.Enums.PaymentType.CASH
            });

            await _paymentEventRepository.SaveAsync();

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:payment-event-successful-queue"));

            await endpoint.Send(new PaymentEventSuccessfulEvent
            {
                EventId = request.EventId
            }, cancellationToken);
        }
    }
}
