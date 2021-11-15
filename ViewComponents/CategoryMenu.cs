using LeaseIt.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private readonly IBrandRepository _brandRepository;
        
        public CategoryMenu(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IViewComponentResult Invoke()
        {
            var brands = _brandRepository.GetAllBrands.OrderBy(c => c.BrandName);
            return View(brands);
        }
    }
}
