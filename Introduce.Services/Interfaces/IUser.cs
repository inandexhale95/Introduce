using Introduce.Data.Models;
using Introduce.Data.ViewModels;

namespace Introduce.Services.Interfaces
{
    public interface IUser
    {
        bool MatchUser(LoginViewModel model);
        /// <summary>
        /// 회원정보를 가져오는 메서드
        /// </summary>
        /// <param name="userId">사용자의 ID로 회원정보를 가져온다.</param>
        /// <returns></returns>
        User GetUserInfo(string userId);

        /// <summary>
        /// 회원가입 시 사용자의 권한을 설정하는 메서드
        /// </summary>
        /// <param name="userId">사용자의 ID로 회원의 권한 ID를 찾고 권한을 설정한다.</param>
        /// <returns></returns>
        RoleByUser GetRoleByUser(string userId);

        /// <summary>
        /// 회원가입 메서드
        /// </summary>
        /// <param name="model">회원가입에 필요한 정보</param>
        /// <returns></returns>
        int Register(RegisterViewModel model);

        /// <summary>
        /// 회원정보를 수정하는 메서드
        /// </summary>
        /// <param name="model">회원수정에 필요한 정보</param>
        /// <returns></returns>
        int Update(UpdateInfoViewModel model);

        int Withdrawn(WithdrawnViewModel model);
    }
}
