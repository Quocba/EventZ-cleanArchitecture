using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Json;
using MediatR;

namespace Event.Application.Feature.EventPackage.Commands.UpdateEventPackage
{
    public class UpdateEventPackageCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Sell Price must be at least 1.")]
        public double SellPrice { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Sale Price must be at least 1.")]
        public double SalePrice { get; set; }
        public Benefit Benefit { get; set; }
    }
}
