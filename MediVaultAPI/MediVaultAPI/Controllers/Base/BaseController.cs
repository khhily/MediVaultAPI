using MediVault.Common.Authentication;
using MediVault.Utils.Token;
using MediVaultData.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediVaultAPI.Controllers.Base;

/// <summary>
/// 基础控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = SimpleAuthenticationHandler.SchemeName)]
public class BaseController : ControllerBase
{
    /// <summary>
    /// 登录用户
    /// </summary>
    protected LoginUser? CurrentUser {
        get
        {
            var tokenUser = TokenUtils.GetTokenUser(HttpContext.User);
            
            if (tokenUser == null) return null;

            return new LoginUser
            {
                UserId = tokenUser.UserId,
                UserName = tokenUser.UserName,
            };
        }
    }
}