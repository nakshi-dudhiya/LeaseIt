using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public interface IProductRepository
    {
        IEnumerable <Product> GetAllProducts { get; }
        IEnumerable<Product> GetProductOnSale { get; }
        Product GetProductById(int productId);
    }
}
