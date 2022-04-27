using Introduce.Data.HashedPassword;
using Introduce.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Services
{
    public class PasswordHasher : IPasswordHasher
    {

        #region private methods

        private bool CheckTheHasedPassword(string userId, string password, string rngSalt, string hashedPassword)
        {
            return GetHashedPassword(userId, password, rngSalt).Equals(hashedPassword);
        }

        private string GetHashedPassword(string userId, string password, string rngSalt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userId + password,
                salt: Encoding.UTF8.GetBytes(rngSalt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 450000,
                numBytesRequested: 256 / 8));
        }

        private string GetRngSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private HashedPasswordModel SetHashedPasswordInfo(string userId, string password)
        {
            string rngSalt = GetRngSalt();

            return new HashedPasswordModel
            {
                RngSalt = rngSalt,
                HashedPassword = GetHashedPassword(userId, password, rngSalt),
            };
        }

        #endregion


        string IPasswordHasher.GetHashedPassword(string userId, string password, string rngSalt)
        {
            return GetHashedPassword(userId, password, rngSalt);
        }

        string IPasswordHasher.GetRngSalt()
        {
            return GetRngSalt();
        }

        HashedPasswordModel IPasswordHasher.SetHashedPassword(string userId, string password)
        {
            return SetHashedPasswordInfo(userId, password);
        }

        bool IPasswordHasher.CheckTheHasedPassword(string userId, string password, string rngSalt, string hashedPassword)
        {
            return CheckTheHasedPassword(userId, password, rngSalt, hashedPassword);
        }
    }
}
