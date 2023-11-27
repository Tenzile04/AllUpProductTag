using Microsoft.AspNetCore.Mvc;

namespace AllUpTask.Areas.Manage.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
