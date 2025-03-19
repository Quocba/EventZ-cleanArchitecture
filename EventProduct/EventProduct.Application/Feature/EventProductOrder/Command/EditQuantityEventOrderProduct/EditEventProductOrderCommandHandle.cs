using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventProduct.Application.Feature.EventProductOrder.Command.EditQuantityEventOrderProduct
{
    public class EditEventProductOrderCommandHandle(IRepository<Domain.Entities.EventOrderProduct> _eventOrderProductRepository
        , IRepository<Domain.Entities.EventOrder> _eventOrderRepository, IRepository<Domain.Entities.EventProduct> _eventProductRepository)
        : IRequestHandler<EditEventProductOrderCommand>
    {
        public async Task Handle(EditEventProductOrderCommand request, CancellationToken cancellationToken)
        {   
            // Update Price in event_order_product
            var checkUpdate = await _eventOrderProductRepository.GetByIdAsync(request.EventProductOrderID);
            if (checkUpdate == null) throw new KeyNotFoundException("Event Order Product Not Found");
            
            var getProduct = await _eventProductRepository.GetByIdAsync(checkUpdate.ProductID);

            checkUpdate.Quantity = request.Quantity;
            checkUpdate.Price = getProduct.SalePrice * request.Quantity;
            await _eventOrderProductRepository.UpdateAsync(checkUpdate);
            await _eventOrderRepository.SaveAsync();


            //Update total price in eventOrder
            var getEventOrder = await _eventOrderRepository.GetByIdAsync(checkUpdate.EventOrderID);
            if (getEventOrder == null) throw new KeyNotFoundException();

            var getListEventProductOrder = await _eventOrderProductRepository.FindWithInclude()
                                                                             .Where(x => x.EventOrderID == checkUpdate.EventOrderID)
                                                                             .ToListAsync();
            if(getListEventProductOrder.Count == 0)
            {
               getEventOrder.TotalPrice = 0;
               await _eventOrderRepository.UpdateAsync(getEventOrder);
            }

            var totalPrice = getListEventProductOrder.Select(x => x.Price).Sum();
            getEventOrder.TotalPrice = totalPrice;
            await _eventOrderRepository.UpdateAsync(getEventOrder);
            await _eventOrderRepository.SaveAsync();
        }
    }
}

