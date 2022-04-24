using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Interfaces
{
    public interface IFreeBoard
    {
        IEnumerable<FreeBoard> GetFreeBoardList();
        int Write(FreeBoardViewModel model);
        int Remove(int freeBoardSeq, string password);
    }
}
