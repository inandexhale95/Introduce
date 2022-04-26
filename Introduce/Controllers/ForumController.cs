using Introduce.Data.ViewModels;
using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Introduce.Controllers
{
    [Authorize(Roles = "SystemUser, GeneralUser")]
    public class ForumController : Controller
    {
        private readonly IForum _forum;
        private AppDbContext _context;
        private ForumPagination _pagination;

        public ForumController(IForum forum,
                               AppDbContext context)
        {
            _forum = forum;
            _context = context;
            _pagination = new ForumPagination();
        }

        #region priavte methods

        private async Task<ForumPagination> GetPaginationAsync(int currentPage = 1)
        {
            const int maxRowPerPage = 10;

            _pagination.Forums = await _context.Forums
                .OrderByDescending(f => f.ForumSeq)
                .Skip((currentPage - 1) * maxRowPerPage)
                .Take(maxRowPerPage)
                .ToListAsync();

            double pageCount = (double)((decimal)_context.Forums.Count() / Convert.ToDecimal(maxRowPerPage));

            _pagination.CurrentPageIndex = currentPage;
            _pagination.PageCount = (int)Math.Ceiling(pageCount);
            return _pagination;
        }

        private async Task<ForumPagination> GetSearchAsync(string search, int currentPage = 1)
        {
            const int maxRowPerPage = 10;

            _pagination.Forums = await _context.Forums
                .Where(f => f.Title.Contains(search))
                .OrderByDescending(f => f.ForumSeq)
                .Skip((currentPage - 1) * maxRowPerPage)
                .Take(maxRowPerPage)
                .ToListAsync();

            double pageCount = (double)((decimal)_context.Forums.Count(f => f.Title.Contains(search)) / Convert.ToDecimal(maxRowPerPage));

            _pagination.CurrentPageIndex = currentPage;
            _pagination.PageCount = (int)Math.Ceiling(pageCount);
            return _pagination;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            ForumPagination model;

            if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
            {
                model = await GetSearchAsync(search);
            }
            else
            {
                model = await GetPaginationAsync();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int currentPage)
        {
            var model = await GetPaginationAsync(currentPage);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Write(ForumViewModel model)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (_forum.Write(model) > 0)
                {
                    return RedirectToAction("Index", "Forum");
                }
            }
            else
            {
                message = "게시글 작성 실패";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }
    
        [HttpGet]
        public IActionResult Detail(int forumSeq)
        {
            var model = _forum.Detail(forumSeq);

            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int forumSeq)
        {
            var model = _forum.GetUpdateInfo(forumSeq);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ForumViewModel model)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (_forum.Update(model) > 0)
                {
                    return RedirectToAction("Detail", "Forum", new { forumSeq = model.ForumSeq});
                }
                else
                {

                }
            }
            else
            {

            }

            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }

        [HttpGet]
        public IActionResult Remove(int forumSeq)
        {
            _forum.Remove(forumSeq);

            return RedirectToAction("Index", "Forum");
        }
    }
}
