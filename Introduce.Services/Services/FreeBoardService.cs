using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Services
{
    public class FreeBoardService : IFreeBoard
    {
        private readonly AppDbContext _context;

        public FreeBoardService(AppDbContext context)
        {
            _context = context;
        }

        #region privates methods

        IEnumerable<FreeBoard> GetFreeBoardList()
        {
            return _context.FreeBoards
                .OrderByDescending(fb => fb.FreeBoardSeq);
        }

        int Write(FreeBoardViewModel model)
        {
            var freeBoard = new FreeBoard
            {
                Name = model.Name,
                Password = model.Password,
                Content = model.Content,
                CreateDate = DateTime.Now,
            };

            _context.FreeBoards.Add(freeBoard);
            return _context.SaveChanges();
        }

        int Remove(int freeBoardSeq, string password)
        {
            var freeBoard = _context.FreeBoards
                .Where(fb => fb.FreeBoardSeq.Equals(freeBoardSeq))
                .FirstOrDefault(fb => fb.Password.Equals(password));

            if (freeBoard == null)
            {
                return 0;
            }

            _context.FreeBoards.Remove(freeBoard);
            return _context.SaveChanges();
        }

        #endregion


        IEnumerable<FreeBoard> IFreeBoard.GetFreeBoardList()
        {
            return GetFreeBoardList();
        }

        int IFreeBoard.Write(FreeBoardViewModel model)
        {
            return Write(model);
        }

        int IFreeBoard.Remove(int freeBoardSeq, string password)
        {
            return Remove(freeBoardSeq, password);
        }
    }
}
