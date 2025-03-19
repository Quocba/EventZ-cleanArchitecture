using Event.Application.Interfaces;
using Event.Domain.Shares;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Commands.CreateLayout
{
    public class CreateLayoutCommandHandler(IRepository<Domain.Entities.Layout> _layoutRepository) : IRequestHandler<CreateLayoutCommand, Guid>
    {
        public async Task<Guid> Handle(CreateLayoutCommand request, CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();

            await _layoutRepository.AddAsync(new Domain.Entities.Layout
            {
                LayoutName = request.LayoutName,
                LayoutFloorNumber = request.LayoutFloorNumber,
                LayoutType = request.LayoutType,
                Rows = request.Rows,
                Cols = request.Cols,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateUtility.GetCurrentDateTime(),
                Id = id
            });

            await _layoutRepository.SaveAsync();

            return id;
        }
    }
}
