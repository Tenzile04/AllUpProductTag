using AllUpTask.DAL;
using AllUpTask.Models;
using AllUpTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AllUpTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sliders = _context.Sliders.ToList()
            };
            return View(homeViewModel);
        }
    }
}






    
