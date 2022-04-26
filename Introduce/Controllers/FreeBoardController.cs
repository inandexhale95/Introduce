using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Introduce.Controllers
{
    public class FreeBoardController : Controller
    {
        private readonly IFreeBoard _freeBoard;
        private AppDbContext _context;
        private FreeBoardPagination _pagination;

        public FreeBoardController(IFreeBoard freeBoard,
                                   AppDbContext context)
        {
            _freeBoard = freeBoard;
            _context = context;
            _pagination = new FreeBoardPagination();
        }

        #region privates method

        private async Task<FreeBoardPagination> GetPaginationAsync(int currentPage = 1)
        {
            const int maxRowsPerPage = 10;

            _pagination.FreeBoards = await _context.FreeBoards
                .OrderByDescending(x => x.FreeBoardSeq)
                .Skip((currentPage - 1) * maxRowsPerPage)
                .Take(maxRowsPerPage)
                .ToListAsync();

            double pageCount = (double)((decimal)_context.FreeBoards.Count() / Convert.ToDecimal(maxRowsPerPage));

            _pagination.PageCount = (int)Math.Ceiling(pageCount);
            _pagination.CurrentPageIndex = currentPage;
            return _pagination;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await GetPaginationAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int currentPage)
        {
            var model = await GetPaginationAsync(currentPage);

            return View(model);
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
