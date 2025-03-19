using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Command.DeleteProduct
{
    public class DeleteProductCommandHandle(IRepository<Domain.Entities.EventProduct> _productRepository) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var checkDelete = await _productRepository.GetByIdAsync(request.ProductID);
            if (checkDelete == null)
            {
                throw new NotImplementedException("Product not found");
            }

            await _productRepository.DeleteAsync(checkDelete);
            await _productRepository.SaveAsync();
        }
    }
}
