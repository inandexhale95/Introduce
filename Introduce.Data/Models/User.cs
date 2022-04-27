using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string RngSalt { get; set; }
        public string HashedPassword { get; set; }
        public DateTime JoinedDate { get; set; }


        public RoleByUser RoleByUser { get; set; }
        public ICollection<Forum> Forums { get; set; }
    }
}
