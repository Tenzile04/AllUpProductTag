using AllUpTask.DAL;
using AllUpTask.Extencions;
using AllUpTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AllUpTask.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (slider.ImageUrl != null)
            {

                string fileName = slider.ImageUrl.FileName;
                if (slider.ImageUrl.ContentType != "image/jpeg" && slider.ImageUrl.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "can only upload .jpeg or .png");
                    return View();
                }

                if (slider.ImageUrl.Length > 1048576)
                {
                    ModelState.AddModelError("ImageFile", "File size must be lower than 1mb");
                    return View();
                }



                slider.Image = Helper.SaveFile(_env.WebRootPath, "uploads/sliders", slider.ImageUrl);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Required!");
                return View();
            }
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {

            Slider wantedSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if (wantedSlider == null) return NotFound();

            return View(wantedSlider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {


            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (existSlider == null) return NotFound();
            if (!ModelState.IsValid) return View(slider);


            //string fileName = slider..FileName;
            if (slider.ImageUrl != null)
            {

                if (slider.ImageUrl.ContentType != "image/jpeg" && slider.ImageUrl.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "can only upload .jpeg or .png");
                }

                if (slider.ImageUrl.Length > 1048576)
                {
                    ModelState.AddModelError("ImageFile", "File size must be lower than 1mb");
                }


                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", existSlider.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string newfilename = Helper.SaveFile(_env.WebRootPath, "uploads/sliders", slider.ImageUrl);
            }
               

                existSlider.Title1 = slider.Title1;
                existSlider.Title2 = slider.Title2;
                existSlider.Description = slider.Description;
                existSlider.RedirectUrl = slider.RedirectUrl;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider wantedSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if (wantedSlider == null) return NotFound();

            return View(wantedSlider);
        }

        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", existSlider.Image);

            if (existSlider.ImageUrl != null)
            {
                if (System.IO.File.Exists(path))

                {
                    System.IO.File.Delete(path);
                }
            }


            _context.Sliders.Remove(existSlider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
