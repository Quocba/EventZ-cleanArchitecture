using Event.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Commands.UpdateLayout
{
    public class UpdateLayoutCommandHandler(IRepository<Domain.Entities.Layout> _layoutRepository) : IRequestHandler<UpdateLayoutCommand>
    {
        public async Task Handle(UpdateLayoutCommand request, CancellationToken cancellationToken)
        {
            var layout = await _layoutRepository.GetByIdAsync(request.Id) ?? throw new Application.Exceptions.NotFoundException("Layout not found");

            layout.LayoutName = request.LayoutName ?? layout.LayoutName;
            layout.LayoutFloorNumber = request.LayoutFloorNumber ?? layout.LayoutFloorNumber;
            layout.LayoutType = request.LayoutType ?? layout.LayoutType;
            layout.Rows = request.Rows ?? layout.Rows;
            layout.Cols = request.Cols ?? layout.Cols;

            await _layoutRepository.UpdateAsync(layout);
            await _layoutRepository.SaveAsync();
        }
    }
}
