using AllUpTask.DAL;
using AllUpTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllUpTask.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (!ModelState.IsValid) return View(product);

            if (!_context.Brands.Any(x => x.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand Not found");
                return View();
            }
           

            bool check = false;
            if (product.TagIds != null)
            {
                foreach (var tagId in product.TagIds)
                {
                    if (!_context.Tags.Any(x => x.Id == tagId))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found");
                return View();
            }
            else
            {
                if (product.TagIds != null)
                {
                    foreach (var tagId in product.TagIds)
                    {
                        ProductTag productTag = new ProductTag
                        {
                            Product = product,
                            TagId = tagId,

                        };
                        _context.ProductTags.Add(productTag);
                    }
                }
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            Product existProduct = _context.Products.Include(x => x.ProductTags).FirstOrDefault(x => x.Id == id);
            if (existProduct == null)
            {
                return NotFound();
            };
            existProduct.TagIds = existProduct.ProductTags.Select(bt => bt.TagId).ToList();

            return View(existProduct);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            if (!ModelState.IsValid) return View();

            Product existProduct = _context.Products.FirstOrDefault(b => b.Id == product.Id);
            if (existProduct == null) return NotFound();
            if (!_context.Brands.Any(g => g.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand not found!");
                return View();
            }

            var existproduct = _context.Products.Include(x => x.ProductTags).FirstOrDefault(x => x.Id == product.Id);
            if (existproduct == null)
            {
                return NotFound();
            }


            existProduct.ProductTags.RemoveAll(bt => !product.TagIds.Contains(bt.TagId));

            foreach (var tagId in product.TagIds.Where(t => !existProduct.ProductTags.Any(bt => bt.TagId == t)))
            {
                ProductTag productTag = new ProductTag
                {
                    TagId = tagId
                };
                existProduct.ProductTags.Add(productTag);
            }
            existProduct.Name = product.Name;
            existProduct.Desc = product.Desc;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CostPrice = product.CostPrice;
            existProduct.DiscountPercent = product.DiscountPercent;
            existProduct.IsAvailable = product.IsAvailable;
            existProduct.Tax = product.Tax;
            existProduct.Code = product.Code;
            existProduct.BrandId = product.BrandId;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {


            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (id == null) return NotFound();

            Product existProduct = _context.Products.FirstOrDefault(b => b.Id == id);
            if (existProduct == null) return NotFound();


            return View(existProduct);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            Product existProduct = _context.Products.FirstOrDefault(b => b.Id == product.Id);
            if (existProduct == null) return NotFound();

            _context.Products.Remove(existProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

