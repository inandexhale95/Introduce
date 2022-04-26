using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Interfaces
{
    public interface IForum
    {
        IEnumerable<Forum> GetForumList();
        int Write(ForumViewModel model);
        Forum Detail(int forumSeq);
        Forum GetUpdateInfo(int forumSeq);
        int Update(ForumViewModel model);
        int Remove(int forumSeq); 
    }
}
