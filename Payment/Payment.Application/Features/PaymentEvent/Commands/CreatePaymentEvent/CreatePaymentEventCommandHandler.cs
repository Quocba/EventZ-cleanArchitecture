using MassTransit;
using MediatR;
using Payment.Application.Exceptions;
using Payment.Application.Interfaces;
using Payment.Domain.Entities;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Commands.CreatePaymentEvent
{
    public class CreatePaymentEventCommandHandler(IVNPayService _vnpayService
            , IRequestClient<GetEventEvent> _requestGetEvent
            , IRepository<Domain.Entities.PaymentEvent> _paymentEventRepository) : IRequestHandler<CreatePaymentEventCommand, string>
    {
        public async Task<string> Handle(CreatePaymentEventCommand request, CancellationToken cancellationToken)
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
            });

            await _paymentEventRepository.SaveAsync();

            var paymentLink = _vnpayService.GetLink(request.UserId.ToString(), paymentId.ToString(), (decimal)eventDetail.Price, "payment-event-return");

            return paymentLink;
        }
    }
}
