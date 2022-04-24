using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.Models
{
    public class FreeBoard
    {
        public int FreeBoardSeq { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
