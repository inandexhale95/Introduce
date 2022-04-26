using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.Models
{
    public class Forum
    {
        public int ForumSeq { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public User User { get; set; }
    }
}
