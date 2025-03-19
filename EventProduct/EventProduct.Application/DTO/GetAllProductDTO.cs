using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class GetAllProductDTO
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double SellPrice { get; set; }
        public double SalePrice { get; set; }
        public int Stock { get; set; }
        public string ThumbnaiURL { get; set; }
        public string ImageURL { get; set; }
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

    }
}
