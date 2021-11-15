using LeaseIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> ProductOnSale { get; set; }
    }
}
