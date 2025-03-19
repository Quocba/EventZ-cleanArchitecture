using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Json;
using MediatR;

namespace Event.Application.Feature.EventPackage.AddEventPackage
{
    public class AddEventPackageCommand : IRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Sell Price must be at least 1.")]
        public double SellPrice { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Sale Price must be at least 1.")]
        public double SalePrice { get; set; }

        [Required]
        public Benefit Benefit { get; set; }
    }
}
