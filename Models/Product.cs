using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal LeasePrice { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnail { get; set; }
        public string Color { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsInStock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
