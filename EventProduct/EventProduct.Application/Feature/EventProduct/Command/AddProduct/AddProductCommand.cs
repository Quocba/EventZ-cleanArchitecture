using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Command.AddProduct
{
    public class AddProductCommand : IRequest<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Sell Price can be not less than 1")]
        public double SellPrice { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Sale Price can be not less than 1")]
        public double SalePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ("Stock can be not less than 0"))]
        public int Stock { get; set; }

        [Required]
        public string ThumbnaiUrl { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public Guid CategoryID { get; set; }
    }
}
