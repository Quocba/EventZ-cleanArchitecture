using Event.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Commands.DeleteLayout
{
    public class DeleteLayoutCommandHandler(IRepository<Domain.Entities.Layout> _layoutRepository) : IRequestHandler<DeleteLayoutCommand>
    {
        public async Task Handle(DeleteLayoutCommand request, CancellationToken cancellationToken)
        {
            var layout = await _layoutRepository.GetByIdAsync(request.LayoutId) ?? throw new Application.Exceptions.NotFoundException("Layout not found");

            layout.IsDeleted = true;

            await _layoutRepository.UpdateAsync(layout);
            await _layoutRepository.SaveAsync();
        }
    }
}
