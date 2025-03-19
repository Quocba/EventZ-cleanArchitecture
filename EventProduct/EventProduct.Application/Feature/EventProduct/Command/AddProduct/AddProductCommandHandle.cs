using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Command.AddProduct
{
    public class AddProductCommandHandle(IRepository<Domain.Entities.EventProduct> _productRepository) : IRequestHandler<AddProductCommand, Guid>
    {
        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var checkAlreadyProduct = _productRepository.FindWithInclude(c => c.Category)
                                                        .FirstOrDefault(x => x.Name.ToLower().Equals(request.Name.ToLower()) &&
                                                         x.CategoryID == request.CategoryID);
            if (checkAlreadyProduct != null) {

                checkAlreadyProduct.Stock += request.Stock;
                await _productRepository.UpdateAsync(checkAlreadyProduct);
                await _productRepository.SaveAsync();
                return checkAlreadyProduct.Id;
            }
            else
            {
                var addProduct = new Domain.Entities.EventProduct
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    SellPrice = request.SellPrice,
                    SalePrice = request.SalePrice,
                    Stock = request.Stock,
                    ThumbnaiURL = request.ThumbnaiUrl,
                    ImageURL = request.ImageURL,
                    CategoryID = request.CategoryID,
                };
                await _productRepository.AddAsync(addProduct);
                await _productRepository.SaveAsync();
                return addProduct.Id;
            }
        }
    }
}
