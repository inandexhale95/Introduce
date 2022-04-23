using Microsoft.AspNetCore.Mvc;

namespace Introduce.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
