using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;

namespace Introduce.Services.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _hasher;

        public UserService(AppDbContext context,
                           IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        #region private methods

        private bool MatchUser(LoginViewModel model)
        {
            var user = _context.Users
                .Where(u => u.UserId.Equals(model.UserId))
                .FirstOrDefault();

            if (user == null)
                return false;

            return _hasher.CheckTheHasedPassword(model.UserId, model.Password, user.RngSalt, user.HashedPassword);
        }

        private User GetUserInfo(string userId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserId == userId);

            return user;
        }

        private Role GetRole(string roleId)
        {
            var role = _context.Roles
                .FirstOrDefault(u => u.RoleId.Equals(roleId));

            return role;
        }

        private RoleByUser GetRoleByUser(string userId)
        {
            var roleByUser = _context.RoleByUsers
                .FirstOrDefault(rbu => rbu.UserId.Equals(userId));

            roleByUser.Role = GetRole(roleByUser.RoleId);

            return roleByUser;
        }

        private int Register(RegisterViewModel model)
        {
            var hashedPasswordInfo = _hasher.SetHashedPassword(model.UserId, model.Password);

            var user = new User
            {
                UserId = model.UserId,
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                RngSalt = hashedPasswordInfo.RngSalt,
                HashedPassword = hashedPasswordInfo.HashedPassword,
                JoinedDate = DateTime.Now,
            };

            var roleByUser = new RoleByUser
            {
                UserId = user.UserId,
                RoleId = "GeneralUser"
            };

            _context.Users.Add(user);
            _context.RoleByUsers.Add(roleByUser);
            return _context.SaveChanges();
        }

        private int Update(UpdateInfoViewModel model)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserId.Equals(model.UserId));

            if (user == null)
                return 0;

            var hashedPasswordInfo = _hasher.SetHashedPassword(model.UserId, model.Password);

            _context.Users.Update(user);

            user.UserName = model.UserName;
            user.UserEmail = model.UserEmail;
            user.RngSalt = hashedPasswordInfo.RngSalt;
            user.HashedPassword = hashedPasswordInfo.HashedPassword;

            return _context.SaveChanges();
        }

        private int Withdrawn(WithdrawnViewModel model)
        {
            var user = GetUserInfo(model.UserId);

            if (user == null)
            {
                return 0;
            }

            _context.Users.Remove(user);
            return _context.SaveChanges();
        }

        #endregion

        bool IUser.MatchUser(LoginViewModel model)
        {
            return MatchUser(model);
        }

        User IUser.GetUserInfo(string userId)
        {
            return GetUserInfo(userId);
        }

        RoleByUser IUser.GetRoleByUser(string userId)
        {
            return GetRoleByUser(userId);
        }

        int IUser.Register(RegisterViewModel model)
        {
            return Register(model);
        }

        int IUser.Update(UpdateInfoViewModel model)
        {
            return Update(model);
        }

        int IUser.Withdrawn(WithdrawnViewModel model)
        {
            return Withdrawn(model);
        }
    }
}
