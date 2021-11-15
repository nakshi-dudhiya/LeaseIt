using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaseIt.Data;
using LeaseIt.Models;
using LeaseIt.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LeaseIt.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductsController(ApplicationDbContext context, IProductRepository productRepository, 
            IBrandRepository brandRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }

        //Display the list of products
        public IActionResult List()
        {
            var productListViewModel = new ProductListViewModel();
            productListViewModel.Products = _productRepository.GetAllProducts;
            productListViewModel.CurrentBrand = "Best Sellers";
            return View(productListViewModel);
        }

        public IActionResult Sort(string brand)
        {
            IEnumerable<Product> products;
            string currentBrand;
            var productListViewModel = new ProductListViewModel();

            if (string.IsNullOrEmpty(brand))
            {
                products = _productRepository.GetAllProducts.OrderBy(c => c.ProductId);
                currentBrand = "All Products";
            }
            else
            {
                products = _productRepository.GetAllProducts.Where(c => c.Brand.BrandName == brand);
                currentBrand = _brandRepository.GetAllBrands.FirstOrDefault(c => c.BrandName ==
                   brand)?.BrandName;
            }
            productListViewModel.Products = products;
            productListViewModel.CurrentBrand = currentBrand;

            return View("List",productListViewModel);
        }

        // GET: Products
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Brand);
            return View(await applicationDbContext.ToListAsync());
        }


        //Search
        public IActionResult Filter(string searchString)
        {
            var productListViewModel = new ProductListViewModel();
            productListViewModel.Products = _productRepository.GetAllProducts;
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = productListViewModel.Products.Where(n => n.ProductName.Contains(searchString) ||
                     n.Description.Contains(searchString)).ToList();
                productListViewModel.Products = filteredResult;
                return View("List",productListViewModel);
            }
            return View(productListViewModel);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product =  _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> ShowDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        // GET: Products/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Price,LeasePrice,ImageUrl,ImageThumbnail,Color,IsOnSale,IsInStock,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Price,LeasePrice,ImageUrl,ImageThumbnail,Color,IsOnSale,IsInStock,BrandId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
