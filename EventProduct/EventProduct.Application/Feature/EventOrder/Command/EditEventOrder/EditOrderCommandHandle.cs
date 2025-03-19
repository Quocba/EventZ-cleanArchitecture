using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using EventProduct.Domain.Shares;
using MediatR;
using RabbitMQ.Client;

namespace EventProduct.Application.Feature.EventOrder.Command.EditEventOrder
{
    public class EditOrderCommandHandle(IRepository<Domain.Entities.EventOrder> _eventOrderRepository)
        : IRequestHandler<EditEventOrderCommand>
    {
        public async Task Handle(EditEventOrderCommand request, CancellationToken cancellationToken)
        {
            var getEventOrder = await _eventOrderRepository.GetByIdAsync(request.EventOrderID);
            if (getEventOrder == null) throw new NotImplementedException();

            getEventOrder.FullName = request.FullName;
            getEventOrder.Address = request.Address;
            getEventOrder.Phone = request.Phone;
            getEventOrder.Email = request.Email;
            getEventOrder.UpdateAt = DateUtility.GetCurrentDateTime();
            getEventOrder.UserID = getEventOrder.UserID;
            getEventOrder.PaymentHistoryID = getEventOrder.PaymentHistoryID;
            getEventOrder.EventID = getEventOrder.EventID;
            getEventOrder.CreateAt = getEventOrder.CreateAt;
            getEventOrder.TotalPrice = getEventOrder.TotalPrice;
            await _eventOrderRepository.UpdateAsync(getEventOrder);
            await _eventOrderRepository.SaveAsync();
        }
    }
}
