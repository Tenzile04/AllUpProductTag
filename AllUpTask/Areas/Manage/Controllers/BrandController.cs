using AllUpTask.DAL;
using AllUpTask.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;

namespace AllUpTask.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Brand> Brands = _context.Brands.ToList();
            return View(Brands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Brands.Any(x => x.Name.ToLower() == brand.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Brand alredy exist!");
                return View();
            }

            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();
            Brand existBrand = _context.Brands.FirstOrDefault(g => g.Id == id);
            if (existBrand == null) return NotFound();

            return View(existBrand);
        }
        [HttpPost]
        public IActionResult Update(Brand brand)
        {
            if (!ModelState.IsValid) return View();
            Brand existBrand = _context.Brands.FirstOrDefault(g => g.Id == brand.Id);
            if (existBrand == null) return NotFound();

            if (_context.Brands.Any(x => x.Id != brand.Id && x.Name.ToLower() == brand.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Brand alredy exist!");
                return View();
            }
            existBrand.Name = brand.Name;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Brand existBrand = _context.Brands.FirstOrDefault(g => g.Id == id);
            return View(existBrand);
        }

        [HttpPost]
        public IActionResult Delete(Brand brand)
        {

            Brand existBrand = _context.Brands.FirstOrDefault(g => g.Id == brand.Id);
            if (existBrand == null) return NotFound();

            _context.Brands.Remove(existBrand);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
