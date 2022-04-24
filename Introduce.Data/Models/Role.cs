using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.Models
{
    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public byte RolePriority { get; set; }

        public ICollection<RoleByUser> RoleByUsers { get; set; }
    }
}
