using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using MediatR;
using MediatR.Pipeline;

namespace Event.Application.Feature.EventPackage.Commands.UpdateEventPackage
{
    public class UpdateEventPackageCommandHanlde(IEventPackageRepository _eventPackageRepository) : IRequestHandler<UpdateEventPackageCommand>
    {
        private readonly IEventPackageRepository _eventPackgeRepository = _eventPackageRepository;
        public async Task Handle(UpdateEventPackageCommand request, CancellationToken cancellationToken)
        {
            var getEventPackage = await _eventPackgeRepository.GetById(request.Id);
            if (getEventPackage == null)
            {
                throw new KeyNotFoundException($"Package not found");
            }

            getEventPackage.SellPrice = request.SellPrice != 0 ? request.SellPrice : getEventPackage.SellPrice;
            getEventPackage.SalePrice = request.SalePrice != 0 ? request.SalePrice : getEventPackage.SalePrice;
            getEventPackage.Name = request.Name ?? getEventPackage.Name;
            getEventPackage.Benefit = request.Benefit ?? getEventPackage.Benefit;
            await _eventPackgeRepository.UpdateAsync(getEventPackage);
            await _eventPackgeRepository.SaveAsync();
        }
    }
}   
