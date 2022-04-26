using Introduce.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.ViewModels
{
    public class ForumPagination
    {
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Forum> Forums { get; set; }
    }
}
