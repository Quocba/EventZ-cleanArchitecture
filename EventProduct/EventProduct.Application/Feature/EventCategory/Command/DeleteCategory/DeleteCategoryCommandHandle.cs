using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandle(IRepository<Domain.Entities.EventCategory> _categoryRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkDelete = await _categoryRepository.GetByIdAsync(request.CategoryID);
            if (checkDelete == null)
            {
                throw new NotImplementedException("Category Not FOund");
            }

            await _categoryRepository.DeleteAsync(checkDelete);
            await _categoryRepository.SaveAsync();
        }
    }
}
