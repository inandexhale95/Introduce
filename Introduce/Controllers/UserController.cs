using Introduce.Data.ViewModels;
using Introduce.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Introduce.Controllers
{
    [Authorize(Roles = "SystemUser, GeneralUser")]
    public class UserController : Controller
    {
        private HttpContext? _httpContext;
        private readonly IUser _user;

        public IActionResult Index()
        {
            return View();
        }

        public UserController(IHttpContextAccessor accessor,
                              IUser user)
        {
            _user = user;
            _httpContext = accessor.HttpContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("{controller}/Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (_user.MatchUser(model))
                {
                    var user = _user.GetUserInfo(model.UserId);
                    var roleByUser = _user.GetRoleByUser(model.UserId);
                    var userData = user.UserName + "|" +
                                   user.UserEmail + "|" +
                                   user.JoinedDate + "|" +
                                   roleByUser.Role.RoleName;

                    var identity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(type: ClaimTypes.Name,
                                  value: user.UserId),
                        new Claim(type: ClaimTypes.Role,
                                  value: roleByUser.RoleId),
                        new Claim(type: ClaimTypes.UserData,
                                  value: userData)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    await _httpContext.SignInAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                                                   principal: new ClaimsPrincipal(identity),
                                                   properties: new AuthenticationProperties
                                                   {
                                                       ExpiresUtc = DateTime.Now.AddDays(1)
                                                   });

                    TempData["Message"] = "로그인 성공";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    message = "로그인에 필요한 항목을 정확히 입력해주세요.";
                }
            }
            else
            {
                message = "로그인 실패";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }

        [HttpGet("{controller}/LogOut")]
        public async Task<IActionResult> LogOutAsync()
        {
            await _httpContext.SignOutAsync();

            TempData["Message"] = "로그아웃 되었습니다.";

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (_user.Register(model) > 0)
                {
                    TempData["Message"] = "회원가입 성공";
                    return RedirectToAction("Login", "User");
                } 
                else
                {
                    message = "회원가입에 필요한 항목을 올바르게 입력해주세요.";
                }
            }
            else
            {
                message = "회원가입에 실패했습니다.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(model); 
        }

        [HttpGet]
        public IActionResult UpdateInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateInfo(UpdateInfoViewModel model)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (_user.Update(model) > 0)
                {
                    TempData["Message"] = "회원정보 수정에 성공했습니다.";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    message = "각 항목을 형식에 맞게 입력해주세요.";
                }
            }
            else
            {
                message = "회원정보 수정에 실패했습니다.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View();
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            return View();
        }
    }
}
