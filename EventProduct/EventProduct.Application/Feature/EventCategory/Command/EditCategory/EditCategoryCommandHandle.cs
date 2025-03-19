using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Command.EditCategory
{
    public class EditCategoryCommandHandle(IRepository<Domain.Entities.EventCategory> _eventCategoryRepository) : IRequestHandler<EditCategoryCommand>
    {
        public async Task Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkUpdate = await _eventCategoryRepository.GetByIdAsync(request.CategoryID);
            if (checkUpdate == null) {
                throw new NotImplementedException("Category Not Found"); 
            }
            var checkAlreadyName = _eventCategoryRepository.FindWithInclude()
                                                            .FirstOrDefault(n => n.Name.ToLower().Equals(request.Name.ToLower()));
            if (checkAlreadyName != null)
            {
                throw new Exception("Category Name Already Exist");
            }

            checkUpdate.Name = request.Name ?? checkUpdate.Name;
            checkUpdate.Description = request.Description ?? checkUpdate.Description;
            await _eventCategoryRepository.UpdateAsync(checkUpdate);
            await _eventCategoryRepository.SaveAsync();
        }
    }
}
