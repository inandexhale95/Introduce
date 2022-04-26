using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Services
{
    public class ForumService : IForum

    {
        private readonly AppDbContext _context;
        private HttpContext _httpContext;

        public ForumService(AppDbContext context,
                             IHttpContextAccessor accessor)
        {
            _context = context;
            _httpContext = accessor.HttpContext;
        }

        #region private methods

        private IEnumerable<Forum> GetForumList()
        {
            return _context.Forums
                .OrderByDescending(f => f.ForumSeq);
        }

        private int Write(ForumViewModel model)
        {
            var forum = new Forum
            {
                Title = model.Title,
                Content = model.Content,
                UserId = _httpContext.User.Identity.Name,
                CreateDate = DateTime.Now,
            };

            _context.Forums.Add(forum);
            return _context.SaveChanges();
        }

        private Forum GetForumInfoByForumSeq(int forumSeq)
        {
            var forum = _context.Forums
                .FirstOrDefault(f => f.ForumSeq.Equals(forumSeq));

            if (forum == null)
            {
                throw new Exception("에러발생");
            }

            return forum;
        }

        private Forum Detail(int forumSeq)
        {
            var forum = GetForumInfoByForumSeq(forumSeq);

            return forum;
        }

        private Forum GetUpdateInfo(int forumSeq)
        {
            var forum = GetForumInfoByForumSeq(forumSeq);

            return forum;
        }

        private int Update(ForumViewModel model)
        {
            var forum = GetForumInfoByForumSeq(model.ForumSeq);

            _context.Forums.Update(forum);
            
            forum.Title = model.Title;
            forum.Content = model.Content;

            return _context.SaveChanges();
        }
        
        private int Remove(int forumSeq)
        {
            var forum = GetForumInfoByForumSeq(forumSeq);

            _context.Forums.Remove(forum);

            return _context.SaveChanges();
        }

        #endregion


        IEnumerable<Forum> IForum.GetForumList()
        {
            return GetForumList();
        }

        int IForum.Write(ForumViewModel model)
        {
            return Write(model);
        }

        Forum IForum.Detail(int forumSeq)
        {
            return Detail(forumSeq);
        }

        Forum IForum.GetUpdateInfo(int forumSeq)
        {
            return GetUpdateInfo(forumSeq);
        }

        int IForum.Update(ForumViewModel model)
        {
            return Update(model);
        }

        int IForum.Remove(int forumSeq)
        {
            return Remove(forumSeq);
        }
    }
}
