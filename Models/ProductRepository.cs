using LeaseIt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Product> GetAllProducts 
        {
            get
            {
                return _applicationDbContext.Products.Include(c => c.Brand);
            }
        }

        public IEnumerable<Product> GetProductOnSale
        {
            get
            {
                return _applicationDbContext.Products.Include(c => c.Brand).Where(p=> p.IsOnSale);
            }
        }

        public Product GetProductById(int productId)
        {
            return _applicationDbContext.Products.FirstOrDefault(c => c.ProductId == productId);
        }
    }
}
