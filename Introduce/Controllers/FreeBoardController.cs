using Microsoft.AspNetCore.Mvc;

namespace Introduce.Controllers
{
    public class FreeBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
