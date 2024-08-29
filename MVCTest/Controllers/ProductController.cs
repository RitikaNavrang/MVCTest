using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;
using X.PagedList.Extensions;

namespace MVCTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public IActionResult GetProducts(string titleSearch, DateTime? dateSearch, int pageNumber = 1)
        {
            var products = _context.Products.ToList();

            if (!string.IsNullOrEmpty(titleSearch))
            {
                products = products.Where(p => p.Title.Contains(titleSearch, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (dateSearch.HasValue)
            {
                products = products.Where(p => p.Date.Date == dateSearch.Value.Date).ToList();
            }

            int pageSize = 10;
            var pagedList = products.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }

        public IActionResult AddProducts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(List<Product> models)
        {
            foreach (var model in models)
            {
                if (model.Image != null)
                {
                    var imagePath = SaveImage(model.Image);
                    model.ImagePath = "/images/" + Path.GetFileName(imagePath);
                }
                _context.Add(model);
            }
            _context.SaveChanges();
            return RedirectToAction("GetProducts");
        }

        private string SaveImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + "_" + image.FileName;

            var imagePath = "/images/" + fileName;
            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return imagePath;
        }
    }
}
