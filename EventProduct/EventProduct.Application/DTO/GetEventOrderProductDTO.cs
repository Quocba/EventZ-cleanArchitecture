using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class GetEventOrderProductDTO
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public double SellPrice { get; set; }
        public double SalePrice { get; set; }
        public int Stock { get; set; }
        public string ThumbnailURL { get; set; }
        public string ImageURL { get; set; }

        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
