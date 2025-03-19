using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventPackage.AddEventPackage
{
    public class AddEventPackageCommandHandle(IEventPackageRepository _eventPackageRepository) : IRequestHandler<AddEventPackageCommand>
    {


        public async Task Handle(AddEventPackageCommand request, CancellationToken cancellationToken)
        {
            EventPackages addEventPackge = new EventPackages
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Benefit = request.Benefit,
                SalePrice = request.SalePrice,
                SellPrice = request.SellPrice,
            };
            await _eventPackageRepository.AddAsync(addEventPackge);
            await _eventPackageRepository.SaveAsync();
        }
    }
}
