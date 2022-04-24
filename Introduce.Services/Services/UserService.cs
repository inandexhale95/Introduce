using Introduce.Data.Models;
using Introduce.Data.ViewModels;
using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;

namespace Introduce.Services.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        #region privates methods

        private bool MatchUser(LoginViewModel model)
        {
            return _context.Users
                .Where(u => u.UserId.Equals(model.UserId) && u.Password.Equals(model.Password))
                .FirstOrDefault() != null ? true : false;
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
    }
}
