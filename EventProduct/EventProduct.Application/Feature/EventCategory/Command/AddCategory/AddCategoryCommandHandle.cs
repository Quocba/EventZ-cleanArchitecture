using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Command.AddCategory
{
    public class AddCategoryCommandHandle(IRepository<Domain.Entities.EventCategory> _categoryRepository) : IRequestHandler<AddCategoryCommand, Guid>
    {

        public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkAlready = _categoryRepository.FindWithInclude()
                                                   .FirstOrDefault(n => n.Name.ToLower().Equals(request.Name.ToLower()));
            if (checkAlready != null)
            {
                throw new Exception("Category already exist");
            }

            Domain.Entities.EventCategory createCategory = new Domain.Entities.EventCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                EventID = request.EventID,
            };
            await _categoryRepository.AddAsync(createCategory);
            await _categoryRepository.SaveAsync();
            return createCategory.Id;

        }
    }
}
