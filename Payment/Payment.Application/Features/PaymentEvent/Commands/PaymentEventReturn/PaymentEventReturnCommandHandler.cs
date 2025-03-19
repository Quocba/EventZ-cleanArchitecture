using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using Payment.Application.DTO;
using Payment.Application.Exceptions;
using Payment.Application.Interfaces;
using RabbitMQ.Contract.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Commands.PaymentEventReturn
{
    public class PaymentEventReturnCommandHandler(IVNPayService _vnpayService
        , IRepository<Domain.Entities.PaymentEvent> _paymentEventRepository
        , ISendEndpointProvider _sendEndpointProvider) : IRequestHandler<PaymentEventReturnCommand, string>
    {
        public async Task<string> Handle(PaymentEventReturnCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentEventRepository.GetByIdAsync(Guid.Parse(request.Vnp_OrderInfo)) ?? throw new NotFoundException("Payment not found");

            var response = new VnpayPayResponse()
            {
                Vnp_Amount = request.Vnp_Amount,
                Vnp_BankCode = request.Vnp_BankCode,
                Vnp_BankTranNo = request.Vnp_BankTranNo,
                Vnp_CardType = request.Vnp_CardType,
                Vnp_OrderInfo = request.Vnp_OrderInfo,
                Vnp_PayDate = request.Vnp_PayDate,
                Vnp_ResponseCode = request.Vnp_ResponseCode,
                Vnp_SecureHash = request.Vnp_SecureHash,
                Vnp_SecureHashType = request.Vnp_SecureHashType,
                Vnp_TmnCode = request.Vnp_TmnCode,
                Vnp_TransactionNo = request.Vnp_TransactionNo,
                Vnp_TransactionStatus = request.Vnp_TransactionStatus,
                Vnp_TxnRef = request.Vnp_TxnRef
            };

            bool isValidSignature = _vnpayService.IsValidSignature(response);

            if (isValidSignature)
            {
                if (response.Vnp_ResponseCode == "00")
                {
                    payment.Status = Domain.Enums.PaymentStatus.PAID;

                    var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:payment-event-successful-queue"));

                    await endpoint.Send(new PaymentEventSuccessfulEvent
                    {
                        EventId = payment.EventId
                    }, cancellationToken);
                }
                else
                {
                    payment.Status = Domain.Enums.PaymentStatus.CANCELLED;
                }
            }
            else
            {
                payment.Status = Domain.Enums.PaymentStatus.CANCELLED;
            }

            await _paymentEventRepository.UpdateAsync(payment);
            await _paymentEventRepository.SaveAsync();

            return _vnpayService.GetClientReturnURL();
        }
    }
}
