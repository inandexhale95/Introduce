using Introduce.Data.HashedPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Interfaces
{
    public interface IPasswordHasher
    {
        bool CheckTheHasedPassword(string userId, string password, string rngSalt, string hashedPassword);
        string GetRngSalt();
        string GetHashedPassword(string userId, string password, string rngSalt);
        HashedPasswordModel SetHashedPassword(string userId, string password);
    }
}
