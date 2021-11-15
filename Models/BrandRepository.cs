using LeaseIt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BrandRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Brand> GetAllBrands => _applicationDbContext.Brands;
       
    }
}
