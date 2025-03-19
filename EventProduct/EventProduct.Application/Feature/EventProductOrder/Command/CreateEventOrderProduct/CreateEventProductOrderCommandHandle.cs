using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using EventProduct.Domain.Shares;
using MediatR;

namespace EventProduct.Application.Feature.EventProductOrder.Command.CreateEventOrderProduct
{
    public class CreateEventProductOrderCommandHandle(IRepository<Domain.Entities.EventOrderProduct> _eventProductOrderRepository, IRepository<Domain.Entities.EventProduct> _eventProductRepository,
        IRepository<Domain.Entities.EventOrder> _eventOrderRepository)
      : IRequestHandler<CreateEventProductOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateEventProductOrderCommand request, CancellationToken cancellationToken)
        {
            var getProduct = await _eventProductRepository.GetByIdAsync(request.ProductID);
            if (getProduct == null) throw new NotImplementedException();

            var getEventOrder = await _eventOrderRepository.GetByIdAsync(request.EventOrderID);
            if (getEventOrder == null) throw new NotImplementedException();

            if (getProduct.Stock < request.Quantity) throw new Exception("The number of products exceeds the number of items in stock");

            var addEventOrderProduct = new Domain.Entities.EventOrderProduct
            {
                Id = Guid.NewGuid(),
                ProductID = request.ProductID,
                EventOrderID = request.EventOrderID,
                CreatedAt = DateUtility.GetCurrentDateTime(),
                UpdatdAt = DateUtility.GetCurrentDateTime(),
                Price = getProduct.SalePrice * request.Quantity,
                Quantity = request.Quantity
            };
            await _eventProductOrderRepository.AddAsync(addEventOrderProduct);
            await _eventProductOrderRepository.SaveAsync();
            await UpdateEventOrderTotalPrice(getEventOrder.Id);

            return addEventOrderProduct.Id;
        }

        private async Task UpdateEventOrderTotalPrice(Guid eventOrderID)
        {
            var listProductByOderEvent = _eventProductOrderRepository.FindWithInclude(x => x.EventOrder)
                                                                     .Where(x => x.EventOrderID == eventOrderID)
                                                                     .ToList();
            if (listProductByOderEvent.Count == 0)
            {
                throw new NotImplementedException();
            }

            var totalPrice = listProductByOderEvent.Sum(x => x.Price);

            var eventOrder = await _eventOrderRepository.GetByIdAsync(eventOrderID);
            if (eventOrder != null)
            {
                eventOrder.TotalPrice = totalPrice;
                await _eventOrderRepository.UpdateAsync(eventOrder);
                await _eventOrderRepository.SaveAsync();
            }
        }
    }
}
