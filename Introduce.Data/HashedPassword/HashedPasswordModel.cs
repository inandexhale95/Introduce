using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.HashedPassword
{
    public class HashedPasswordModel
    {
        public string RngSalt { get; set; }
        public string HashedPassword { get; set; }
    }
}
