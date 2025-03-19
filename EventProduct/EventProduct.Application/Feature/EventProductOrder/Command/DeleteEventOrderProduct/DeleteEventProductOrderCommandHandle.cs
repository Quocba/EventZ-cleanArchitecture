using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventProduct.Application.Feature.EventProductOrder.Command.DeleteEventOrderProduct
{
    public class DeleteEventProductOrderCommandHandle(IRepository<Domain.Entities.EventOrderProduct> _eventOrderProductRepository, IRepository<Domain.Entities.EventOrder> _eventOrderRepository) 
        : IRequestHandler<DeleteEventOrderProductCommand>
    {
        public async Task Handle(DeleteEventOrderProductCommand request, CancellationToken cancellationToken)
        {
            var checkDelete = await _eventOrderProductRepository.GetByIdAsync(request.EventProductOrderID);
            if (checkDelete == null) throw new KeyNotFoundException("Event Product Order Not Found");
            await _eventOrderProductRepository.DeleteAsync(checkDelete);
            await _eventOrderProductRepository.SaveAsync();

            var getEventOrder = await _eventOrderRepository.GetByIdAsync(checkDelete.EventOrderID);
            if(getEventOrder == null) throw new KeyNotFoundException("Event Order Not Found");

            var getListOrderProduct = await _eventOrderProductRepository.FindWithInclude()
                                                                        .Where(x => x.EventOrderID == checkDelete.EventOrderID)
                                                                        .ToListAsync();
            if (getListOrderProduct.Count == 0)
            {
                getEventOrder.TotalPrice = 0;
                await _eventOrderRepository.UpdateAsync(getEventOrder);
            }

            else
            {
                var totalPrice = getListOrderProduct.Select(x => x.Price).Sum();
                getEventOrder.TotalPrice = totalPrice;

                await _eventOrderRepository.UpdateAsync(getEventOrder);
            }
            await _eventOrderRepository.SaveAsync();

        }
    }
}
