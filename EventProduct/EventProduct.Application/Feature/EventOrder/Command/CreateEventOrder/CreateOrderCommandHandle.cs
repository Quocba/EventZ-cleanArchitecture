using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Command.CreateEventOrder
{
    public class CreateOrderCommandHandle(IRepository<Domain.Entities.EventOrder> _evetnOrderRepository) : IRequestHandler<CreateEventOrderCommand,Guid>
    {
        public async Task<Guid> Handle(CreateEventOrderCommand request, CancellationToken cancellationToken)
        {
            var createEventOrder = new Domain.Entities.EventOrder
            {
                Id = Guid.NewGuid(),
                TotalPrice = request.TotalPrice,
                CreateAt = request.CreateAt,
                UpdateAt = request.UpdateAt,
                EventID = request.EventID,
                UserID = request.UserID,
                Address = request.Address,
                Email = request.Email,
                FullName = request.FullName,
                Phone = request.Phone,
                PaymentHistoryID = request.PaymentHistoryID,
            };

            await _evetnOrderRepository.AddAsync(createEventOrder);
            await _evetnOrderRepository.SaveAsync();
            return createEventOrder.Id;
        }
    }
}
