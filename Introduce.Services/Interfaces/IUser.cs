using Introduce.Data.Models;
using Introduce.Data.ViewModels;

namespace Introduce.Services.Interfaces
{
    public interface IUser
    {
        bool MatchUser(LoginViewModel model);
        User GetUserInfo(string userId);
        RoleByUser GetRoleByUser(string userId);
        int Register(RegisterViewModel model);
    }
}
