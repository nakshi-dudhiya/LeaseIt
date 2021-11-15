using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAllBrands { get; }
    }
}
