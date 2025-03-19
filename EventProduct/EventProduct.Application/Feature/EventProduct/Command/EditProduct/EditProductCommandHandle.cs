using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EventProduct.Application.Interfaces;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Command.EditProduct
{
    public class EditProductCommandHandle(IRepository<Domain.Entities.EventProduct> _productRepository) : IRequestHandler<EditProductCommand>
    {
        public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var checkUpdate = await _productRepository.GetByIdAsync(request.ProductID);
            if (checkUpdate == null) { 
                
                throw new NotImplementedException("Product not found");
            }

            checkUpdate.Name = request.Name ?? checkUpdate.Name;
            checkUpdate.Description = request.Description ?? checkUpdate.Description;
            checkUpdate.SalePrice = request.SalePrice != 0 ? request.SalePrice : checkUpdate.SalePrice;
            checkUpdate.SellPrice = request.SellPrice != 0 ? request.SellPrice : checkUpdate.SellPrice;
            if (checkUpdate.Stock == 0)
            {
                checkUpdate.Stock = request.Stock != 0 ? request.Stock : checkUpdate.Stock;
                
            }
            else
            {
                checkUpdate.Stock += request.Stock;
            }
            checkUpdate.ThumbnaiURL = request.ThumbnaiUrl ?? checkUpdate.ThumbnaiURL;
            checkUpdate.ImageURL = request.ImageURL ?? checkUpdate.ImageURL;
            checkUpdate.CategoryID = request.CategoryID != null ? request.CategoryID : checkUpdate.CategoryID;
            await _productRepository.UpdateAsync(checkUpdate);
            await _productRepository.SaveAsync();

        }
    }
}
