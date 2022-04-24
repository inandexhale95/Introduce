using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using Introduce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Introduce.Controllers
{
    public class FreeBoardController : Controller
    {
        private readonly IFreeBoard _freeBoard;

        public FreeBoardController(IFreeBoard freeBoard)
        {
            _freeBoard = freeBoard;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var freeBoardList = _freeBoard.GetFreeBoardList();

            return View(freeBoardList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Write(FreeBoardViewModel model)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (_freeBoard.Write(model) > 0)
                {
                    return RedirectToAction("Index", "FreeBoard");
                }
            }
            else
            {
                message = "각 항목을 올바르게 입력해주세요.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }
    
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int freeBoardSeq, string password)
        {
            if (ModelState.IsValid)
            {
                if (_freeBoard.Remove(freeBoardSeq, password) > 0)
                {
                    return RedirectToAction("Index", "FreeBoard");
                }
            }

            return RedirectToAction("Index", "FreeBoard");
        }
    }
}
