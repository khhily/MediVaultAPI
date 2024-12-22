using MediVaultAPI.Controllers.Base;
using MediVaultData.Dto.Account;
using MediVaultService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediVaultAPI.Controllers;

/// <summary>
/// 账户控制器类
/// </summary>
/// <param name="accountService"></param>
public class AccountController(AccountService accountService) : BaseController
{
    /// <summary>
    /// 客户端登录
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<UserSessionLite> Login(UserForLogin user)
    {
        return await accountService.LoginAsync(user);
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<UserSessionLite> Register(UserForRegistry user)
    {
        return await accountService.Register(user);
    }

    /// <summary>
    /// 刷新token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("refreshToken")]
    public async Task<UserSessionLite> RefreshToken(RefreshTokenRequest user)
    {
        return await accountService.RefreshAccessToken(user.RefreshToken);
    }
}